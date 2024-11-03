using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace www
{
    public partial class dab : Form
    {
        private SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-EAONQ68; Initial Catalog=asd; Integrated Security=True");
        private DataTable table = new DataTable();

        private string currentTableName; // Хранит название текущей таблицы
        public dab()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Master", con);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable table = new DataTable();

            table.Load(reader);
            dataGridView1.DataSource = table;

            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Client", con);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable table = new DataTable();

            table.Load(reader);
            dataGridView1.DataSource = table;

            con.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Service", con);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable table = new DataTable();

            table.Load(reader);
            dataGridView1.DataSource = table;

            con.Close();

            dataGridView1.ReadOnly = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Appointment", con);
            SqlDataReader reader = cmd.ExecuteReader();
            DataTable table = new DataTable();

            table.Load(reader);
            dataGridView1.DataSource = table;
            con.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        if (row.Cells["name_subject"].Value != null && row.Cells["name_subject"].Value.ToString().Length > 0)
                        {
                            SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM subject WHERE name_subject = @name_subject", con);
                            checkCmd.Parameters.Add("@name_subject", SqlDbType.VarChar, 50);
                            checkCmd.Parameters["@name_subject"].Value = row.Cells["name_subject"].Value;
                            int count = (int)checkCmd.ExecuteScalar();

                            if (count == 0)
                            {
                                SqlCommand cmd = new SqlCommand("INSERT INTO subject (name_subject) VALUES (@name_subject)", con);
                                cmd.Parameters.Add("@name_subject", SqlDbType.VarChar, 50);
                                cmd.Parameters["@name_subject"].Value = row.Cells["name_subject"].Value;
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                MessageBox.Show("Запись с таким именем предмета уже существует. Вставка не выполнена.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Поле name_subject не заполнено. Вставка не выполнена.", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                MessageBox.Show("Успешно", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }
    }
}