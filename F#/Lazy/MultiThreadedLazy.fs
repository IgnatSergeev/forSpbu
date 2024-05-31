module Lazy.M

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
