﻿using System;
using System.Collections.Generic;

public class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> heap = new List<T>();

    public int Count { get { return heap.Count; } }

    public void Enqueue(T item)
    {
        heap.Add(item);
        int i = heap.Count - 1;
        while (i > 0)
        {
            int parent = (i - 1) / 2;
            if (heap[i].CompareTo(heap[parent]) >= 0)
                break;

            Swap(i, parent);
            i = parent;
        }
    }

    public T Dequeue()
    {
        if (heap.Count == 0)
            throw new InvalidOperationException("Queue is empty");

        T result = heap[0];
        int last = heap.Count - 1;
        heap[0] = heap[last];
        heap.RemoveAt(last);

        int i = 0;
        while (true)
        {
            int left = i * 2 + 1;
            if (left >= heap.Count)
                break;

            int right = left + 1;
            int smallest = (right < heap.Count && heap[right].CompareTo(heap[left]) < 0) ? right : left;

            if (heap[smallest].CompareTo(heap[i]) >= 0)
                break;

            Swap(i, smallest);
            i = smallest;
        }

        return result;
    }

    public bool Contains(T item)
    {
        return heap.Contains(item);
    }

    private void Swap(int i, int j)
    {
        T temp = heap[i];
        heap[i] = heap[j];
        heap[j] = temp;
    }
}
