using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace Toli_bichig
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=C:\\dict\\toli.db;Version=3;";
        private string savedConnectionString = "Data Source=C:\\dict\\saved.db;Version=3;";

        // zasah bolon ustgah ugiin songogdson id
        private int selectedId = -1;
        public Form1()
        {
            InitializeComponent();
            LoadData();
            LoadSavedData();
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

        // saved.db датабаазаас хадгалсан үгсийг dataGridView2 рүү татаж харуулах
        private void LoadSavedData()
        {
            using (SQLiteConnection conn = new SQLiteConnection(savedConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM saved_words;";
                    SQLiteDataAdapter da = new SQLiteDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView2.DataSource = dt; // Баруун талын хүснэгтийн нэр
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Хадгалсан өгөгдөл татахад алдаа гарлаа: " + ex.Message);
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
            if (selectedId == -1)
            {
                MessageBox.Show("Эхлээд засах үгээ хүснэгтээс сонгоно уу!");
                return;
            }

            string connectionString = "Data Source=C:\\dict\\toli.db;Version=3;";
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                string query = "UPDATE words SET eng = @eng, mon = @mon, rus = @rus WHERE id = @id;";

                using (var cmd = new SQLiteCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@eng", textBox2.Text);
                    cmd.Parameters.AddWithValue("@mon", textBox3.Text);
                    cmd.Parameters.AddWithValue("@rus", textBox4.Text);
                    cmd.Parameters.AddWithValue("@id", selectedId);

                    cmd.ExecuteNonQuery();
                }
            }
            MessageBox.Show("Амжилттай засагдлаа!");
            LoadData();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (selectedId == -1)
            {
                MessageBox.Show("Эхлээд устгах үгээ хүснэгтээс сонгоно уу!");
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Энэ үгийг устгах уу?", "Баталгаажуулалт", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string connectionString = "Data Source=C:\\dict\\toli.db;Version=3;";
                using (var conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();
                    string query = "DELETE FROM words WHERE id = @id;";

                    using (var cmd = new SQLiteCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", selectedId);
                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Амжилттай устгагдлаа!");
                LoadData();

                selectedId = -1;
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {

            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();

            selectedId = -1;

            dataGridView1.ClearSelection();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 1. Хэрэглэгч хүснэгтээс ямар нэгэн мөр сонгосон эсэхийг шалгах
            if (selectedId == -1)
            {
                MessageBox.Show("Эхлээд хадгалах үгээ хүснэгтээс сонгоно уу!");
                return;
            }

            DataGridViewRow selectedRow = null;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["id"].Value != null && Convert.ToInt32(row.Cells["id"].Value) == selectedId)
                {
                    selectedRow = row;
                    break;
                }
            }

            if (selectedRow == null)
            {
                MessageBox.Show("Сонгогдсон мөр олдсонгүй!");
                return;
            }

            string engWord = selectedRow.Cells["eng"].Value.ToString();
            string monWord = selectedRow.Cells["mon"].Value.ToString();
            string rusWord = selectedRow.Cells["rus"].Value.ToString();

            

            using (var conn = new SQLiteConnection(savedConnectionString))
            {
                conn.Open();

                string insertQuery = "INSERT INTO saved_words (eng, mon, rus) VALUES (@eng, @mon, @rus);";

                using (var cmd = new SQLiteCommand(insertQuery, conn))
                {
                    cmd.Parameters.AddWithValue("@eng", engWord);
                    cmd.Parameters.AddWithValue("@mon", monWord);
                    cmd.Parameters.AddWithValue("@rus", rusWord);

                    cmd.ExecuteNonQuery();
                }
            }

            MessageBox.Show("Сонгосон үг амжилттай хадгалагдлаа!");
            LoadSavedData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                selectedId = Convert.ToInt32(row.Cells["id"].Value);

            }
        }



        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }


    }
}
