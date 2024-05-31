module Primes 

/// <summary>
/// Generates infinite sequence of prime numbers
/// </summary>
let primes =
    let isPrime n = 
        let sequence = { 2 .. (n |> float |> sqrt |> int) }
        Seq.forall (fun x -> n % x <> 0) sequence

    Seq.initInfinite id |> Seq.filter (fun x -> x >= 2) |> Seq.filter isPrime

