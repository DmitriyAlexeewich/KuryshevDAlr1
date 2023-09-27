using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KuryshevDAlr1.Domain
{
    public class LinkedList<T> where T : struct
    {
        public int Length { get => GetLength(_firstItem, 0); }
        public bool IsEmpty { get => Length == 0; }
        public Item<T> Head { get => _firstItem; }
        public Item<T> End { get => GetLastItem(_firstItem); }

        private Item<T> _firstItem;

        public override string ToString()
        {
            if(_firstItem is null)
                return string.Empty;

            return ToString(_firstItem);
        }

        public void AddToEnd(T value)
        {
            if (_firstItem is null)
            {
                _firstItem = new Item<T>(value);
                return;
            }

            var last = GetLastItem(_firstItem);
            last.Next = new Item<T>(value);
        }

        public void AddToHead(T value)
        {
            if (_firstItem is null)
            {
                _firstItem = new Item<T>(value);
                return;
            }

            var newFirst = new Item<T>(value);
            newFirst.Next = _firstItem;
            _firstItem = newFirst;
        }

        public void RemoveLast()
        {
            if (_firstItem is null)
                return;

            var penultimate = GetPenultimate(_firstItem);
            penultimate.Next = null;
        }

        public void RemoveFirst()
        {
            if (_firstItem is null)
                return;

            if (_firstItem.IsLast)
            {
                _firstItem = null;
                return;
            }

            _firstItem = _firstItem.Next;
        }

        public void AddAt(T value, int index)
        {
            if(index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (index == 0)
            {
                AddToHead(value);
                return;
            }

            if (_firstItem is null)
            {
                _firstItem = new Item<T>(value);
                return;
            }

            var prevItem = GetAt(_firstItem, index - 1);
            var newItem = new Item<T>(value);

            newItem.Next = prevItem.Next;
            prevItem.Next = newItem;
        }

        public Item<T> GetAt(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (_firstItem is null)
                return null;

            return GetAt(_firstItem, index);
        }

        public void RemoveAt(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (index == 0)
            {
                RemoveFirst();
                return;
            }

            if (_firstItem is null)
                return;

            var prevItem = GetAt(_firstItem, index - 1);

            if (prevItem.Next.IsLast)
            {
                RemoveLast();
                return;
            }

            var nextItem = prevItem.Next.Next;
            prevItem.Next = nextItem;
        }

        public void RemoveAll()
        {
            _firstItem = null;
        }

        public void SetAt(T value, int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (_firstItem is null)
            {
                _firstItem = new Item<T>(value);
                return;
            }

            var item = GetAt(_firstItem, index);
            item.Value = value;
        }

        public void Reverse()
        {
            if (_firstItem is null || _firstItem.IsLast)
                return;

            _firstItem = Reverse(_firstItem);
        }

        public void AddRangeAt(int index, LinkedList<T> list)
        {
            if(index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            if(list is null)
                throw new ArgumentNullException(nameof(list));

            if (list.IsEmpty)
                return;

            if (_firstItem is null)
                _firstItem = list.Head;

            if (index == 0)
            {
                return;
            }

            var prevItem = GetAt(_firstItem, index - 1);
            list.End.Next = prevItem.Next;
            prevItem.Next = list.Head;
        }

        public void AddRangeToEnd(LinkedList<T> list)
        {
            if (list is null)
                throw new ArgumentNullException(nameof(list));

            if (list.IsEmpty)
                return;

            if (_firstItem is null)
            {
                _firstItem = list.Head;
                return;
            }

            var last = GetLastItem(_firstItem);
            last.Next = list.Head;
        }

        public void AddRangeToHead(LinkedList<T> list)
        {
            if (list is null)
                throw new ArgumentNullException(nameof(list));

            if (list.IsEmpty)
                return;

            if (_firstItem is null)
            {
                _firstItem = list.Head;
                return;
            }

            list.End.Next = _firstItem;
            _firstItem = list.Head;
        }

        public bool ContainsRange(LinkedList<T> list)
        {
            return GetFirstCoincidence(list) != -1;
        }

        public int GetFirstCoincidence(LinkedList<T> list)
        {
            var coincidence = GetFirstCoincidence(_firstItem, list.Head);

            if (coincidence is null)
                return -1;

            return GetIndex(_firstItem, coincidence);
        }

        public int GetLastCoincidence(LinkedList<T> list)
        {
            Reverse();
            list.Reverse();

            var coincidence = GetFirstCoincidence(_firstItem, list.Head);

            if (coincidence is null)
                return -1;

            Reverse();

            return GetIndex(_firstItem, coincidence);
        }

        public void ReplaceAt(int targedIndex, int replacedIndex)
        {
            if(targedIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(targedIndex));

            if (replacedIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(replacedIndex));

            if (_firstItem is null)
                return;

            if (_firstItem.IsLast)
                return;

            if (targedIndex == replacedIndex)
                return;

            Item<T> target = null;
            Item<T> replaced = null;

            if (targedIndex == 0)
                target = _firstItem;

            if(replacedIndex == 0)
                replaced = _firstItem;

            Item<T> tempItem = _firstItem;
            targedIndex--;
            replacedIndex--;

            for (int i = 0; !tempItem.IsLast || target != null && replaced != null; i++)
            {
                if (target is null && i == targedIndex)
                    target = tempItem;

                if (replaced is null && i == replacedIndex)
                    replaced = tempItem;

                tempItem = tempItem.Next;
            }

            if (target is null || replaced is null)
                return;

            if (target.IsLast || replaced.IsLast)
                return;

            if (targedIndex == 0)
            {
                var second = target.Next;
                target.Next = replaced.Next.Next;
                replaced.Next = target;
                _firstItem = replaced.Next.Next;
                _firstItem.Next = second;
                return;
            }

            if (replacedIndex == 0)
            {
                var second = replaced.Next;
                replaced.Next = target.Next.Next;
                target.Next = replaced;                
                _firstItem = target.Next.Next;
                _firstItem.Next = second;
                return;
            }

            if (targedIndex < replacedIndex)
            {
                var second = target.Next;
                var third = target.Next;

                target.Next = replaced.Next;
                target.Next.Next = replaced.Next.Next;
                replaced.Next = second;
                replaced.Next.Next = third;
                return;
            }
            else
            {
                var second = replaced.Next;
                var third = replaced.Next;

                replaced.Next = target.Next;
                replaced.Next.Next = target.Next.Next;
                target.Next = second;
                target.Next.Next = third;
                return;
            }
        }

        private Item<T> GetLastItem(Item<T> item)
        {
            if(item is null)
                throw new ArgumentNullException(nameof(item));

            if(item.IsLast)
                return item;

            return GetLastItem(item.Next);
        }

        private Item<T> GetPenultimate(Item<T> item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            if (item.Next.IsLast)
                return item;

            return GetPenultimate(item.Next);
        }

        private Item<T> GetAt(Item<T> item, int index)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            if (item.IsLast)
                return item;

            if(index == 0)
                return item;

            index--;

            return GetAt(item.Next, index);
        }

        private int GetLength(Item<T> item, int length)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));

            if (item is null)
                return length;

            length++;

            if(item.IsLast)
                return length;

            return GetLength(item.Next, length);
        }

        private Item<T> Reverse(Item<T> item)
        {
            if(item is null)
                throw new ArgumentNullException(nameof(item));

            var current = item.Next;

            if (!item.Next.IsLast)
                current = Reverse(item.Next);

            current.Next = item;
            item.Next = null;

            return current;
        }

        private Item<T> GetByValue(Item<T> item, T value)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            if(item.Value.Equals(value))
                return item;

            if (item.IsLast)
                return null;

            return GetByValue(item.Next, value);
        }

        private Item<T> GetFirstCoincidence(Item<T> item, Item<T> searchedItem)
        {
            if (item is null)
                return null;

            if (searchedItem is null)
                throw new ArgumentNullException(nameof(searchedItem));

            if (item.IsLast && !searchedItem.IsLast)
                return null;

            var coincidence = GetByValue(item, searchedItem.Value);

            if (coincidence is null)
                return null;

            if(searchedItem.IsLast)
                return coincidence;

            var nextCoincidence = GetFirstCoincidence(coincidence.Next, searchedItem.Next);

            if (nextCoincidence is null)
                return null;

            if (coincidence.Next != nextCoincidence)
                return null;

            return coincidence;
        }

        private int GetIndex(Item<T> head, Item<T> item, int index = 0)
        {
            if (head is null)
                throw new ArgumentNullException(nameof(head));

            if (item is null)
                throw new ArgumentNullException(nameof(item));

            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (head == item)
                return index;

            if (head.IsLast)
                return -1;

            index++;

            return GetIndex(head.Next, item, index);
        }

        private string ToString(Item<T> item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            if (item.IsLast)
                return item.Value.ToString();

            return $"{item.Value.ToString()} {ToString(item.Next)}";
        }
    }
}
