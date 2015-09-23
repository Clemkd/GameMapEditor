using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameMapEditor.Objects.Class
{
    class LimitedStack<T> : LinkedList<T>
    {
        private const int DEFAULT_LIMIT = 20;

        private int limit;

        public LimitedStack(int limit)
        {
            this.limit = limit;
        }

        public void Push(T item)
        {
            this.AddFirst(item);

            if (this.Count > limit)
            {
                this.RemoveLast();
            }
        }

        public T Pop()
        {
            var item = this.First.Value;
            this.RemoveFirst();
            return item;
        }
    }
}
