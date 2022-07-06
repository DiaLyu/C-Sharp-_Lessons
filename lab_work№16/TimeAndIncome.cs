using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_work_16
{
    class TimeAndIncome : ICloneable, IComparable
    {
        int year;
        int month;
        int income;
        string[] months = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь", };

        public int Year
        {
            get { return year; }
            set { year = value; }
        }

        public int Month
        {
            get { return month; }
            set
            {
                if (value < 13 && value > 0)
                    month = value;
            }
        }

        public int Income
        {
            get { return income; }
            set { income = value; }
        }

        public TimeAndIncome()
        {
            Year = 0;
            Month = 1;
            Income = 0;
        }

        public TimeAndIncome(int one, int two, int three)
        {
            Year = one;
            Month = two;
            Income = three;
        }

        public virtual object Clone()
        {
            return new TimeAndIncome(this.Year, this.Month, this.Income);
        }

        public override string ToString()
        {
            string nameMonth = months[Month - 1];
            return $"{Year} год, {nameMonth}, {Income} руб.";
        }

        public int CompareTo(object obj)//сравнение площадей
        {
            TimeAndIncome temp = (TimeAndIncome)obj;
            if (this.Year > temp.Year) return 1;
            if (this.Year < temp.Year) return -1;
            return 0;
        }
    }
}
