using AttendanceV5.Gateway;
using AttendanceV5.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AttendanceV5
{
    public partial class AddStudent : Form
    {
        public AddStudent()
        {
            InitializeComponent();
        }


        string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        Student student = new Student();
        Student tstudent = new Student();
        StudentGateway sg = new StudentGateway();

        public void SetInitialValue()
        {
            tstudent.StudentId = Convert.ToInt32(StudentIdtextBox.Text);
            tstudent.StudentName = StudentNametextBox.Text;
            tstudent.StudentAddress = StudentAddresstextBox.Text;
            tstudent.StudentDepartment = StudentDepartmentcomboBox.Text;
            tstudent.StudentSemester = StudentDepartmentcomboBox.Text;
            tstudent.GuardianPhone = PhoneTextbox.Text;

            if (radioButton1.Checked == true) tstudent.StudentProgram = "PGDIT";
            if (radioButton2.Checked == true) tstudent.StudentProgram = "PMIT";
            if (radioButton3.Checked == true) tstudent.StudentProgram = "M.Sc";
            if (radioButton4.Checked == true) tstudent.StudentProgram = "B.Sc";

        }

        public void ClearField()
        {
            StudentIdtextBox.Text = "";
            StudentNametextBox.Text = "";
            StudentAddresstextBox.Text = "";
            StudentDepartmentcomboBox.Text = "Select Department";
            StudentSemestercomboBox.Text = "Select Semeseter";
            PhoneTextbox.Text = "";

            if (radioButton1.Checked == true) radioButton1.Checked = false;
            if (radioButton2.Checked == true) radioButton2.Checked = false;
            if (radioButton3.Checked == true) radioButton3.Checked = false;
            if (radioButton4.Checked == true) radioButton4.Checked = false;
        }


        public void ShowGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string q = "select * from Student";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();

            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(q, con);
            sda.Fill(dt);


            dataGridView1.DataSource = dt;
            con.Close();

        }
        
        private void label6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuMaterialTextbox1_OnValueChanged(object sender, EventArgs e)
        {

        }

        private void bunifuDropdown1_onItemSelected(object sender, EventArgs e)
        {

        }

        private void AddStudent_Load(object sender, EventArgs e)
        {
            ShowGridView();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            student.StudentId = Convert.ToInt32(StudentIdtextBox.Text);
            student.StudentName = StudentNametextBox.Text;
            student.StudentAddress = StudentAddresstextBox.Text;
            student.StudentDepartment = StudentDepartmentcomboBox.Text;
            student.StudentSemester = StudentSemestercomboBox.Text;
            student.GuardianPhone = PhoneTextbox.Text;

            if (radioButton1.Checked) student.StudentProgram = "PGDIT";
            if (radioButton2.Checked) student.StudentProgram = "PMIT";
            if (radioButton3.Checked) student.StudentProgram = "M.Sc";
            if (radioButton4.Checked) student.StudentProgram = "B.Sc";


            if (student.StudentId != tstudent.StudentId || student.StudentName != tstudent.StudentName || student.StudentAddress != tstudent.StudentAddress || student.StudentDepartment != tstudent.StudentDepartment || student.StudentSemester != tstudent.StudentSemester || student.StudentSemester != tstudent.StudentSemester || student.GuardianPhone != tstudent.GuardianPhone)
            {
                bool flag = sg.Update(student);
                if (flag == true) MessageBox.Show("Updated successfully");
                else MessageBox.Show("Not Updated");
            }
            else
            {
                MessageBox.Show("Select data from GridView");
            }

            ShowGridView();
            ClearField();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = e.RowIndex;
            StudentIdtextBox.Text = dataGridView1.Rows[RowIndex].Cells[0].Value.ToString();
            StudentNametextBox.Text = dataGridView1.Rows[RowIndex].Cells[1].Value.ToString();
            StudentAddresstextBox.Text = dataGridView1.Rows[RowIndex].Cells[2].Value.ToString();
            StudentDepartmentcomboBox.Text = dataGridView1.Rows[RowIndex].Cells[3].Value.ToString();
            StudentSemestercomboBox.Text = dataGridView1.Rows[RowIndex].Cells[4].Value.ToString();

            string s = dataGridView1.Rows[RowIndex].Cells[5].Value.ToString();
            if (s == "PGDIT") radioButton1.Checked = true;
            if (s == "PMIT") radioButton1.Checked = true;
            if (s == "M.Sc") radioButton1.Checked = true;
            if (s == "B.Sc") radioButton1.Checked = true;

            SetInitialValue();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            student.StudentId = Convert.ToInt32(StudentIdtextBox.Text);
            student.StudentName = StudentNametextBox.Text;
            student.StudentAddress = StudentAddresstextBox.Text;
            student.StudentDepartment = StudentDepartmentcomboBox.Text;
            student.StudentSemester = StudentSemestercomboBox.Text;
            student.GuardianPhone = PhoneTextbox.Text;

            if (radioButton1.Checked) student.StudentProgram = "PGDIT";
            if (radioButton2.Checked) student.StudentProgram = "PMIT";
            if (radioButton3.Checked) student.StudentProgram = "M.Sc";
            if (radioButton4.Checked) student.StudentProgram = "B.Sc";



            string flag = sg.Insert(student);
            MessageBox.Show(flag);
            ShowGridView();
            ClearField();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            string sId = StudentIdtextBox.Text;
            bool isSuccess = sg.Delete(sId);
            if (isSuccess == true) MessageBox.Show("Data deleted successfully");
            else MessageBox.Show("Data didn't deleted");
            ShowGridView();
            ClearField();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            bool isSuccess = sg.Clear();
            if (isSuccess == true) MessageBox.Show("All the information cleared");
            else MessageBox.Show("Information exists");
            ShowGridView();
            ClearField();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = e.RowIndex;
            StudentIdtextBox.Text = dataGridView1.Rows[RowIndex].Cells[0].Value.ToString();
            StudentNametextBox.Text = dataGridView1.Rows[RowIndex].Cells[1].Value.ToString();
            StudentAddresstextBox.Text = dataGridView1.Rows[RowIndex].Cells[2].Value.ToString();
            StudentDepartmentcomboBox.Text = dataGridView1.Rows[RowIndex].Cells[3].Value.ToString();
            StudentSemestercomboBox.Text = dataGridView1.Rows[RowIndex].Cells[4].Value.ToString();
            PhoneTextbox.Text = dataGridView1.Rows[RowIndex].Cells[6].Value.ToString();

            string s = dataGridView1.Rows[RowIndex].Cells[5].Value.ToString();
           // MessageBox.Show(s);
            if (s == "PGDIT") radioButton1.Checked = true;
            if (s == "PMIT") radioButton2.Checked = true;
            if (s == "M.Sc") radioButton3.Checked = true;
            if (s == "B.Sc") radioButton4.Checked = true;

            SetInitialValue();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            StudentSemestercomboBox.Items.Clear();

            //for pmit and pgdit
            StudentSemestercomboBox.Items.Add("1st");
            StudentSemestercomboBox.Items.Add("2nd");
            StudentSemestercomboBox.Items.Add("3rd");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            StudentSemestercomboBox.Items.Clear();

            //for pmit and pgdit
            StudentSemestercomboBox.Items.Add("1st");
            StudentSemestercomboBox.Items.Add("2nd");
            StudentSemestercomboBox.Items.Add("3rd");
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            StudentSemestercomboBox.Items.Clear();
            //for msc
            StudentSemestercomboBox.Items.Add("1st");
            StudentSemestercomboBox.Items.Add("2nd");
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            StudentSemestercomboBox.Items.Clear();

            //for Bsc
            StudentSemestercomboBox.Items.Add("1st");
            StudentSemestercomboBox.Items.Add("2nd");
            StudentSemestercomboBox.Items.Add("3rd");
            StudentSemestercomboBox.Items.Add("4th");
            StudentSemestercomboBox.Items.Add("5th");
            StudentSemestercomboBox.Items.Add("6th");
            StudentSemestercomboBox.Items.Add("7th");
            StudentSemestercomboBox.Items.Add("8th");
        }
    }
}
