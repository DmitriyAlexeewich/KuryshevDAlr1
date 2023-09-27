using LinkedList = KuryshevDAlr1.Domain.LinkedList<int>;

string[] commands = new string[]
{
    "1. добавление в конец списка",
    "2. добавление в начало списка",
    "3. удаление последнего элемента",
    "4. удаление первого элемента",
    "5. добавление элемента по индексу (вставка перед элементом, который был ранее доступен по этому индексу)",
    "6. получение элемента по индексу",
    "7. удаление элемента по индексу",
    "8. получение размера списка",
    "9. удаление всех элементов списка",
    "10. замена элемента по индексу на передаваемый элемент",
    "11. проверка на пустоту списка",
    "12. меняет порядок элементов в списке на обратный",
    "13. вставка другого списка в список, начиная с индекса",
    "14. вставка другого списка в конец",
    "15. вставка другого списка в начало",
    "16. проверка на содержание другого списка в списке",
    "17. поиск первого вхождения другого списка в список",
    "18. поиск последнего вхождения другого списка в список",
    "19. обмен двух элементов списка по индексам",
    "20. выход"
};
var list = new LinkedList();
var command = -1;

while (command != 20)
{
    Console.WriteLine($"List: {list.ToString()}");

    foreach (var cmd in commands)
        Console.WriteLine(cmd);

    Console.WriteLine("Command: ");

    if (!int.TryParse(Console.ReadLine(), out command))
        break;

    switch (command)
    {
        case 1:
            var addToEndValue = 0;

            Console.WriteLine("Value: ");

            if (!int.TryParse(Console.ReadLine(), out addToEndValue))
                break;

            list.AddToEnd(addToEndValue);
            break;
        case 2:
            var addToHeadValue = 0;

            Console.WriteLine("Value: ");

            if (!int.TryParse(Console.ReadLine(), out addToHeadValue))
                break;

            list.AddToHead(addToHeadValue);
            break;
        case 3:
            list.RemoveLast();
            break;
        case 4:
            list.RemoveFirst();
            break;
        case 5:
            var addAtValue = 0;
            var addAt = 0;

            Console.WriteLine("Value: ");

            if (!int.TryParse(Console.ReadLine(), out addAtValue))
                break;

            Console.WriteLine("Index: ");

            if (!int.TryParse(Console.ReadLine(), out addAt))
                break;

            list.AddAt(addAtValue, addAt);
            break;
        case 6:
            var getAt = 0;

            Console.WriteLine("Index: ");

            if (!int.TryParse(Console.ReadLine(), out getAt))
                break;

            Console.WriteLine(list.GetAt(getAt));
            break;
        case 7:
            var removeAt = 0;

            Console.WriteLine("Index: ");

            if (!int.TryParse(Console.ReadLine(), out removeAt))
                break;

            list.RemoveAt(removeAt);
            break;
        case 8:
            Console.WriteLine(list.Length);
            break;
        case 9:
            list.RemoveAll();
            break;
        case 10:
            var setAtValue = 0;
            var setAt = 0;

            Console.WriteLine("Value: ");

            if (!int.TryParse(Console.ReadLine(), out setAtValue))
                break;

            Console.WriteLine("Index: ");

            if (!int.TryParse(Console.ReadLine(), out setAt))
                break;

            list.SetAt(setAtValue, setAt);
            break;
        case 11:
            Console.WriteLine(list.IsEmpty);
            break;
        case 12:
            list.Reverse();
            break;
        case 13:
            var tempList = new LinkedList();
            var maxSize = 0;
            var addRangeIndex = 0;

            Console.WriteLine("Max temp list size: ");

            if (!int.TryParse(Console.ReadLine(), out maxSize))
                break;

            for (int i = 0; i < maxSize; i++)
            {
                var tempValue = 0;

                Console.WriteLine("Value: ");

                if (!int.TryParse(Console.ReadLine(), out tempValue))
                    break;

                tempList.AddToEnd(tempValue);
            }

            Console.WriteLine("Index: ");

            if (!int.TryParse(Console.ReadLine(), out addRangeIndex))
                break;

            list.AddRangeAt(addRangeIndex, tempList);
            break;
        case 14:
            var tempListAddRange = new LinkedList();
            var maxSizeAddRange = 0;
            Console.WriteLine("Max temp list size: ");

            if (!int.TryParse(Console.ReadLine(), out maxSizeAddRange))
                break;

            for (int i = 0; i < maxSizeAddRange; i++)
            {
                var tempValue = 0;

                Console.WriteLine("Value: ");

                if (!int.TryParse(Console.ReadLine(), out tempValue))
                    break;

                tempListAddRange.AddToEnd(tempValue);
            }

            list.AddRangeToEnd(tempListAddRange);
            break;
        case 15:
            var tempListAddRangeHead = new LinkedList();
            var maxSizeAddRangeHead = 0;
            Console.WriteLine("Max temp list size: ");

            if (!int.TryParse(Console.ReadLine(), out maxSizeAddRangeHead))
                break;

            for (int i = 0; i < maxSizeAddRangeHead; i++)
            {
                var tempValue = 0;

                Console.WriteLine("Value: ");

                if (!int.TryParse(Console.ReadLine(), out tempValue))
                    break;

                tempListAddRangeHead.AddToEnd(tempValue);
            }

            list.AddRangeToHead(tempListAddRangeHead);
            break;
        case 16:
            var tempListContain = new LinkedList();
            var maxSizeContain = 0;
            Console.WriteLine("Max temp list size: ");

            if (!int.TryParse(Console.ReadLine(), out maxSizeContain))
                break;

            for (int i = 0; i < maxSizeContain; i++)
            {
                var tempValue = 0;

                Console.WriteLine("Value: ");

                if (!int.TryParse(Console.ReadLine(), out tempValue))
                    break;

                tempListContain.AddToEnd(tempValue);
            }

            Console.WriteLine(list.ContainsRange(tempListContain));
            break;
        case 17:
            var tempListContainFirst = new LinkedList();
            var maxSizeContainFirst = 0;
            Console.WriteLine("Max temp list size: ");

            if (!int.TryParse(Console.ReadLine(), out maxSizeContainFirst))
                break;

            for (int i = 0; i < maxSizeContainFirst; i++)
            {
                var tempValue = 0;

                Console.WriteLine("Value: ");

                if (!int.TryParse(Console.ReadLine(), out tempValue))
                    break;

                tempListContainFirst.AddToEnd(tempValue);
            }

            Console.WriteLine(list.GetFirstCoincidence(tempListContainFirst));
            break;
        case 18:
            var tempListContainLast = new LinkedList();
            var maxSizeContainLast = 0;
            Console.WriteLine("Max temp list size: ");

            if (!int.TryParse(Console.ReadLine(), out maxSizeContainLast))
                break;

            for (int i = 0; i < maxSizeContainLast; i++)
            {
                var tempValue = 0;

                Console.WriteLine("Value: ");

                if (!int.TryParse(Console.ReadLine(), out tempValue))
                    break;

                tempListContainLast.AddToEnd(tempValue);
            }

            Console.WriteLine(list.GetLastCoincidence(tempListContainLast));
            break;
        case 19:
            var targedIndex = 0;
            var replacedIndex = 0;
            Console.WriteLine("Target index: ");

            if (!int.TryParse(Console.ReadLine(), out targedIndex))
                break;

            Console.WriteLine("Replaced index: ");

            if (!int.TryParse(Console.ReadLine(), out replacedIndex))
                break;

            list.ReplaceAt(targedIndex, replacedIndex);
            break;
        default:
            break;
    }

    Console.ReadKey();
    Console.Clear();
}