using System;
using System.Collections.Generic;
using System.Text;
using labwork10;

namespace lab_work_14
{
    class TestCollections
    {
        SortedDictionary<State, Republic> col4 = new SortedDictionary<State, Republic>();
        int sizeCollections = 0;
        Random rnd = new Random();
        string[] strs = {"QQQQQ", "WWWWW", "RRRRRR", "TTTTTT", "YYYYYYY", "KKKKKK", "OOOOO", "FFFFFF", "LLLLLL", "CCCCCC", "BBBBB" };

        void ReadyCollections()
        {
            Republic r1 = new Republic("Бенин", 112622, 12000000, 10);
            State s1 = r1.GetBase();
            col4.Add(s1, r1);
            Republic r2 = new Republic("Канада", 9984670, 37602103, 10);
            State s2 = r2.GetBase();
            col4.Add(s2, r2);
            Republic r3 = new Republic("Бутан", 38394, 758288, 3);
            State s3 = r3.GetBase();
            col4.Add(s3, r3);
            Republic r4 = new Republic("Гватемала", 108889, 14373472, 4);
            State s4 = r4.GetBase();
            col4.Add(s4, r4);
            Republic r5 = new Republic("Япония", 377944, 125900000, 60);
            State s5 = r5.GetBase();
            col4.Add(s5, r5);
            Republic r6 = new Republic("Камбоджа", 181035, 16926984, 3);
            State s6 = r6.GetBase();
            col4.Add(s6, r6);
            Republic r7 = new Republic("Филиппины", 299764, 102921200, 6);
            State s7 = r7.GetBase();
            col4.Add(s7, r7);
            Republic r8 = new Republic("Австралия", 7692024, 25180200, 1000);
            State s8 = r8.GetBase();
            col4.Add(s8, r8);
            Republic r9 = new Republic("Дания", 43094, 5822763, 280);
            State s9 = r9.GetBase();
            col4.Add(s9, r9);
            Republic r10 = new Republic("Чили", 756102, 18186770, 4);
            State s10 = r10.GetBase();
            col4.Add(s10, r10);
            sizeCollections = 10;
        }

        void PrintSortedDictionaryOfString(SortedDictionary<State, Republic> collection)
        {
            Console.WriteLine("\nПечать коллекции SortedDictionary<State, Republic>");
            ICollection<State> keys = collection.Keys;
            foreach (State s in keys)
            {
                Console.WriteLine($"{s.ToString()} | {collection[s].ToString()}");
            }
        }

        public TestCollections()
        {
            ReadyCollections();
            //PrintCollections();
        }

        public void PrintCollections()
        {
            PrintSortedDictionaryOfString(col4);
        }

        public int Count
        {
            get
            {
                int count = 0;
                foreach (State node in col4.Keys)
                {
                    count++;
                }
                sizeCollections = count;
                return sizeCollections;
            }
        }

        public void Add()
        {
            Republic r = new Republic();
            State s = r.GetBase();
            int obj1, obj2, obj3, obj4;
                obj1 = rnd.Next(0, 9);
                obj2 = rnd.Next(2, 100000);
                obj3 = rnd.Next(2, 100000);
                obj4 = rnd.Next(1, 100000);
                obj4 = rnd.Next(1, 15);
                r = new Republic(strs[obj1], obj2, obj3, obj4);
                col4.Add(s, r);
                sizeCollections++;
        }

        public void Remove()
        {
            try
            {
                if (Count > 0)
                {
                    Console.WriteLine("Удаляемый элемент");
                    State s;
                    Republic r = new Republic();
                    r.Init();
                    s = r.GetBase();
                    bool remv = col4.Remove(s);
                    if (remv)
                    {
                        sizeCollections--;
                        Console.WriteLine("Элемент удален");
                    }
                    else
                    {
                        Console.WriteLine("Элемент для удаления не найден");
                    }
                }
                else
                {
                    Console.WriteLine("Коллекции пусты");
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Неверный ввод данных");
            }
        }

        public override string ToString()
        {
            int i = 0, n = col4.Count;
            string result = "";
            foreach (var item in col4)
                result += $"{item.Key}: {item.Value.NameState}, {item.Value.Territory}, {item.Value.NumberOfCitizens}\n";
            return result;
        }

        public IQuery QueryCollections(bool linq)
        {
            IQuery result;
            if (linq)
                result = new LinqQuery { col4 = col4 };
            else
                result = new ExtensionQuery { col4 = col4 };
            return result;
        }
    }
}
