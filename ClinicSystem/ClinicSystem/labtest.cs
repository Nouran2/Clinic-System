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

namespace ClinicSystem
{
    public partial class labtest : Form
    {
        public labtest()
        {
            InitializeComponent();
            DisplayTest();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Abdo\Documents\ClinicDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void DisplayTest()
        {
            Con.Open();
            string Query = "Select * from TestTable";
            SqlDataAdapter sda = new SqlDataAdapter(Query, Con);

            // Instantiate SqlCommandBuilder and associate it with the SqlDataAdapter
            SqlCommandBuilder Builder = new SqlCommandBuilder(sda);

            var ds = new DataSet();
            sda.Fill(ds);

            labtestDGV.DataSource = ds.Tables[0];
            clear();
            Con.Close();


        }
        int key = 0;
        private void clear()
        {

            labtestTB.Text = "";
            labcostTB.Text = "";

            key = 0;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void rjButton5_Click(object sender, EventArgs e)
        {
            if (labcostTB.Text == "" || labtestTB.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("insert into TestTable(TestName,TestCost)values(@TN,@TC)", Con);
                    cmd.Parameters.AddWithValue("@TN", labtestTB.Text);
                    cmd.Parameters.AddWithValue("@TC", labcostTB.Text);


                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Test is Added");
                    Con.Close();
                    DisplayTest();
                    clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void DelBtn(object sender, EventArgs e)
        {
            if (key == 0)
            {
                MessageBox.Show("Select the Lab Test");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Delete from TestTable where TestNum=@TKey", Con);
                    cmd.Parameters.AddWithValue("@TKey", key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Lab Test is Deleted");
                    Con.Close();
                    DisplayTest();
                    clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }

        }

        private void EditBtn_Click(object sender, EventArgs e)
        {
            if (labcostTB.Text == "" || labtestTB.Text == "")
            {
                MessageBox.Show("Missing Information");
            }
            else
            {
                try
                {
                    Con.Open();
                    SqlCommand cmd = new SqlCommand("Update TestTable Set TestName=@TN,TestCost=@TC where TestNum=@TKey", Con);
                    cmd.Parameters.AddWithValue("@TN", labtestTB.Text);
                    cmd.Parameters.AddWithValue("@TC", labcostTB.Text);
                    cmd.Parameters.AddWithValue("@TKey", key);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Test is Updated");
                    Con.Close();
                    DisplayTest();
                    clear();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        private void labtestDGV_SelectionChanged(object sender, EventArgs e)
        {
            if(labtestDGV.SelectedRows.Count > 0)
            {
                labtestTB.Text = labtestDGV.SelectedRows[0].Cells[1].Value.ToString();
                labcostTB.Text = labtestDGV.SelectedRows[0].Cells[2].Value.ToString();

                
                if (int.TryParse(labtestDGV.SelectedRows[0].Cells[0].Value?.ToString(), out int selectedKey))
                {
                    key = selectedKey;
                }
                else
                {
                    key = 0;
                }
            }
              else
            {
                labtestTB.Text = "";
                labcostTB.Text = "";
                key = 0;
            }
        }
    }
}
