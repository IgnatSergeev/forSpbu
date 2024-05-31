module Lazy.S

type Lazy<'a>(f)  = 
    let mutable value : Option<'a> = None
    interface ILazy.ILazy<'a> with
        member this.Get() =
            if value.IsNone then
                value <- Some(f())
            value.Value
