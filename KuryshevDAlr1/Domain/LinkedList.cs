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

        /// <summary>
        /// Переопределение перобразования списка в строку
        /// </summary>
        public override string ToString()
        {
            if(_firstItem is null)
                return string.Empty;

            return ToString(_firstItem);
        }

        /// <summary>
        /// Добавляет элемент в конец списка
        /// </summary>
        /// <param name="value">Значение</param>
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

        /// <summary>
        /// Добавляет элемент в начало списка
        /// </summary>
        /// <param name="value">Значение</param>
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

        /// <summary>
        /// Удаляет элемент в конце списка
        /// </summary>
        public void RemoveLast()
        {
            if (_firstItem is null)
                return;

            var penultimate = GetPenultimate(_firstItem);
            penultimate.Next = null;
        }

        /// <summary>
        /// Удаляет элемент в начале списка
        /// </summary>
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

        /// <summary>
        /// Добавляет элемент по индексу
        /// </summary>
        /// <param name="value">Значение</param>
        /// <param name="index">Индекс</param>
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

        /// <summary>
        /// Возвращает элемент по индексу
        /// </summary>
        /// <param name="index">Индекс</param>
        public Item<T> GetAt(int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (_firstItem is null)
                return null;

            return GetAt(_firstItem, index);
        }

        /// <summary>
        /// Удаляет элемент по индексу
        /// </summary>
        /// <param name="index">Индекс</param>
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

        /// <summary>
        /// Удаляет все элементы
        /// </summary>
        public void RemoveAll()
        {
            _firstItem = null;
        }

        /// <summary>
        /// Заменяет элемент по индексу на передаваемый элемент
        /// </summary>
        /// <param name="value">Значение</param>
        /// <param name="index">Индекс</param>
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

        /// <summary>
        /// Меняет порядок элементов в списке на обратный
        /// </summary>
        public void Reverse()
        {
            if (_firstItem is null || _firstItem.IsLast)
                return;

            var (head, end) = Reverse(_firstItem);
            _firstItem = head;
        }

        /// <summary>
        /// Добавляет элементы по индексу
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <param name="list">Вставляемый список</param>
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

        /// <summary>
        /// Добавляет элементы в конец
        /// </summary>
        /// <param name="list">Вставляемый список</param>
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

        /// <summary>
        /// Добавляет элементы в начало
        /// </summary>
        /// <param name="list">Вставляемый список</param>
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

        /// <summary>
        /// Проверяет на наличие списка в списке
        /// </summary>
        /// <param name="list">Список</param>
        public bool ContainsRange(LinkedList<T> list)
        {
            return GetFirstCoincidence(list) != -1;
        }

        /// <summary>
        /// Возвращает индекс первого вхождения списка в список
        /// </summary>
        /// <param name="list">Список</param>
        public int GetFirstCoincidence(LinkedList<T> list)
        {
            var coincidence = GetFirstCoincidence(_firstItem, list.Head);

            if (coincidence is null)
                return -1;

            return GetIndex(_firstItem, coincidence);
        }

        /// <summary>
        /// Возвращает индекс последнего вхождения списка в список
        /// </summary>
        /// <param name="list">Список</param>
        public int GetLastCoincidence(LinkedList<T> list)
        {
            Reverse();
            list.Reverse();

            var coincidence = GetFirstCoincidence(_firstItem, list.Head);

            if (coincidence is null)
                return -1;

            Reverse();

            return GetIndex(_firstItem, coincidence) - list.Length + 1;
        }

        /// <summary>
        /// Меняет элементы местами
        /// </summary>
        /// <param name="targedIndex">Индекс ориинального элемента</param>
        /// <param name="replacedIndex">Индекс заменяемого элемента</param>
        public void ReplaceAt(int targedIndex, int replacedIndex)
        {
            if (targedIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(targedIndex));

            if (replacedIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(replacedIndex));

            if (_firstItem is null)
                return;

            if (_firstItem.IsLast)
                return;

            if (targedIndex == replacedIndex)
                return;

            Item<T> current = _firstItem;
            Item<T> target = null;
            Item<T> replaced = null;

            for (int i = 0; target is null && replaced is null || !current.IsLast; i++)
            {
                if (i == replacedIndex)
                    replaced = current;

                if(i == targedIndex)
                    target = current;

                current = current.Next;
            }

            if (target is null || replaced is null)
                return;

            var targetValue = target.Value;
            target.Value = replaced.Value;
            replaced.Value = targetValue;
        }

        /// <summary>
        /// Возвращает последний элемент в последовательности
        /// </summary>
        /// <param name="item">Элемент последовательности</param>
        private Item<T> GetLastItem(Item<T> item)
        {
            if(item is null)
                throw new ArgumentNullException(nameof(item));

            if(item.IsLast)
                return item;

            return GetLastItem(item.Next);
        }

        /// <summary>
        /// Возвращает предпоследний элемент в последовательности
        /// </summary>
        /// <param name="item">Элемент последовательности</param>
        private Item<T> GetPenultimate(Item<T> item)
        {
            if (item is null)
                throw new ArgumentNullException(nameof(item));

            if (item.Next.IsLast)
                return item;

            return GetPenultimate(item.Next);
        }

        /// <summary>
        /// Возвращает элемент в последовательности по индексу
        /// </summary>
        /// <param name="item">Элемент последовательности</param>
        /// <param name="index">Индекс</param>
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

        /// <summary>
        /// Возвращает длину последовательности
        /// </summary>
        /// <param name="item">Элемент последовательности</param>
        /// <param name="length">Длина</param>
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

        /// <summary>
        /// Меняет порядок последовательности
        /// </summary>
        /// <param name="item">Элемент последовательности</param>
        private (Item<T>, Item<T>) Reverse(Item<T> item)
        {
            if(item is null)
                throw new ArgumentNullException(nameof(item));

            if (item.Next.IsLast)
            {
                var reverseHead = item.Next;
                reverseHead.Next = item;
                item.Next = null;
                return (reverseHead, item);
            }

            var (head, end) = Reverse(item.Next);
            end.Next = item;
            item.Next = null;

            return (head, item);
        }

        /// <summary>
        /// Возвращает первое совпадение по значению в последовательности
        /// </summary>
        /// <param name="item">Элемент последовательности</param>
        /// <param name="value">Значение</param>
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

        /// <summary>
        /// Возвращает первое совпадение по элементу в последовательности
        /// </summary>
        /// <param name="item">Элемент последовательности</param>
        /// <param name="searchedItem">Искомый элемент</param>
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

        /// <summary>
        /// Возвращает индекс элемента последовательности
        /// </summary>
        /// <param name="head">Начальный элемент последовательности</param>
        /// <param name="item">Искомый элемент последовательности</param>
        /// <param name="index">Индекс</param>
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

        /// <summary>
        /// Преобразование последовательности в строку
        /// </summary>
        /// <param name="item">Элемент последовательности</param>
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
