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


namespace PriorityQueue;

/// <summary>
/// Priority first FIFO container 
/// </summary>
/// <typeparam name="T">Element type</typeparam>
public class PriorityQueue<T>
{
    /// <summary>
    /// Adds an element with given value and priority to the queue
    /// </summary>
    /// <param name="value">Element value</param>
    /// <param name="priority">Element priority</param>
    public void Enqueue(T value, int priority)
    {
        if (_priorityDictionary.ContainsKey(priority))
        {
            _priorityDictionary[priority].Enqueue(value);
        }
        else
        {
            _priorities.Add(priority, priority);
            _priorityDictionary.Add(priority, new Queue<T>());
            _priorityDictionary[priority].Enqueue(value);
        }
    }

    /// <summary>
    /// Removes an element with highest priority (if there are several of them, removes first added)
    /// </summary>
    /// <returns>Remove element value</returns>
    /// <exception cref="EmptyQueueException">If trying to dequeue from empty queue</exception>
    public T Dequeue()
    {
        if (Empty)
        {
            throw new EmptyQueueException();
        }

        var maxPriority = _priorities.GetValueAtIndex(_priorities.Count - 1);
        var returnValue = _priorityDictionary[maxPriority].Dequeue();
        if (_priorityDictionary[maxPriority].Count == 0)
        {
            _priorities.RemoveAt(_priorities.Count - 1);
            _priorityDictionary.Remove(maxPriority);
        }

        return returnValue;
    }

    /// <summary>
    /// Stores if the priority queue is empty
    /// </summary>
    public bool Empty => _priorities.Count == 0;
    private readonly Dictionary<int, Queue<T>> _priorityDictionary = new();
    private readonly SortedList<int, int> _priorities = new();
}