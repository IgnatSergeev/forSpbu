
module FibonacciSum

/// Calculates sum of even fibonacci numbers which are less than 1000000
let fibonacciEvenSum() =
    let rec fibonacciEvenSumHelper acc prev prevPrev =
        let curr = prevPrev + prev
        if curr <= 1000000 then
            if curr % 2 = 0 then
                fibonacciEvenSumHelper (acc + curr) curr prev
            else
                fibonacciEvenSumHelper acc curr prev
        else
            acc
    
    fibonacciEvenSumHelper 0 1 1

printfn "%d" <| fibonacciEvenSum()