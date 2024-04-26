
module Square

/// Generates string with star square
let genStarSquare n =
    let rec replicateStr str times = 
        match times with
        | 1 -> str
        | _ ->
            let prevStr = (replicateStr str (times - 1)):string
            prevStr.Insert(0, str)
    
    let rec genSquare i =
        match i with
        | 1 ->
            replicateStr "*" n
        | _ ->
            let prev = genSquare (i - 1)
            if i = n then
                prev.Insert(prev.Length, "\n" + replicateStr "*" n)
            else
                prev.Insert(prev.Length, "\n*" + replicateStr " " (n - 2) + "*")
    
    if n > 0 then
        genSquare n
    else
        ""
   