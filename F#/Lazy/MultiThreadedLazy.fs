module Lazy.M

/// <summary>
/// Multi threaded implementation of iLazy interface
/// </summary>
/// <typeparam name="'a">Result type</typeparam>
type Lazy<'a>(func)  = 
    let mutable value : Option<'a> = None
    let valueLock = obj
    interface ILazy.ILazy<'a> with
        member this.Get() =
            if value.IsNone then
                lock valueLock (fun () ->
                    if value.IsNone then
                        value <- Some(func())
                )
            value.Value
