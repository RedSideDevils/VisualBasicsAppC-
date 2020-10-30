using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace AppWithDb
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        public Form1()
        {
            InitializeComponent();
        }

        private async void load_db()
        {
            string ConString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\AppWithDb\AppWithDb\Database1.mdf;Integrated Security=True";
            con = new SqlConnection(ConString);
            await con.OpenAsync();
            SqlDataReader reader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM Students", con);
            try
            {
                reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    string output = "ID:" + Convert.ToString(reader["id"]) + " : " + Convert.ToString(reader["Name"]);
                    listBox1.Items.Add(output);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), ex.Source.ToString(), MessageBoxButtons.OKCancel);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            load_db();        
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (con != null && con.State != ConnectionState.Closed)
            {
                con.Close();
                System.Windows.Forms.Application.ExitThread();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void button2_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("INSERT INTO Students (Name) VALUES (@Name)",con);
            command.Parameters.AddWithValue("Name", textBox1.Text);
            await command.ExecuteNonQueryAsync();
            listBox1.Items.Clear();
            load_db();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            load_db();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("DELETE FROM Students WHERE Id = @id", con);
            string item = listBox1.SelectedItem.ToString();
            string item_id = item.Substring(3, 3);
            command.Parameters.AddWithValue("id", item_id);
            await command.ExecuteNonQueryAsync();
            listBox1.Items.Clear();
            load_db();
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("UPDATE Students SET Name = @name WHERE Id = @id", con);
            command.Parameters.AddWithValue("name", textBox1.Text);
            string item = listBox1.SelectedItem.ToString();
            string item_id = item.Substring(3, 3);
            command.Parameters.AddWithValue("id", item_id);
            await command.ExecuteNonQueryAsync();
            listBox1.Items.Clear();
            load_db();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
