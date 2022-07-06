using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace lab_work_16
{
    public partial class Form1 : Form
    {
        private SqlConnection sqlConnection = null;
        string subdiv = "";
        string month = "";
        int numTask = 0;
        int idelem = 1;
        Random rnd = new Random();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection(ConfigurationManager.ConnectionStrings["Company"].ConnectionString);

            sqlConnection.Open();

            if(sqlConnection.State == ConnectionState.Open)
            {
                MessageBox.Show("Подключение установлено");
            }

            string query = $"DELETE FROM dbo.Report";
            SqlCommand commandSelect = new SqlCommand(query, sqlConnection);
            commandSelect.ExecuteNonQuery();
            query = $"DELETE FROM dbo.TimeAndIncome";
            commandSelect = new SqlCommand(query, sqlConnection);
            commandSelect.ExecuteNonQuery();

            RandomFormation(1);
            RandomFormation(2);
            RandomFormation(3);
            RandomFormation(4);
            RandomFormation(5);

            LoadData();
        }

        private void LoadData()
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter(
                "SELECT R.IdReport, S.NameSubdivision, T.Year, T.Month, T.Income FROM dbo.Report AS R " +
                "JOIN dbo.Subdivisions AS S ON R.IdSubdivision = S.IdSubdivisions " +
                "JOIN dbo.TimeAndIncome AS T ON R.IdTimeAndIncome = T.IdTimeAndIncome " +
                "ORDER BY IdReport",
                sqlConnection);

            DataSet dataset = new DataSet();
            dataAdapter.Fill(dataset);

            dataGridView1.DataSource = dataset.Tables[0];
        }

        private void iNSERTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            dataGridView1.Visible = false;
            button1.Visible = true;
            button2.Visible = true;
            button1.Text = "Добавить";

            numTask = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            switch (numTask)
            {
                case 1:
                    InsertData();
                    break;
                case 2:
                    DeleteData();
                    break;
                case 3:
                    UpdateData();
                    break;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            subdiv = comboBox1.SelectedItem.ToString();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            month = comboBox2.SelectedItem.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            label2.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            textBox1.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            dataGridView1.Visible = true;
            button1.Visible = false;
            button2.Visible = false;

            LoadData();
        }

        private void dELETEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label5.Visible = true;
            textBox3.Visible = true;
            button1.Visible = false;
            button2.Visible = false;
            dataGridView1.Visible = false;
            button1.Visible = true;
            button2.Visible = true;

            button1.Text = "Удалить";
            numTask = 2;
        }

        private void InsertData()
        {
            try
            {
                bool haveSubdiv = false;
                int numSubdiv = 0;

                SqlCommand command = sqlConnection.CreateCommand();
                command.CommandText = "SELECT MAX(IdTimeAndIncome) FROM dbo.TimeAndIncome";
                int result;
                if (command.ExecuteScalar() == DBNull.Value)
                {
                    result = 1;
                }
                else
                {
                    result = Convert.ToInt32(command.ExecuteScalar()) + 1;
                }

                command.CommandText = "INSERT INTO dbo.TimeAndIncome (IdTimeAndIncome, Year, Month, Income) VALUES (@idTime, @Year, @Month, @Income)";
                command.Parameters.Add("idTime", SqlDbType.NVarChar).Value = result;
                command.Parameters.Add("Year", SqlDbType.NVarChar).Value = Convert.ToInt32(textBox1.Text);
                command.Parameters.Add("Month", SqlDbType.NVarChar).Value = month;
                command.Parameters.Add("Income", SqlDbType.NVarChar).Value = Convert.ToInt32(textBox2.Text);

                string query = "SELECT NameSubdivision FROM dbo.Subdivisions";
                SqlCommand commandSelect = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = commandSelect.ExecuteReader();
                List<string> data = new List<string>();
                while (reader.Read())
                {
                    data.Add(reader.GetValue(0).ToString());
                }
                reader.Close();

                for (int i = 0; i < data.Count; i++)
                {
                    if (subdiv == data[i])
                    {
                        haveSubdiv = true;
                        numSubdiv = i + 1;
                    }
                }

                if (haveSubdiv)
                {
                    object id = null;
                    command.ExecuteNonQuery();
                    query = "SELECT IdTimeAndIncome FROM dbo.TimeAndIncome WHERE (Year = " + Convert.ToInt32(textBox1.Text) + ") AND (Month = \'" + month + "\') AND (Income = " + Convert.ToInt32(textBox2.Text) + ")";
                    commandSelect = new SqlCommand(query, sqlConnection);
                    reader = commandSelect.ExecuteReader();
                    while (reader.Read())
                    {
                        id = reader.GetValue(0);
                    }
                    reader.Close();

                    command.CommandText = "SELECT MAX(IdReport) FROM dbo.Report";
                    if (command.ExecuteScalar() == DBNull.Value)
                    {
                        result = 1;
                    }
                    else
                    {
                        result = Convert.ToInt32(command.ExecuteScalar()) + 1;
                    }

                    command.CommandText = "INSERT INTO dbo.Report (IdReport, IdCompany, IdSubdivision, IdTimeAndIncome) VALUES (@idRep, @company, @subdivision, @time)";
                    command.Parameters.Add("idRep", SqlDbType.NVarChar).Value = result;
                    command.Parameters.Add("company", SqlDbType.NVarChar).Value = 1;
                    command.Parameters.Add("subdivision", SqlDbType.NVarChar).Value = numSubdiv;
                    command.Parameters.Add("time", SqlDbType.NVarChar).Value = id;

                    command.ExecuteNonQuery();

                    MessageBox.Show("Данные добавлены в базу данных", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Введенного вами подразделения в данной компании не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void DeleteData()
        {
            try
            {
                string query = $"DELETE FROM dbo.Report WHERE IdReport = {Convert.ToInt32(textBox3.Text)}";
                SqlCommand commandSelect = new SqlCommand(query, sqlConnection);
                int number = commandSelect.ExecuteNonQuery();
                if (number == 0)
                {
                    MessageBox.Show("Данные с введенным ключом не найдены", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Данные удалены", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateData()
        {
            try
            {
                int subd = 0;
                int numTimeInc = 0;

                string query = "SELECT NameSubdivision FROM dbo.Subdivisions";
                SqlCommand commandSelect = new SqlCommand(query, sqlConnection);
                SqlDataReader reader = commandSelect.ExecuteReader();
                List<string> data = new List<string>();
                while (reader.Read())
                {
                    data.Add(reader.GetValue(0).ToString());
                }
                reader.Close();

                query = $"SELECT IdTimeAndIncome FROM dbo.Report WHERE IdReport = {Convert.ToInt32(textBox3.Text)}";
                commandSelect = new SqlCommand(query, sqlConnection);
                reader = commandSelect.ExecuteReader();
                while (reader.Read())
                {
                    numTimeInc = Convert.ToInt32(reader.GetValue(0));
                }
                reader.Close();

                for (int i = 0; i < data.Count; i++)
                {
                    if (subdiv == data[i])
                    {
                        subd = i + 1;
                    }
                }

                query = $"UPDATE dbo.Report SET IdSubdivision = {subd}, IdTimeAndIncome = {numTimeInc} WHERE IdReport = {Convert.ToInt32(textBox3.Text)}";
                commandSelect = new SqlCommand(query, sqlConnection);
                commandSelect.ExecuteNonQuery();
                int number = commandSelect.ExecuteNonQuery();
                if (number == 0)
                {
                    MessageBox.Show("Данные с введенным ключом не найдены", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Данные обновлены", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    query = "UPDATE dbo.TimeAndIncome SET Year = " + Convert.ToInt32(textBox1.Text) +
                    ", Month = \'" + month + "\', Income = " + Convert.ToInt32(textBox2.Text) +
                    " WHERE IdTimeAndIncome = " + numTimeInc;
                    commandSelect = new SqlCommand(query, sqlConnection);
                    commandSelect.ExecuteNonQuery();
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Неверный формат данных", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void uPDATEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;
            label4.Visible = true;
            label5.Visible = true;
            comboBox1.Visible = true;
            comboBox2.Visible = true;
            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            dataGridView1.Visible = false;
            button1.Visible = true;
            button2.Visible = true;
            button1.Text = "Обновить";

            numTask = 3;
        }

        private void RandomFormation(int numSubdivision)
        {
            SqlCommand command = sqlConnection.CreateCommand();

            int year = 2016;
            int incm;
            string[] months = { "Январь", "Февраль", "Март", "Апрель", "Май", "Июнь", "Июль", "Август", "Сентябрь", "Октябрь", "Ноябрь", "Декабрь" };

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    incm = rnd.Next(2000, 100000);
                    command.CommandText = "INSERT INTO dbo.TimeAndIncome (IdTimeAndIncome, Year, Month, Income) VALUES (" + idelem + ", " + year + ", \'" + months[j] + "\', " + incm + ")";
                    command.ExecuteNonQuery();

                    command.CommandText = $"INSERT INTO dbo.Report (IdReport, IdCompany, IdSubdivision, IdTimeAndIncome) VALUES ({idelem}, {1}, {numSubdivision}, {idelem})";
                    command.ExecuteNonQuery();
                    idelem++;
                }
                year++;
                Thread.Sleep(1);
            }
        }

        private string AverageIncome(string subdivision)
        {
            return "SELECT AVG(T.Income) FROM dbo.Report AS R " +
                "JOIN dbo.Subdivisions AS S ON R.IdSubdivision = S.IdSubdivisions " +
                "JOIN dbo.TimeAndIncome AS T ON R.IdTimeAndIncome = T.IdTimeAndIncome " +
                "WHERE T.Year >= 2016 AND T.Year <= 2020 AND S.NameSubdivision = \'" + subdivision + "\'";
        }

        private void averageIncomeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string info = "";
            int averag;
            SqlCommand command = new SqlCommand(AverageIncome("Бухгалтерия"), sqlConnection);
            averag = Convert.ToInt32(command.ExecuteScalar());
            info += "Бухгалтерия: ";
            info += averag.ToString();
            info += System.Environment.NewLine;
            
            command = new SqlCommand(AverageIncome("Лаборатория технико-экономических исследований"), sqlConnection);
            averag = Convert.ToInt32(command.ExecuteScalar());
            info += "Лаборатория технико-экономических исследований: ";
            info += averag.ToString();
            info += System.Environment.NewLine;
            
            command = new SqlCommand(AverageIncome("Служба делопроизводства"), sqlConnection);
            averag = Convert.ToInt32(command.ExecuteScalar());
            info += "Служба делопроизводства: ";
            info += averag.ToString();
            info += System.Environment.NewLine;
            
            command = new SqlCommand(AverageIncome("Технический отдел"), sqlConnection);
            averag = Convert.ToInt32(command.ExecuteScalar());
            info += "Технический отдел: ";
            info += averag.ToString();
            info += System.Environment.NewLine;
            
            command = new SqlCommand(AverageIncome("Опытно-экспериментальный цех"), sqlConnection);
            averag = Convert.ToInt32(command.ExecuteScalar());
            info += "Опытно - экспериментальный цех: ";
            info += averag.ToString();

            MessageBox.Show(info);
        }

        private string LongestPeriod(string division)
        {
            return "SELECT Income FROM dbo.TimeAndIncome AS T " +
                "JOIN dbo.Report AS R ON R.IdTimeAndIncome = T.IdTimeAndIncome " +
                "JOIN dbo.Subdivisions AS S ON R.IdSubdivision = S.IdSubdivisions " +
                "WHERE S.NameSubdivision = \'" + division + "\'";
        }

        private void theLongestPeriodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string info = SubdivPeriod("Бухгалтерия");
            info += SubdivPeriod("Лаборатория технико-экономических исследований");
            info += SubdivPeriod("Служба делопроизводства");
            info += SubdivPeriod("Технический отдел");
            info += SubdivPeriod("Опытно-экспериментальный цех");
            MessageBox.Show(info);
        }

        public string SubdivPeriod(string subdivision)
        {
            string info = "";
            int averag;
            SqlCommand command = new SqlCommand(LongestPeriod(subdivision), sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            List<int> data = new List<int>();
            while (reader.Read())
            {
                data.Add(Convert.ToInt32(reader.GetValue(0)));
            }
            reader.Close();
            command = new SqlCommand(AverageIncome(subdivision), sqlConnection);
            averag = Convert.ToInt32(command.ExecuteScalar());
            int maxPeriod = 1;
            IEnumerable<int> datas = new List<int>();
            datas = data;
            while (datas.Count() != 0)
            {
                int countData = (datas.TakeWhile(x => (x < averag))).Count();
                datas = datas.SkipWhile(x => x < averag);
                datas = datas.SkipWhile(x => x > averag);
                if (countData > maxPeriod)
                    maxPeriod = countData;
            }
            info += subdivision;
            info += ": ";
            info += maxPeriod;
            info += System.Environment.NewLine;
            return info;
        }
    }
}
