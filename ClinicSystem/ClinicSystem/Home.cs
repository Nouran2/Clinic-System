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
    public partial class Home : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Abdo\Documents\ClinicDB.mdf;Integrated Security=True;Connect Timeout=30");
        private void CountPat()
        {
            Con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select count(*) from PatientTable" , Con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            PatNumlb.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountDoc()
        {
            Con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select count(*) from DoctorTable", Con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            DocNumlb.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        private void CountLab()
        {
            Con.Open();
            SqlDataAdapter adapter = new SqlDataAdapter("select count(*) from TestTable", Con);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            LabNumLb.Text = dt.Rows[0][0].ToString();
            Con.Close();
        }
        public Home()
        {
            InitializeComponent();
            if (Form1.Role == "Receptionist")
            {
                ReceptionistLb.Enabled = false;
                DoctorsLb.Enabled = false;
                LabLb.Enabled = false;
            }
            CountPat();
            CountDoc();
            CountLab();
        }

        private void pictureBox14_Click(object sender, EventArgs e)
        {

        }

        private void PatientsLb_Click(object sender, EventArgs e)
        {
            patient obj = new patient();
            obj.Show();
            this.Hide();
        }

        private void DoctorsLb_Click(object sender, EventArgs e)
        {
            Doctors obj = new Doctors();
            obj.Show();
            this.Hide();
        }

        private void LabLb_Click(object sender, EventArgs e)
        {
            labtest obj = new labtest();
            obj.Show();
            this.Hide();
        }

        private void ReceptionistLb_Click(object sender, EventArgs e)
        {
            receptionist obj = new receptionist();
            obj.Show();
            this.Hide();
        }

        private void rjButton1_Click(object sender, EventArgs e)
        {
            Form1 obj = new Form1();
            obj.Show();
            this.Hide();
        }
    }
}
