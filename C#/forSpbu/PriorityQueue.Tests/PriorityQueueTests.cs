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
        Assert.That(priorityQueue.Empty(), Is.False);
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