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
    public partial class AddFaculty : Form
    {
        public AddFaculty()
        {
            InitializeComponent();
        }

        string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        Faculty faculty = new Faculty();
        Faculty tfaculty = new Faculty();
        FacultyGateway fg = new FacultyGateway();

        public void SetInitialValue()
        {
            tfaculty.id = bunifuMaterialTextbox1.Text;
            tfaculty.name = bunifuMaterialTextbox2.Text;
            tfaculty.shortName = bunifuMaterialTextbox3.Text;
            tfaculty.password = bunifuMaterialTextbox4.Text;
            tfaculty.department = comboBox2.Text;
            tfaculty.Pgdit = "NO";
            tfaculty.Pmit = "NO";
            tfaculty.Msc = "NO";
            tfaculty.Bsc = "NO";
            if (checkBox1.Checked) tfaculty.Pgdit = "YES";
            if (checkBox2.Checked) tfaculty.Pmit = "YES";
            if (checkBox3.Checked) tfaculty.Msc = "YES";
            if (checkBox4.Checked) tfaculty.Bsc = "YES";
        }

        public void ShowGridView()
        {
            SqlConnection con = new SqlConnection(cs);
            string q = "select * from Faculty";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();

            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(q, con);
            sda.Fill(dt);
            dt.Columns.Remove("Path");
            dt.Columns.Remove("Username");

            dataGridView1.DataSource = dt;
            con.Close();

        }

        public void ClearField()
        {
            checkBox1.Checked = false;
            checkBox2.Checked = false;
            checkBox3.Checked = false;
            checkBox4.Checked = false;
            bunifuMaterialTextbox1.Text = "";
            bunifuMaterialTextbox2.Text = "";
            bunifuMaterialTextbox3.Text = "";
            bunifuMaterialTextbox4.Text = "";
            comboBox2.Text = "Select Department";

        }

        public void ShowGridViewBySearch(string id)
        {
            SqlConnection con = new SqlConnection(cs);
            string q = "select * from Faculty where Id = '" + id + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();

            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(q, con);
            sda.Fill(dt);

            dataGridView1.DataSource = dt;
            con.Close();
        }

        public void FillIdComboBox()
        {
            List<string> list = new List<string>();
            list = fg.GetFacultyIdList();
            foreach (string s in list)
            {

                comboBox1.Items.Add(s);
            }
        }

        private void AddFaculty_Load(object sender, EventArgs e)
        {
            FillIdComboBox();
            ShowGridView();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            faculty.id = bunifuMaterialTextbox1.Text;
            faculty.name = bunifuMaterialTextbox2.Text;
            faculty.shortName = bunifuMaterialTextbox3.Text;
            faculty.password = bunifuMaterialTextbox4.Text;
            faculty.department = comboBox2.Text;
            faculty.phone = PhoneTextbox.Text;

            faculty.Pgdit = "NO";
            if (checkBox1.Checked) faculty.Pgdit = "YES";
            faculty.Pmit = "NO";
            if (checkBox2.Checked) faculty.Pmit = "YES";
            faculty.Msc = "NO";
            if (checkBox3.Checked) faculty.Msc = "YES";
            faculty.Bsc = "NO";
            if (checkBox4.Checked) faculty.Bsc = "YES";

            string isSaved = fg.Insert(faculty);
            MessageBox.Show(isSaved);

            ShowGridView();
            ClearField();
            FillIdComboBox();
            
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            string fId = bunifuMaterialTextbox1.Text;
            bool isSuccess = fg.Delete(fId);
            if (isSuccess == true) MessageBox.Show("Data deleted successfully");
            else MessageBox.Show("Data didn't deleted");
            ShowGridView();
            ClearField();
            FillIdComboBox();
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            faculty.id = bunifuMaterialTextbox1.Text;
            faculty.name = bunifuMaterialTextbox2.Text;
            faculty.shortName = bunifuMaterialTextbox3.Text;
            faculty.password = bunifuMaterialTextbox4.Text;
            faculty.department = comboBox2.Text;
            faculty.Pgdit = "NO";
            faculty.Pmit = "NO";
            faculty.Msc = "NO";
            faculty.Bsc = "NO";
            if (checkBox1.Checked) faculty.Pgdit = "YES";
            if (checkBox2.Checked) faculty.Pmit = "YES";
            if (checkBox3.Checked) faculty.Msc = "YES";
            if (checkBox4.Checked) faculty.Bsc = "YES";


            if (tfaculty.id != faculty.id || tfaculty.name != faculty.name || tfaculty.shortName != faculty.shortName || tfaculty.password != faculty.password || tfaculty.department != faculty.department || tfaculty.Pgdit != faculty.Pgdit || tfaculty.Pmit != faculty.Pmit || tfaculty.Msc != faculty.Msc || tfaculty.Bsc != faculty.Bsc)
            {
                bool flag = fg.Update(faculty);
                if (flag == true) MessageBox.Show("Updated successfully");
                else MessageBox.Show("Not Updated");
            }
            else
            {
                MessageBox.Show("Select data from GridView");
            }

            ShowGridView();
            ClearField();
            FillIdComboBox();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            bool isSuccess = fg.Clear();
            if (isSuccess == true) MessageBox.Show("All the information cleared");
            else MessageBox.Show("Information exists");
            ShowGridView();
            ClearField();
        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
            string id = comboBox1.Text;
            if (id == "")
            {
                MessageBox.Show("Select Course Code");
            }
            else
            {
                ShowGridViewBySearch(id);
                comboBox1.Text = "";
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int RowIndex = e.RowIndex;
            bunifuMaterialTextbox1.Text = dataGridView1.Rows[RowIndex].Cells[0].Value.ToString();
            bunifuMaterialTextbox2.Text = dataGridView1.Rows[RowIndex].Cells[1].Value.ToString();
            bunifuMaterialTextbox3.Text = dataGridView1.Rows[RowIndex].Cells[2].Value.ToString();
            bunifuMaterialTextbox4.Text = dataGridView1.Rows[RowIndex].Cells[3].Value.ToString();
            comboBox2.Text = dataGridView1.Rows[RowIndex].Cells[4].Value.ToString();

            string s = dataGridView1.Rows[RowIndex].Cells[5].Value.ToString();
            if (s == "YES") checkBox1.Checked = true;
            if (s == "NO") checkBox1.Checked = false;

            s = dataGridView1.Rows[RowIndex].Cells[6].Value.ToString();
            if (s == "YES") checkBox2.Checked = true;
            if (s == "NO") checkBox2.Checked = false;

            s = dataGridView1.Rows[RowIndex].Cells[7].Value.ToString();
            if (s == "YES") checkBox3.Checked = true;
            if (s == "NO") checkBox3.Checked = false;

            s = dataGridView1.Rows[RowIndex].Cells[8].Value.ToString();
            if (s == "YES") checkBox4.Checked = true;
            if (s == "NO") checkBox4.Checked = false;

            SetInitialValue();
        }
    }
}
