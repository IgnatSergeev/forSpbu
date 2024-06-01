module LambdaInterpreter

/// <summary>
/// Represents lamda term
/// </summary>
type LambdaTerm = 
    | Var of string
    | App of LambdaTerm * LambdaTerm
    | Abstr of string * LambdaTerm

/// <summary>
/// Recursively maps given function to the lamda term, skipping recursive mapping, when needed
/// </summary>
/// <param name="mapping"> The function to produce a new term from the given one, and the condition of whether recursion inside current term should be skipped </param>
/// <param name="term"> Term to map </param>
/// <returns> Mapped term </returns>
let rec mapTerm mapping term =
    let (mappedTerm, skip) = mapping term
    if not skip then
        match mappedTerm with
        | Var _ -> mappedTerm 
        | App(lhs, rhs) -> App(mapTerm mapping lhs, mapTerm mapping rhs)
        | Abstr (param, rhs) -> Abstr(param, mapTerm mapping rhs)
    else
        mappedTerm

/// <summary>
/// Applies function to each term recursively threading an accumulator argument through computation
/// </summary>
/// <param name="mapping"> Function to update accumulator based on term value </param>
/// <param name="acc"> The initial state </param>
/// <param name="term"> Term to fold </param>
/// <returns> Mapped term </returns>
let bfsFoldTerm fold acc term =
    let rec bfsFoldHelper fold acc queue = 
        match queue with
        | [] -> acc
        | Var(var)::tail -> 
            bfsFoldHelper fold (fold (Var(var)) acc) tail
        | App(lhs, rhs)::tail -> 
            bfsFoldHelper fold (fold (App(lhs, rhs)) acc) (tail @ [ lhs; rhs ]) 
        | Abstr(param, rhs)::tail -> 
            bfsFoldHelper fold (fold (Abstr(param, rhs)) acc) (tail @ [ rhs ]) 
    bfsFoldHelper fold acc [ term ]

/// <summary>
/// Returns set of all variable names in term
/// </summary>
/// <param name="mainTerm"> Term to search names from </param>
/// <returns> Set of names </returns>
let getAllNames mainTerm = 
    let fold term acc =
        match term with
        | Var v -> Set.add v acc
        | Abstr(param, _) -> Set.add param acc
        | _ -> acc
    bfsFoldTerm fold Set.empty mainTerm

/// <summary>
/// Generates new variable name based on give one, which doesnt interrupt with given term 
/// </summary>
/// <param name="oldName"> Name to base new one from </param>
/// <param name="mainTerm"> Term to search names from </param>
/// <returns> New name </returns>
let generateName oldName mainTerm =
    let freeNames = getAllNames mainTerm
    let rec generateNameHelper counter = 
        let newName = oldName + counter.ToString()
        if freeNames.Contains(newName) then generateNameHelper (counter + 1) else newName 
    generateNameHelper 1

/// <summary>
/// Applies alpha transformation on given term
/// </summary>
/// <param name="term"> Term to apply transofrmation to </param>
/// <returns> Transformed term </returns>
let alphaTransform term = 
    match term with
    | Abstr(param, subterm) -> 
        let newName = generateName param subterm
        let mapping mappingTerm = 
            match mappingTerm with
            | Var v when v.Equals(param)-> (Var(newName), false)
            | _ -> (mappingTerm, false)
        Abstr(newName, mapTerm mapping subterm) 
    | _ -> term

/// <summary>
/// Applies beta transformation based on normal strategy on given term
/// </summary>
/// <param name="mainTerm"> Term to apply transofrmation to </param>
/// <returns> Transformed term </returns>
let betaTransform mainTerm = 
    let reduceMatch term acc = 
        match (term, acc) with
        | (App(Abstr(_,_), _), None) -> Some(term)
        | _ -> acc
    let reduceTerm = bfsFoldTerm reduceMatch None mainTerm
    match reduceTerm with
    | None -> mainTerm
    | Some(App(Abstr(param, abstrTerm), appTerm)) -> 
        let allNames = getAllNames appTerm
        let reduceMapping term = 
            match term with
            | Var v when v.Equals(param) -> (appTerm, true) 
            | Abstr(p, _) when allNames.Contains(p) -> (alphaTransform term, false)
            | _ -> (term, false)

        let reducedTerm = mapTerm reduceMapping abstrTerm 
        mapTerm (fun x -> if x = reduceTerm.Value then (reducedTerm, true) else (x, false)) mainTerm
    | _ -> mainTerm

/// <summary>
/// Normalizes term if possible based on normal reduction strategy
/// </summary>
/// <param name="term"> Term to normalize </param>
/// <returns> Transformed term </returns>
let rec normalize term = 
    let reduced = betaTransform term
    if term = reduced then term else normalize reduced
