using System;
using System.Collections.Generic;
using System.Text;

namespace CustomStack
{
    public class StackOfStrings : Stack<string>
    {
        public bool IsEmpty()
        {
            return this.Count == 0 ? true : false;
        }

        public Stack<string> AddRange(IEnumerable<string> elements)
        {
            foreach (string element in elements)
            {
                this.Push(element);
            }
            return this;
        }
    }
}
