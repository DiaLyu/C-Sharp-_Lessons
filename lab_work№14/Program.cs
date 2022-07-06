using System;
using System.Linq;
using labwork10;
using System.Collections.Generic;

namespace lab_work_14
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Коллекция автоматически сформирована\n");
            TestCollections collection = new TestCollections();
            int count = -1;
            bool mLinq = true;
            do
            {
                Console.WriteLine("\n1. Добавить элемент");
                Console.WriteLine("2. Удалить элемент");
                Console.WriteLine("3.Изменить тип запросов(сейчас " + (mLinq ? "LINQ" : "Методы расширения") + ")");
                Console.WriteLine("  Запросы");
                Console.WriteLine("  4. Группировка стран по сроку правления президента");
                Console.WriteLine("  5. Количество стран, в которых площадь больше 500000 км^2");
                Console.WriteLine("  6. Среднее значение площади среди всех стран");
                Console.WriteLine("  7. Медианная численность людей среди всех стран");
                Console.WriteLine("  8. Множество успешных республик(площадь > 500000, численность > 500000)");
                Console.WriteLine("0. Выход");
                NumericalInputValidation(ref count);
                switch (count)
                {
                    case 1:
                        collection.Add();
                        Console.WriteLine("Элемент добавлен");
                        break;
                    case 2:
                        if (collection.Count == 0)
                            Console.WriteLine("Коллекция пуста");
                        else
                            collection.Remove();
                        break;
                    case 3:
                        mLinq = !mLinq;
                        Console.WriteLine("Тип поменен");
                        break;
                    case 4:
                        if (collection.Count == 0)
                            Console.WriteLine("Коллекция пуста");
                        else
                        {
                            IEnumerable<IGrouping<int, Republic>> states = (collection.QueryCollections(mLinq)).GroupTerm();
                            string result = "";
                            Console.WriteLine("\nГруппировка по сроку правления: ");
                            foreach (IGrouping<int, Republic> item in states)
                            {
                                Console.Write($"{item.Key}: ");
                                foreach (var t in item)
                                    Console.Write($"{t.NameState} ");
                                Console.WriteLine();
                            }
                            Console.WriteLine(result);
                        }
                        break;
                    case 5:
                        if (collection.Count == 0)
                            Console.WriteLine("Коллекция пуста");
                        else
                        {
                            Console.WriteLine($"Количество стран: {collection.QueryCollections(mLinq).CountTerr()}");
                        }
                        break;
                    case 6:
                        if (collection.Count == 0)
                            Console.WriteLine("Коллекция пуста");
                        else
                        {
                            Console.WriteLine($"Средняя площадь: {collection.QueryCollections(mLinq).AverageTerr()}");
                        }
                        break;
                    case 7:
                        if (collection.Count == 0)
                            Console.WriteLine("Коллекция пуста");
                        else
                        {
                            Console.WriteLine($"Медианная площадь территории: {collection.QueryCollections(mLinq).MedianNumber()}");
                        }
                        break;
                    case 8:
                        if (collection.Count == 0)
                            Console.WriteLine("Коллекция пуста");
                        else
                        {
                            IEnumerable<string> states = (collection.QueryCollections(mLinq)).RepublicKingdom();
                            string result = "";
                            foreach (var item in states)
                                result += item + "\n";
                            Console.WriteLine("^^Множество республик и королевств^^");
                            Console.WriteLine(result);
                        }
                        break;
                    default:
                        Console.WriteLine("Неправильная команда");
                        break;
                }
            } while (count != 0);
        }

        public static void NumericalInputValidation(ref int k, string instruction = "> ") //параметр по умолчанию
        {
            bool ok;
            do
            {
                Console.Write(instruction);
                string buf = Console.ReadLine();
                ok = int.TryParse(buf, out k);
                if (!ok)
                    Console.WriteLine("Не число! Попробуйте ещё раз!");
            } while (!ok);
        }
    }
}
