module PriorityQueue

/// <summary>
/// Exception to be thrown when attempted to dequeue empty priority queue
/// </summary>
exception PriorityQueueEmptyException

type private Node<'T> = { Priority : int; Value : 'T } 

/// <summary>
/// Queue with priority of elements
/// </summary>
/// <typeparam name="'T">The type of queue elements</typeparam>
type PriorityQueue<'T> () =
    let mutable list = List<Node<'T>>.Empty
    member private this.EnqueueHelper (value : Node<'T>, curList : List<Node<'T>>) =
        match curList with
        | head::tail ->
            if head.Priority >= value.Priority then
                head::this.EnqueueHelper(value, tail)
            else
                value::head::tail
        | [] -> [ value ]
    
    /// <summary>
    /// Adds element with given value and priority to the queue
    /// </summary>
    /// <param name="value">Value to add to the queue</param>
    /// <param name="priority">Priority of given value in queue</param>
    member this.Enqueue (value : 'T, priority :int) =
        list <- this.EnqueueHelper({ Value = value; Priority = priority }, list)
    
    /// <summary>
    /// Removes first element of the queue
    /// </summary>
    /// <returns>First element value</returns>
    member this.Dequeue =
        match list with
        | head::tail ->
            list <- tail
            head.Value
        | [] -> raise PriorityQueueEmptyException
    