module Lazy.ILazy

type ILazy<'a> =
    abstract member Get: unit -> 'a
