// Copyright 2023 Ignatii Sergeev.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// https://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.


namespace PriorityQueue.Tests;

public class PriorityQueueTests
{
    [Test]
    public void EmptyQueueIsEmptyShouldReturnTrue()
    {
        var priorityQueue = new PriorityQueue<int>();
        Assert.That(priorityQueue.Empty);
    }
    
    [Test]
    public void NotEmptyQueueIsEmptyShouldReturnFalse()
    {
        var priorityQueue = new PriorityQueue<int>();
        priorityQueue.Enqueue(20, 5);
        Assert.That(priorityQueue.Empty, Is.False);
    }
    
    [Test]
    public void QueueEnqueueDequeueIsEmptyShouldReturnTrue()
    {
        var priorityQueue = new PriorityQueue<int>();
        priorityQueue.Enqueue(20, 5);
        priorityQueue.Dequeue();
        Assert.That(priorityQueue.Empty);
    }
    
    [Test]
    public void EmptyQueueDequeueShouldThrowEmptyQueueException()
    {
        var priorityQueue = new PriorityQueue<int>();
        Assert.Throws<EmptyQueueException>(() => priorityQueue.Dequeue());
    }
    
    [Test]
    public void QueueEnqueueDequeueDequeueShouldThrowEmptyQueueException()
    {
        var priorityQueue = new PriorityQueue<int>();
        priorityQueue.Enqueue(20, 5);
        priorityQueue.Dequeue();
        Assert.Throws<EmptyQueueException>(() => priorityQueue.Dequeue());
    }
    
    [Test]
    public void QueueEnqueueDequeueShouldReturnTheSameValue()
    {
        var priorityQueue = new PriorityQueue<int>();
        priorityQueue.Enqueue(20, 5);
        Assert.That(priorityQueue.Dequeue(), Is.EqualTo(20));
    }
    
    [Test]
    public void Queue2xEnqueueDequeueShouldReturnTheValueWithBiggestPriority()
    {
        var priorityQueue = new PriorityQueue<int>();
        priorityQueue.Enqueue(2, 6);
        priorityQueue.Enqueue(20, 5);
        Assert.That(priorityQueue.Dequeue(), Is.EqualTo(2));
    }
    
    [Test]
    public void Queue2xEnqueueOfSamePriorityDequeueShouldReturnTheFirstAddedValue()
    {
        var priorityQueue = new PriorityQueue<int>();
        priorityQueue.Enqueue(20, 5);
        priorityQueue.Enqueue(2, 5);
        Assert.That(priorityQueue.Dequeue(), Is.EqualTo(20));
    }
    
    [Test]
    public void QueueEnqueueDequeueEnqueueSameValueDequeueShouldReturnTheValue()
    {
        var priorityQueue = new PriorityQueue<int>();
        priorityQueue.Enqueue(3, 6);
        priorityQueue.Dequeue();
        priorityQueue.Enqueue(3, 6);
        Assert.That(priorityQueue.Dequeue(), Is.EqualTo(3));
    }
}