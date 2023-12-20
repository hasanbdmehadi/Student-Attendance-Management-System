using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using AttendanceV5.Gateway;
using AttendanceV5.UI;


namespace AttendanceV5
{
    public partial class FacultyProfile : Form
    {
        public FacultyProfile()
        {
            InitializeComponent();
            
        }

        //public static Image facultyImage = null;

        FacultyProfileGateway fpg = new FacultyProfileGateway();
        Faculty faculty = new Faculty();

        string imgLocation = "";

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "jpg files(*.jpg)|*.jpg|All files(*.*)|*.*";
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                imgLocation = dialog.FileName.ToString();
                pictureBox1.ImageLocation = imgLocation;
            }

        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            byte[] image = null;
            FileStream fs = new FileStream(imgLocation, FileMode.Open,FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);
            image = br.ReadBytes((int)fs.Length);
            fs.Close();
            br.Close();

            faculty.id = label7.Text;
            faculty.name = bunifuMaterialTextbox2.Text;
            faculty.shortName = bunifuMaterialTextbox3.Text;
            faculty.username = bunifuMaterialTextbox5.Text;
            faculty.password = bunifuMaterialTextbox4.Text;
            faculty.image = image;
           // faculty.phone = bunifuMaterialTextbox1.Text;

           // bool isUpdate = fpg.Update(faculty, image);
          //  if (isUpdate) MessageBox.Show("Updated Successfully");
          //  else MessageBox.Show("Something went wrong, Try again");

           

        }

        public void ShowFacultyInfo(string fid)
        {
            Faculty faculty2 = new Faculty();
            faculty2 = fpg.GetFacultyInformation(fid);

            bunifuMaterialTextbox2.Text = faculty2.name;
            bunifuMaterialTextbox3.Text = faculty2.shortName;
            bunifuMaterialTextbox4.Text = faculty2.password;
            bunifuMaterialTextbox5.Text = faculty2.username;

            byte[] img = faculty2.image;

            MemoryStream ms = new MemoryStream(img);

            pictureBox1.Image = Image.FromStream(new MemoryStream(img));

            ms.Close();

            //facultyImage = pictureBox1.Image;


            //MemoryStream stream = new MemoryStream(img);
            //var newImage = System.Drawing.Image.FromStream(stream);
            //MessageBox.Show(newImage.ToString());
            //stream.Dispose();
            //pictureBox1.Image = newImage;
            
           

        }

        private void FacultyProfile_Load(object sender, EventArgs e)
        {
            label7.Text = Form1.fid;
           //label7.Text = "12132103018";
            ShowFacultyInfo(label7.Text);
            
        }
    }
}
