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
    public partial class AddCourse : Form
    {
        public AddCourse()
        {
            InitializeComponent();
        }

        string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        Course course = new Course();

        CourseGateway cg = new CourseGateway();

        public void ShowGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string q = "select * from Course";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();

            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(q, con);
            sda.Fill(dt);

            dt.Columns.Remove("CourseId");
            dataGridView1.DataSource = dt;
            con.Close();
        }

        public void ShowGridViewBySearch(string cCode)
        {
            SqlConnection con = new SqlConnection(cs);
            string q = "select * from Course where CourseCode = '" + cCode + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();

            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(q, con);
            sda.Fill(dt);

            dt.Columns.Remove("CourseId");
            dataGridView1.DataSource = dt;
            con.Close();
        }
        public void ClearField()
        {
            bunifuMaterialTextbox1.Text = "";
            bunifuMaterialTextbox2.Text = "";
            bunifuMaterialTextbox3.Text = "";
            comboBox2.Text = "Select Department";
            
            
        }

        public void FillCourseCodeComboBox()
        {
            List<string> list = new List<string>();
            list = cg.GetCourseCodeList();
            comboBox1.Items.Clear();
            foreach (string s in list)
            {
                //MessageBox.Show("ase to");
                comboBox1.Items.Add(s);
                

            }
        }

        private void AddCourse_Load(object sender, EventArgs e)
        {
            ShowGridView();
            FillCourseCodeComboBox();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            course.CourseCode = bunifuMaterialTextbox1.Text;
            course.CourseName = bunifuMaterialTextbox2.Text;
            course.CourseCredit = (float)Convert.ToDouble(bunifuMaterialTextbox3.Text);
            course.Department = comboBox2.Text;

            string flag = cg.insert(course);
            MessageBox.Show(flag);
            ShowGridView();
            ClearField();
            FillCourseCodeComboBox();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            string cCode = bunifuMaterialTextbox1.Text;
            bool isSuccess = cg.delete(cCode);
            if (isSuccess == true) MessageBox.Show("Data deleted successfully");
            else MessageBox.Show("Data didn't deleted");
            ShowGridView();
            FillCourseCodeComboBox();
            ClearField();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            course.CourseCode = bunifuMaterialTextbox1.Text;
            course.CourseName = bunifuMaterialTextbox2.Text;
            course.CourseCredit = (float)Convert.ToDouble(bunifuMaterialTextbox3.Text);
            course.Department = comboBox2.Text;

            if (course.CourseCode != "" || course.CourseName != "" || course.Department != "")
            {
                bool flag = cg.update(course);
                if (flag == true) MessageBox.Show("Updated successfully");
                else MessageBox.Show("Not Updated");
            }
            else
            {
                MessageBox.Show("Select data from GridView");
            }

            ShowGridView();
            FillCourseCodeComboBox();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            bool isSuccess = cg.clear();
            if (isSuccess == true) MessageBox.Show("All the information cleared");
            else MessageBox.Show("Information exists");
            ShowGridView();
            FillCourseCodeComboBox();
            ClearField();
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            string cCode = comboBox1.Text;
            if (cCode == "")
            {
                MessageBox.Show("Select Course Code");
            }
            else
            {
                ShowGridViewBySearch(cCode);
                comboBox1.Text = "";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = e.RowIndex;
            bunifuMaterialTextbox1.Text = dataGridView1.Rows[RowIndex].Cells[0].Value.ToString();
            bunifuMaterialTextbox2.Text = dataGridView1.Rows[RowIndex].Cells[1].Value.ToString();
            bunifuMaterialTextbox3.Text = dataGridView1.Rows[RowIndex].Cells[2].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[RowIndex].Cells[3].Value.ToString();
        }
    }
}
