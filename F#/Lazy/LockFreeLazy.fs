module Lazy.LF

type Lazy<'a>(func)  = 
    let mutable value : Option<'a> = None
    interface ILazy.ILazy<'a> with
        member this.Get() =
            if value.IsNone then
                let localValue = func()
                System.Threading.Interlocked.CompareExchange(&value, Some(localValue), None) |> ignore
            value.Value
