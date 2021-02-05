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

namespace HamzaPasswordManager
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var select = "SELECT * FROM person";
            var c = new SqlConnection("Data Source=DESKTOP-NSRE279;Initial Catalog=HamzaPasswordManager;Integrated Security=true");
            var dataAdapter = new SqlDataAdapter(select, c);

            var commandBuilder = new SqlCommandBuilder(dataAdapter);
            var ds = new DataSet();
            dataAdapter.Fill(ds);
            dataGridView1.ReadOnly = true;
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source = DESKTOP-NSRE279; Initial Catalog = HamzaPasswordManager; Integrated Security = True"))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO person VALUES (@title, @username,@password)", con);

                cmd.Parameters.AddWithValue("@title", textBox1.Text);
                cmd.Parameters.AddWithValue("@username", textBox2.Text);
                cmd.Parameters.AddWithValue("@password", textBox2.Text);
                cmd.ExecuteNonQuery();
                con.Close();
            }

            MessageBox.Show("done!");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source = DESKTOP-NSRE279; Initial Catalog = HamzaPasswordManager; Integrated Security = True"))
            {
                SqlCommand delcmd = new SqlCommand();
                for (int x = dataGridView1.Rows.Count - 1; x >= 0; x--)
                {
                    var row = dataGridView1.Rows[x];

                    if (row.Selected)
                    {
                        dataGridView1.Rows.RemoveAt(row.Index);
                        try
                        {
                            delcmd.CommandText = "Delete From person Where titre=@titre";
                            con.Open();
                            delcmd.Connection = con;
                            delcmd.Parameters.AddWithValue("@titre", row.Cells["titre"].Value.ToString());
                            delcmd.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }
                }

            }
        }
    }
}
