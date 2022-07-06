using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace lab_work13
{

    public class NodeList<T>
    {
        public T Data { get; set; }
        public NodeList<T> Next { get; set; }

        public NodeList()
        {
            Data = default(T);
            Next = null;
        }
        public NodeList(T data)
        {
            Data = data;
            Next = null;
        }

        public NodeList(T data, NodeList<T> next)
        {
            this.Data = data;
            this.Next = next;
        }
    }

    class CircleList<T> : IEnumerable<T>
    {
        NodeList<T> head; // головной/первый элемент
        NodeList<T> tail; // последний/хвостовой элемент
        int count;  // количество элементов в списке

        public int Count
        {
            get { return count; }
            set
            {
                if (value >= 0)
                {
                    count = value;
                }
                else
                    throw new ArgumentOutOfRangeException();
            }
        }
        public CircleList()
        {
            head = null;
            tail = null;
            Count = 0;
        }

        public CircleList(int capacity) : this()
        {
            NodeList<T> beg = new NodeList<T>();
            head = beg;
            for (int i = 1; i < capacity; ++i)
            {
                NodeList<T> p = new NodeList<T>();
                p.Next = beg;
                beg = p;
            }
            tail = head;
        }

        public CircleList(CircleList<T> list) : this()
        {
            foreach (T item in list)
                Add(item);
        }

        public T this[int index]
        {
            get
            {
                if (index < 0)
                    throw new ArgumentOutOfRangeException();
                NodeList<T> currentNode = head;
                for (int i = 0; i < index; i++)
                {
                    if (currentNode.Next == null)
                        throw new ArgumentOutOfRangeException();
                    currentNode = currentNode.Next;
                }
                return currentNode.Data;
            }
            set
            {
                if(index < 0 && index >= count)
                    throw new ArgumentOutOfRangeException();
                NodeList<T> currentNode = head;
                for (int i = 0; i < index; i++)
                {
                    if (currentNode.Next == null)
                        throw new ArgumentOutOfRangeException();
                    currentNode = currentNode.Next;
                }
                currentNode.Data = value;
            }
        }

        
        public void Add(T data)
        {
            NodeList<T> node = new NodeList<T>(data);
            if (head == null)
            {
                head = node;
                tail = node;
                tail.Next = head;
            }
            else
            {
                node.Next = head;
                tail.Next = node;
                tail = node;
            }
            count++;
        }

        public void InsertAt(int index, T item)
        {
            if (index > count || index < 0)
                throw new ArgumentOutOfRangeException();
            if (index == 0)
            {
                if (IsEmpty)
                    head = tail = new NodeList<T>(item);
                else
                    head = new NodeList<T>(item, head);
                count++;
            }
            else if (index == (count - 1))
            {
                if (IsEmpty)
                    head = tail = new NodeList<T>(item);
                else
                    tail = tail.Next = new NodeList<T>(item);
                count++;
            }
            else
            {
                NodeList<T> currentNode = head;
                for (int i = 0; i < index - 1; i++)
                {
                    currentNode = currentNode.Next;
                }
                NodeList<T> newNode = new NodeList<T>(item, currentNode.Next);
                currentNode.Next = newNode;
                count++;
            }
        }

        public bool Remove(T data)
        {
            NodeList<T> current = head;
            NodeList<T> previous = null;

            if (IsEmpty) return false;

            do
            {
                if (current.Data.Equals(data))
                {
                    if (previous != null)
                    {
                        previous.Next = current.Next;

                        if (current == tail)
                            tail = previous;
                    }
                    else
                    {
                        if (count == 1)
                        {
                            head = tail = null;
                        }
                        else
                        {
                            head = current.Next;
                            tail.Next = current.Next;
                        }
                    }
                    count--;
                    return true;
                }

                previous = current;
                current = current.Next;
            } while (current != head);

            return false;
        }

        public object RemoveAt(int index)
        {
            if (index > count || index < 0)
                throw new ArgumentOutOfRangeException();
            object removedData;
            if (index == 0)
            {
                if (IsEmpty)
                    throw new ApplicationException("Список пуст");
                removedData = head.Data;
                if (head == tail)
                    head = tail = null;
                else
                    head = tail.Next;
            }
            else if (index == (count - 1))
            {
                if (IsEmpty)
                    throw new ApplicationException("Список пуст");
                removedData = tail.Data;
                if (head == tail)
                    head = tail = null;
                else
                {
                    NodeList<T> currentNode = head;
                    while (currentNode.Next != tail)
                        currentNode = currentNode.Next;
                    tail = currentNode;
                    currentNode.Next = head;
                }
            }
            else
            {
                NodeList<T> currentNode = head;
                for (int i = 0; i < index; i++)
                {
                    currentNode = currentNode.Next;
                }
                removedData = currentNode.Data;
                currentNode.Next = currentNode.Next.Next;
            }
            count--;
            return removedData;
        }

        public bool IsEmpty { get { return count == 0; } }

        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }

        public bool Contains(T data)
        {
            NodeList<T> current = head;
            if (current == null) return false;
            do
            {
                if (current.Data.Equals(data))
                    return true;
                current = current.Next;
            }
            while (current != head);
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            NodeList<T> current = head;
            do
            {
                if (current != null)
                {
                    yield return current.Data;
                    current = current.Next;
                }
            }
            while (current != head);
        }

        public override string ToString()
        {
            string result = "";
            foreach (T item in this)
                result += item + " ";
            return result;
        }
    }
}


/*
 классу-издателю, генерирующему событие не нужно знать, скольок классов-подписчиков подпишется или отпишется. Он создал
событие для определенных методов, ограничив их делегатом по определенной сигнатуре

1.Определение уловий возникновения события и методы, которые должны сработать 
2.Определение сигнатуры этих мтеодов и создайте делегат на основе этой сигнатуры
3. создайте общедоступное событие на основе этого делегата и вызовите, когда событие сработает
4.Обязательно подпишитесь на это событие теми методами, которые должны сработать и сигнатуры которых подходят к делегату

Событие - это ситуация, при возникновении которой произойдет действие или несколько действие ИЛИ
        - именованный делегат, при вызове которого будут запущены все подписавшиеся на момент вызова события методы заданной сигнатуры

 select, selectMany
 */