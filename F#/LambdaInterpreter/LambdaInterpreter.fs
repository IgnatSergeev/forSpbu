module LambdaInterpreter

type LambdaTerm = 
    | Var of string
    | App of LambdaTerm * LambdaTerm
    | Abstr of string * LambdaTerm

let rec mapTerm mapping term =
    match term with
    | Var _ -> mapping term 
    | App (lhs, rhs) -> mapping (App(mapTerm mapping lhs, mapTerm mapping rhs))
    | Abstr (param, rhs) -> mapping (Abstr(param, mapTerm mapping rhs))

let bfsFoldTerm fold acc term =
    let rec bfsFoldHelper fold acc queue = 
        match queue with
        | [] -> acc
        | Var(var)::tail -> 
            bfsFoldHelper fold acc tail |> fold (Var(var))
        | App(lhs, rhs)::tail -> 
            bfsFoldHelper fold acc (tail @ [ lhs; rhs ]) |> fold (App(lhs, rhs))
        | Abstr(param, rhs)::tail -> 
            bfsFoldHelper fold acc (tail @ [ rhs ]) |> fold (Abstr(param, rhs))
    bfsFoldHelper fold acc [ term ]

let generateName oldName mainTerm =
    let fold term acc =
        match term with
        | Var v -> Set.add v acc
        | Abstr(param, _) -> Set.remove param acc
        | _ -> acc
    let freeNames = bfsFoldTerm fold Set.empty mainTerm
    let rec generateNameHelper counter = 
        let newName = oldName + counter.ToString()
        if freeNames.Contains(newName) then generateNameHelper (counter + 1) else newName 
        
    generateNameHelper 1

let alphaTransform term = 
    match term with
    | Abstr(param, subterm) -> 
        let newName = generateName param subterm
        let mapping mappingTerm = 
            match mappingTerm with
            | Var v -> if v.Equals(param) then Var(newName) else term
            | _ -> term

        mapTerm mapping term 
    | _ -> term

let betaReduce mainTerm = 
    let reduceMatch term acc = 
        match (term, acc) with
        | (App(Abstr(_,_), _), None) -> Some(term)
        | _ -> acc
    let reduceTerm = bfsFoldTerm reduceMatch None mainTerm
    match reduceTerm with
    | None -> mainTerm
    | Some(App(Abstr(param, abstrTerm), appTerm)) -> 
        let reduceMapping term = 
            match term with
            | Var v when v.Equals(param) -> appTerm 
            | Abstr(p, _) when p.Equals(param) -> alphaTransform term 
            | _ -> term

        let reducedTerm = mapTerm reduceMapping abstrTerm 
        mapTerm (fun x -> if x = reduceTerm.Value then reducedTerm else x) mainTerm
    | _ -> mainTerm

let rec normalize term = 
    let reduced = betaReduce term
    if term = reduced then term else betaReduce reduced
