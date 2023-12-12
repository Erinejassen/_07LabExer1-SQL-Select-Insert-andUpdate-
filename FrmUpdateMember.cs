using Microsoft.SqlServer.Server;
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

namespace _07LabExer1_SQL_Select_Insert_andUpdate_
{
    public partial class FrmUpdateMember : Form
    {
        private SqlCommand command;
        private SqlConnection conn;
        public DataTable dataTable;
        private SqlDataReader read;
        public BindingSource bindingSource;
        public string connect;
        public FrmUpdateMember()
        {
            InitializeComponent();
            connect = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=\"C:\\Users\\erine\\source\\repos\\_07LabExer1(SQL Select,Insert andUpdate)\\ClubDB.mdf\";Integrated Security=True;Context Connection=False";
            conn = new SqlConnection(connect);
        }

        private void FrmUpdateMember_Load(object sender, EventArgs e)
        {
            ComboDisplay();
        }

        public void ComboDisplay()
        {
            try
            {
                conn.Open();
                string retrieveInfo = "SELECT StudentID FROM ClubMembers";
                command = new SqlCommand(retrieveInfo, conn);
                read = command.ExecuteReader();
                cbStudID.Items.Clear();

                while (read.Read())
                {
                    long studentID = read.GetInt64(0);
                    cbStudID.Items.Add(studentID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void cbStudID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStudID.SelectedItem != null)
            {
                long studentID = (long)cbStudID.SelectedItem;
                DisplayStudentInfo(studentID);
            }
        }

        private void DisplayStudentInfo(long studentID)
        {
            try
            {
                conn.Open();
                string retrieveInfo = "SELECT FirstName, MiddleName, LastName, Age, Gender, Program FROM ClubMembers WHERE StudentID = @StudentID";
                command = new SqlCommand(retrieveInfo, conn);
                command.Parameters.AddWithValue("@StudentID", studentID);
                read = command.ExecuteReader();

                if (read.Read())
                {
                    txtFname.Text = read.GetString(0);
                    txtMname.Text = read.GetString(1);
                    txtLname.Text = read.GetString(2);
                    txtAge.Text = read.GetInt32(3).ToString();
                    cbGender.Text = read.GetString(4);
                    cbProgram.Text = read.GetString(5);
                }
                else
                {
                    MessageBox.Show("Student not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();

            string update = "UPDATE ClubMembers SET FirstName = @FirstName, MiddleName = @MiddleName, LastName = @LastName, Age = @Age, Gender = @Gender, Program = @Program WHERE StudentID = @StudentID";
            command = new SqlCommand(update, conn);
            command.Parameters.AddWithValue("@StudentID", cbStudID.Text.ToString());
            command.Parameters.AddWithValue("@FirstName", txtFname.Text);
            command.Parameters.AddWithValue("@MiddleName", txtMname.Text);
            command.Parameters.AddWithValue("@LastName", txtLname.Text);
            command.Parameters.AddWithValue("@Age", txtAge.Text);
            command.Parameters.AddWithValue("@Gender", cbGender.Text.ToString());
            command.Parameters.AddWithValue("@Program", cbProgram.Text.ToString());

            int rowsAffected = command.ExecuteNonQuery();
            conn.Close();

            if (rowsAffected > 0)
            {
                MessageBox.Show("Update successful");
            }
            else
            {
                MessageBox.Show("Update failed");
            }
        }
    }
}

