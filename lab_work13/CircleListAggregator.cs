using System;
using System.Collections.Generic;
using System.Text;

namespace lab_work13
{
    class CircleListAggregator<T>
    {
        protected CircleList<T> CList;

        protected string Name { get; }

        public bool IsNull => CList == null;

        public int Count => CList.Count;

        public CircleListAggregator(string name)
        {
            Name = name;
            CList = new CircleList<T>();
        }

        public virtual T this[int index]
        {
            get => CList[index];
            set => CList[index] = value;
        }

        public virtual void Add(T item)
        {
            CList.Add(item);
        }

        public virtual void InsertAt(int index, T item)
        {
            CList.InsertAt(index, item);
        }

        public virtual bool Remove(T data)
        {
            if (CList.IsEmpty) return false;
            bool ok = CList.Remove(data);
            return ok;
        }

        public virtual bool RemoveAt(int index)
        {
            bool result = index >= 0 && index < Count;
            if (result)
                CList.RemoveAt(index);
            return result;
        }

        public virtual void Clear()
        {
            CList.Clear();
        }

        public override string ToString()
        {
            string result = "";
            foreach (T item in CList)
                result += item + "\n";
            return result;
        }
    }
}
