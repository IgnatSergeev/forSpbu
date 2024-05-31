module Lazy.LF

/// <summary>
/// Lock free multi threaded implementation of iLazy interface
/// </summary>
/// <typeparam name="'a">Result type</typeparam>
type Lazy<'a>(func)  = 
    let mutable value : Option<'a> = None
    interface ILazy.ILazy<'a> with
        member this.Get() =
            if value.IsNone then
                let localValue = func()
                System.Threading.Interlocked.CompareExchange(&value, Some(localValue), None) |> ignore
            value.Value
