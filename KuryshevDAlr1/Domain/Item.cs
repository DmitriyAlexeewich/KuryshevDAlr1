namespace KuryshevDAlr1.Domain
{
    /// <summary>
    /// Элемент односвязного списка
    /// </summary>
    /// <typeparam name="T">Тип данных</typeparam>
    public class Item<T>
    {
        /// <summary>
        /// Значение
        /// </summary>
        public T Value { get; set; }

        /// <summary>
        /// Ссылка на следующий элемент
        /// </summary>
        public Item<T> Next { get; set; }

        /// <summary>
        /// Флаг - последний элемент
        /// </summary>
        public bool IsLast { get { return Next is null; } }

        /// <summary>
        /// Конструктор элемент
        /// </summary>
        /// <param name="value">Значение элемента</param>
        public Item(T value)
        {
            if(value is null)
                throw new ArgumentNullException(nameof(value));

            Value = value;
        }

        /// <summary>
        /// Преобразование последовательности в строку
        /// </summary>
        public override string ToString()
        {
            if (Value is null)
                return "null";

            return Value.ToString();
        }
    }
}
