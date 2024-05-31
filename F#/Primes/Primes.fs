module Primes 

let primes =
    let isPrime n = 
        let sequence = { 2 .. (n - 1) }
        Seq.forall (fun x -> n % x <> 0) sequence

    Seq.initInfinite id |> Seq.filter (fun x -> x >= 2) |> Seq.filter isPrime

