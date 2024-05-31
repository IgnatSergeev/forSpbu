module Lazy.ILazy

/// <summary>
/// Interface for lazy function calculation
/// </summary>
/// <typeparam name="'a">Function return value</typeparam>
type ILazy<'a> =
    /// <summary>
    /// Lazily calculates given function
    /// </summary>
    /// <returns>Function result</returns>
    abstract member Get: unit -> 'a
