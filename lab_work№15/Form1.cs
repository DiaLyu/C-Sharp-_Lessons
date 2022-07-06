using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab_work_15
{
    public partial class Form1 : Form
    {
        Thread t1, t2, t3;
        static object locker = new object();

        public Form1()
        {
            InitializeComponent();
            t1 = new Thread(PrintGraphic1);
            t2 = new Thread(PrintGraphic2);
            t3 = new Thread(PrintGraphic3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try 
            {
                t1.Start();
            }
            catch (System.Threading.ThreadStateException)
            {
                MessageBox.Show("Поток запущен. Перезапустите снова", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void PrintGraphic1()
        {
            double a = -10, b = 4, h = 0.1, x, y;
            //ClearPoints(0);
            x = a;
            while (x <= b)
            {
                y = Math.Sin(x);
                lock (locker)
                {
                    this.Invoke(new EventHandler(delegate
                    {
                        this.chart1.Series[0].Points.AddXY(x, y);
                    }));
                }
                x += h;
                Thread.Sleep(5);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                t2.Start();
            }
            catch (System.Threading.ThreadStateException)
            {
                MessageBox.Show("Поток запущен. Перезапустите снова", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try { 
                t3.Start();
            }
            catch (System.Threading.ThreadStateException)
            {
                MessageBox.Show("Поток запущен. Перезапустите снова", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.chart1.Series[0].Points.Clear();
            this.chart1.Series[1].Points.Clear();
            this.chart1.Series[2].Points.Clear();
            t1 = new Thread(PrintGraphic1);
            t2 = new Thread(PrintGraphic2);
            t3 = new Thread(PrintGraphic3);
        }

        public void PrintGraphic2()
        {
            double a = -10, b = 4, h = 0.1, x, y;
            //ClearPoints(1);
            x = a;
            while (x <= b)
            {
                y = 4 * Math.Pow(x, 2) - 2 * x - 22;
                lock (locker)
                {
                    this.Invoke(new EventHandler(delegate
                    {
                        this.chart1.Series[1].Points.AddXY(x, y);
                    }));
                }
                x += h;
                Thread.Sleep(5);
            }
        }

        public void PrintGraphic3()
        {
            double a = -10, b = -3, h = 0.1, x, y;
            //ClearPoints(2);
            x = a;
            while (x <= b)
            {
                y = Math.Log(Math.Pow(x, 2)) / Math.Pow(x, 3);
                lock (locker)
                {
                    this.Invoke(new EventHandler(delegate
                    {
                        this.chart1.Series[2].Points.AddXY(x, y);
                    }));
                }
                x += h;
                Thread.Sleep(5);
            }
        }

    }
}
