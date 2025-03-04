namespace VoteAnalyzingSystem
{
    
    public class CustomLinkedList<T>
    {
        public Node<T> Head { get; private set; }
        public Node<T> Tail { get; private set; }
        public int Count { get; private set; }

        public CustomLinkedList()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        public void AddLast(T value)
        {
            var newNode = new Node<T>(value);
            if (Head == null)
            {
                Head = newNode;
                Tail = newNode;
            }
            else
            {
                Tail.Next = newNode;
                Tail = newNode;
            }
            Count++;
        }

        public void Remove(Node<T> node)
        {
            if (node == Head)
            {
                Head = Head.Next;
                if (Head == null)
                {
                    Tail = null;
                }
            }
            else
            {
                var current = Head;
                while (current.Next != node)
                {
                    current = current.Next;
                }
                current.Next = node.Next;
                if (node == Tail)
                {
                    Tail = current;
                }
            }
            Count--;
        }

        public void Clear()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }
    }
}