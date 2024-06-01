module Workflows

/// <summary>
/// Rounding to a precision workflow builder
/// </summary>
/// <param name="prec"> Workflow precision </param>
type RoundingBuilder(prec: int) = 
    member this.Bind(x: float, f) =
        f (System.Math.Round(x, prec))
    member this.Return(x: float) = System.Math.Round(x, prec)

/// <summary>
/// Builds rounding to a precision workflow 
/// </summary>
let rounding = RoundingBuilder

/// <summary>
/// String calculation workflow builder
/// </summary>
type StringCalculateBuilder() = 
    member this.Bind(x: string, f) =
        let res = 0
        let parsed = System.Int32.TryParse(x, ref res)
        if parsed then Some(res) else None
    member this.Return(x: int) = Some(x)


/// <summary>
/// Build string calculation workflow 
/// </summary>
let calculate = StringCalculateBuilder()
