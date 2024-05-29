module EvenNumbers

let foldEvenCounter list = 
    list |> List.fold (fun acc x -> acc + ((x + 1u) % 2u)) 0u 

let filterEvenCounter list = 
    list |> List.filter (fun x -> (x % 2u) = 0u) |> List.length |> uint32

let mapEvenCounter list = 
    list |> List.map (fun x -> (x + 1u) % 2u) |> List.sum
