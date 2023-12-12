using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Runtime.Remoting.Contexts;

namespace _07LabExer1_SQL_Select_Insert_andUpdate_
{
    public partial class FrmClubRegistration : Form
    {
        public FrmClubRegistration()
        {
            InitializeComponent();
        }

        private ClubRegistrationQuery clubRegistrationQuery;
        private int ID, Age, count;
        private string FirstName, MiddleName, LastName, Gender, Program;

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            clubRegistrationQuery = new ClubRegistrationQuery();

            RefreshListOfClubMembers();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            long studentid = Convert.ToInt64(dataGridView.SelectedRows[0].Cells["StudentID"].Value);
            string connect = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\erine\\source\\repos\\_07LabExer1(SQL Select,Insert andUpdate)\\ClubDB.mdf\";Integrated Security=True;Context Connection=False";
            SqlConnection conn = new SqlConnection(connect);
            conn.Open();
            string Delete = "DELETE FROM ClubMembers Where StudentID = @StudentID";
            SqlCommand sqlCommand = new SqlCommand(Delete, conn);
            sqlCommand.Parameters.AddWithValue("@StudentID", studentid);
            sqlCommand.ExecuteNonQuery();
            conn.Close();
        }

        private long StudentId;
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            FrmUpdateMember updateMember = new FrmUpdateMember();
            updateMember.Show();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            clubRegistrationQuery = new ClubRegistrationQuery();

            RefreshListOfClubMembers();
        }
        public void RefreshListOfClubMembers()
        {
            clubRegistrationQuery.DisplayList();
            dataGridView.DataSource = clubRegistrationQuery.bindingSource;
        }

        public int RegistrationID() 
        {
            count++;
            return count;
        }
        private void button1_Click(object sender, EventArgs e)
        {
                StudentId = Int64.Parse(txtStudID.Text);
                FirstName = txtFName.Text;
                MiddleName = txtMname.Text;
                LastName = txtLName.Text;
                Age = Int32.Parse(txtAge.Text);
                Gender = cbGender.Text;
                Program = cbProgram.Text;

                clubRegistrationQuery.RegisterStudent(RegistrationID(), StudentId, FirstName, MiddleName, LastName, Age, Gender, Program);
                RefreshListOfClubMembers();
           
        }
    }
}
