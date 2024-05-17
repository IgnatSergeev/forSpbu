module Homework1

/// <summary>
/// Calculates factorial of the given number
/// </summary>
/// <param name="num">Number to calculate factorial from</param>
let rec factorial num =
    let rec factorialHelper acc number =
        match number with
         | 0u -> acc
         | _ -> factorialHelper (acc * number) (number - 1u)
    factorialHelper 1u num
    
/// <summary>
/// Calculates fibonacci number
/// </summary>
/// <param name="num">Index of fibonacci number(starting with 0)</param>
let rec fibonacci num =
    let rec fibonacciHelper ind curr next =
        match ind with
         | 0u -> curr
         | _ -> fibonacciHelper (ind - 1u) next (curr + next)
    fibonacciHelper num 1 1

/// <summary>
/// Reverses the list
/// </summary>
/// <param name="list">List to reverse</param>
/// <returns>Reversed list</returns>
let rec reverseList list =
    let rec reverseListHelper list tail =
        match list with
        | [] -> tail
        | head :: t -> reverseListHelper t (head :: tail)
    reverseListHelper list []

/// <summary>
/// Generates list of [2^n; 2^(n + 1); ...; 2^(n + m)]
/// </summary>
/// <param name="n">First power</param>
/// <param name="m">Number of elements</param>
let rec genPowerList n m =
    let rec genPowerListHelper num list curPower =
        if curPower = n + m then
            num :: list
        else if curPower < n then   
            genPowerListHelper (num * 2) list (curPower + 1)
        else
            genPowerListHelper (num * 2) (num :: list) (curPower + 1)
    reverseList (genPowerListHelper 2 [] 1)
    
/// <summary>
/// Search for element in the list
/// </summary>
/// <param name="mainList">List to search</param>
/// <param name="elem">Element to search</param>
/// <returns>None if element not found and position otherwise</returns>
let rec findElement mainList elem =
    let rec findElementHelper pos list =
        match list with
        | [] -> None
        | head :: tail ->
            if head = elem then
                Some(pos)
            else
                findElementHelper (pos + 1) tail
    findElementHelper 0 mainList