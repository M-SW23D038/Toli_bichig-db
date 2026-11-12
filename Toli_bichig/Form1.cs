using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Toli_bichig
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=C:\\dict\\toli.db;Version=3;";

        // zasah bolon ustgah ugiin songogdson id
        private int selectedId = -1;
        public Form1()
        {
            InitializeComponent();
            LoadData();
        }

        // words table-iig haruulah
        private void LoadData()
        {
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM words;";
                    SQLiteDataAdapter da = new SQLiteDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Өгөгдөл татахад алдаа гарлаа: " + ex.Message);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("Англи болон Монгол үгээ оруулна уу!");
                return;
            }

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO words (eng, mon, rus) VALUES (@eng, @mon, @rus);";
                    using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@eng", textBox2.Text.Trim());
                        cmd.Parameters.AddWithValue("@mon", textBox3.Text.Trim());
                        cmd.Parameters.AddWithValue("@rus", textBox4.Text.Trim());

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Үг амжилттай нэмэгдлээ!");
                    LoadData();
                    btnClear_Click(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Нэмэхэд алдаа гарлаа: " + ex.Message);
                }
            }
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {

        }
        private void btnDelete_Click(object sender, EventArgs e)
        {

        }
        private void btnClear_Click(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
