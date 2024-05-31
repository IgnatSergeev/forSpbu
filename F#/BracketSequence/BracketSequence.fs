module BracketSequence

/// <summary>
/// Get corresponding opened bracket
/// </summary>
/// <param name="str"> Char to check </param>
/// <returns> If given char is any braket, returns corresponding opened bracket, None otherwise </returns>
let bracketGetOpened str = 
    match str with
    | '(' | ')' -> Some('(')
    | '{' | '}' -> Some('{')
    | '[' | ']' -> Some('[')
    | _ -> None

/// <summary>
/// Checks if string is correct bracket sequence
/// </summary>
/// <param name="str"> String to check </param>
/// <returns> If given bracket string is correct, true, false otherwise </returns>
let rec checkBracketSequence str = 
    let rec bracketHelper list (openedStack: List<char>) = 
        match list with
        | [] -> openedStack.IsEmpty
        | head::tail -> 
            let opened = bracketGetOpened head
            match opened with
            | Some (a) -> 
                let isOpened = a.Equals(head)
                match openedStack with
                | openedHead::openedTail -> 
                    if isOpened then
                        bracketHelper tail (a::openedStack)
                    else if a.Equals(openedHead) then
                        bracketHelper tail openedTail
                    else 
                        false
                | [] -> 
                    if isOpened then
                        bracketHelper tail [ a ]
                    else 
                        false
            | _ -> false
    bracketHelper (Seq.toList str) List.empty

