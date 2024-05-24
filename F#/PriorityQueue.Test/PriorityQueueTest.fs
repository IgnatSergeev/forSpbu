module PriorityQueue.Test

open NUnit.Framework

let QueuesWithTwoValue =
    [
            TestCaseData(PriorityQueue<string>(), "asd", "bsd")
            TestCaseData(PriorityQueue<int>(), 1, 2)
            TestCaseData(PriorityQueue<float>(), 1.0, 2.0)
    ]

[<TestCaseSource("QueuesWithTwoValue")>]
let TestEnqueueDequeue (queue : PriorityQueue<'T>, fstValue : 'T, _ : 'T) =
    queue.Enqueue(fstValue, 1)
    Assert.That(queue.Dequeue, Is.EqualTo fstValue)
    
[<TestCaseSource("QueuesWithTwoValue")>]
let TestEmptyDequeue (queue : PriorityQueue<'T>, _ : 'T, _ : 'T) =
    Assert.Throws<PriorityQueueEmptyException>(fun () -> queue.Dequeue |> ignore) |> ignore
    
[<TestCaseSource("QueuesWithTwoValue")>]
let TestEnqueueEqualPrioritiesDequeue (queue : PriorityQueue<'T>, fstValue : 'T, secValue : 'T) =
    queue.Enqueue(fstValue, 1)
    queue.Enqueue(secValue, 1)
    Assert.That(queue.Dequeue, Is.EqualTo fstValue)
    
[<TestCaseSource("QueuesWithTwoValue")>]
let TestEnqueueGreaterPriorityDequeue (queue : PriorityQueue<'T>, fstValue : 'T, secValue : 'T) =
    queue.Enqueue(fstValue, 1)
    queue.Enqueue(secValue, 2)
    Assert.That(queue.Dequeue, Is.EqualTo secValue)
    
    