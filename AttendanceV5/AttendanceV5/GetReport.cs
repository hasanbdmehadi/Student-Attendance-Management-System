using AttendanceV5.Gateway;
using AttendanceV5.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
//using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using iTextSharp.text;
using iTextSharp.text.pdf;

namespace AttendanceV5
{
    public partial class GetReport : Form
    {
        public GetReport()
        {
            InitializeComponent();
        }


        ReportGateway rg = new ReportGateway();
        Report report = new Report();

        DataTable dtable = new DataTable();

        List<int> Row = new List<int>();
        public void ShowGridView(List<string> idlist, List<string> namelist, List<string> pcntlist, List<string> acntlist, List<string> percentlist, List<string> marklist)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Id", typeof(string));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("No of Present", typeof(string));
            dt.Columns.Add("No of Absent", typeof(string));
            dt.Columns.Add("Percentage", typeof(string));
            dt.Columns.Add("Marks Out of 10", typeof(string));

            int ind = 0;

            foreach (string id in idlist)
            {
                DataRow rw = dt.NewRow();
                rw[0] = id;
                rw[1] = namelist[ind];
                rw[2] = pcntlist[ind];
                rw[3] = acntlist[ind];
                rw[4] = percentlist[ind];
                rw[5] = marklist[ind];
                ind++;
                dt.Rows.Add(rw);
            }

            dtable.Clear();
            dtable = dt;
            dataGridView1.DataSource = dt;

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            //style.ForeColor = Color.Red;
            //style.BackColor = Color.Red;
            style.BackColor = System.Drawing.Color.Red;

            //dgVenta.Rows[0].Cells[0].Style = style;
            ind = 0;
            int len = Row.Count;
            foreach (DataGridViewRow item in dataGridView1.Rows)
            {
                for (int i = 0; i < len; i++)
                {
                    if (ind == Row[i])
                    {
                        dataGridView1.Rows[Row[i]].Cells[4].Style = style;
                    }
                }
                ind++;
            }


        }


        private void GetReport_Load(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = comboBox1.Text;
            if (s == "PGDIT")
            {
                comboBox2.Items.Clear();

                comboBox2.Items.Add("1st");
                comboBox2.Items.Add("2nd");
                comboBox2.Items.Add("3rd");
            }
            if (s == "PMIT")
            {
                comboBox2.Items.Clear();

                comboBox2.Items.Add("1st");
                comboBox2.Items.Add("2nd");
                comboBox2.Items.Add("3rd");
            }
            if (s == "M.Sc")
            {
                comboBox2.Items.Clear();

                comboBox2.Items.Add("1st");
                comboBox2.Items.Add("2nd");

            }
            if (s == "B.Sc")
            {
                comboBox2.Items.Clear();

                comboBox2.Items.Add("1st");
                comboBox2.Items.Add("2nd");
                comboBox2.Items.Add("3rd");
                comboBox2.Items.Add("4th");
                comboBox2.Items.Add("5th");
                comboBox2.Items.Add("6th");
                comboBox2.Items.Add("7th");
                comboBox2.Items.Add("8th");
            }
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
            report.Program = comboBox1.Text;
            report.Department = comboBox4.Text;
            report.Semester = comboBox2.Text;
            report.CourseCode = comboBox3.Text;
            report.StartDate = bunifuDatepicker1.Value.ToString("yyyy-MM-dd");
            report.EndDate = bunifuDatepicker2.Value.ToString("yyyy-MM-dd");

            int noc = rg.GetNumberOfClasses(report);

            List<string> idlist = new List<string>();
            idlist = rg.GetId(report);

            
            

            List<string> pcntlist = new List<string>();
            List<string> acntlist = new List<string>();
            List<string> percentlist = new List<string>();
            List<string> marklist = new List<string>();
            List<string> namelist = new List<string>();

            int ind = 0;
            Row.Clear();

            foreach (string id in idlist)
            {
                string name = rg.GetNameById(id);
                namelist.Add(name);

                int pcnt = rg.GetIdCount(id, report);
                pcntlist.Add(pcnt.ToString());

                int acnt = noc - pcnt;
                acntlist.Add(acnt.ToString());

                double percentage = (double)(pcnt * 100) / (double)noc;
                percentage = Math.Ceiling(percentage);
                if (percentage < 60)
                {
                    Row.Add(ind);
                }
                percentlist.Add(percentage.ToString() + "%");

                double mark = (double)(pcnt * 10) / (double)noc;
                mark = Math.Ceiling(mark);
                marklist.Add(mark.ToString());
                //MessageBox.Show("Id ="+id+" present ="+pcnt.ToString()+" percentage= "+percentage.ToString());
                ind++;

            }


            label9.Text = "Total Number of Class = " + noc.ToString();


            ShowGridView(idlist, namelist, pcntlist, acntlist, percentlist, marklist);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sem = comboBox2.Text;
            List<string> cCodes = new List<string>();
            cCodes = rg.GetCourseCodes(sem);
            foreach (string code in cCodes)
            {
                comboBox3.Items.Add(code);
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cCode = comboBox3.Text;
            bunifuMaterialTextbox1.Text = rg.GetCourseName(cCode);
            bunifuMaterialTextbox2.Text = rg.GetCourseCredit(cCode);
        }



        public bool ExportDataTableToPdf(DataTable dtblTable, String strPdfPath, string strHeader)
        {


            //System.IO.FileStream fs = new FileStream(strPdfPath, FileMode.Create, FileAccess.Write, FileShare.None);


            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save Report";
            sfd.Filter = "PDF |*.pdf";
            sfd.ShowDialog();
            using (FileStream fs = new FileStream(sfd.FileName + ".pdf", FileMode.Create))
            {

                Document document = new Document();
                document.SetPageSize(iTextSharp.text.PageSize.A4);
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.Open();



                //Report Header
                BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font fntHead = new Font(bfntHead, 16, 1, BaseColor.BLACK);
                Paragraph prgHeading = new Paragraph();
                prgHeading.Alignment = Element.ALIGN_CENTER;
                prgHeading.Add(new Chunk(strHeader.ToUpper(), fntHead));
                document.Add(prgHeading);

                //Date
                Paragraph prgAuthor = new Paragraph();
                BaseFont btnAuthor = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font fntAuthor = new Font(btnAuthor, 8, 2, BaseColor.GRAY);
                prgAuthor.Alignment = Element.ALIGN_RIGHT;
                //prgAuthor.Add(new Chunk("Author : Dotnet Mob", fntAuthor));
                prgAuthor.Add(new Chunk("\nRun Date : " + DateTime.Now.ToShortDateString(), fntAuthor));
                document.Add(prgAuthor);

                //Add a line seperation
                Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                document.Add(p);

                //Add line break
                document.Add(new Chunk("\n", fntAuthor));

                //Add Program
                BaseFont bfntProgram = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font fntProgram = new Font(bfntProgram, 10, 1, BaseColor.BLACK);
                Paragraph prgProg = new Paragraph();
                prgProg.Alignment = Element.ALIGN_LEFT;
                prgProg.Add(new Chunk("Program : " + report.Program, fntProgram));
                document.Add(prgProg);

                //Add Department
                BaseFont bfntDepartment = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font fntDepartment = new Font(bfntDepartment, 10, 1, BaseColor.BLACK);
                Paragraph prgDepartment = new Paragraph();
                prgDepartment.Alignment = Element.ALIGN_LEFT;
                prgDepartment.Add(new Chunk("Department : " + report.Department, fntDepartment));
                document.Add(prgDepartment);

                //Add Semester
                BaseFont bfntSemester = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font fntSemester = new Font(bfntSemester, 10, 1, BaseColor.BLACK);
                Paragraph prgSemester = new Paragraph();
                prgSemester.Alignment = Element.ALIGN_LEFT;
                prgSemester.Add(new Chunk("Semester : " + report.Semester, fntSemester));
                document.Add(prgSemester);

                //Add CourseName
                BaseFont bfntCourseName = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font fntCourseName = new Font(bfntCourseName, 10, 1, BaseColor.BLACK);
                Paragraph prgCourseName = new Paragraph();
                prgCourseName.Alignment = Element.ALIGN_LEFT;
                prgCourseName.Add(new Chunk("CourseName : " + bunifuMaterialTextbox1.Text, fntSemester));
                document.Add(prgCourseName);


                //Add DateRange
                BaseFont bfntDateRange = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font fntDateRange = new Font(bfntDateRange, 10, 1, BaseColor.BLACK);
                Paragraph prgDateRange = new Paragraph();
                prgDateRange.Alignment = Element.ALIGN_CENTER;
                prgDateRange.Add(new Chunk("From : " + bunifuDatepicker1.Value.ToString("dd-MM-yyyy") + "                       To : " + bunifuDatepicker2.Value.ToString("dd-MM-yyyy"), fntSemester));
                document.Add(prgDateRange);

                //Total number of class
                BaseFont bfnClass = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font fntClass = new Font(bfnClass, 10, 1, BaseColor.BLACK);
                Paragraph prgClass = new Paragraph();
                prgClass.Alignment = Element.ALIGN_CENTER;
                prgClass.Add(new Chunk(label9.Text));
                document.Add(prgClass);


                //Add line break
                document.Add(new Chunk("\n", fntDateRange));


                //Write the table
                PdfPTable table = new PdfPTable(dtblTable.Columns.Count);
                //Table header
                BaseFont btnColumnHeader = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                Font fntColumnHeader = new Font(btnColumnHeader, 10, 1, BaseColor.WHITE);
                for (int i = 0; i < dtblTable.Columns.Count; i++)
                {
                    PdfPCell cell = new PdfPCell();
                    cell.BackgroundColor = BaseColor.GRAY;
                    cell.AddElement(new Chunk(dtblTable.Columns[i].ColumnName.ToUpper(), fntColumnHeader));
                    table.AddCell(cell);
                }
                //table Data
                for (int i = 0; i < dtblTable.Rows.Count; i++)
                {
                    for (int j = 0; j < dtblTable.Columns.Count; j++)
                    {
                        table.AddCell(dtblTable.Rows[i][j].ToString());
                    }
                }

                document.Add(table);
                document.Close();
                writer.Close();
                fs.Close();
            }



            return true;
        }


        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            bool donePdf = ExportDataTableToPdf(dtable, @"E:\Test.pdf", "Summary Report");
            if (donePdf) MessageBox.Show("PDF Created");
            else MessageBox.Show("PDF creation process is terminated");
        }
    }
}
