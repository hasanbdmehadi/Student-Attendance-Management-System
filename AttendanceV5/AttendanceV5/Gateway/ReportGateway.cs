using AttendanceV5.UI;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceV5.Gateway
{
    class ReportGateway
    {
        string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;


        public List<string> GetCourseCodes(string sem)
        {
            List<string> list = new List<string>();

            SqlConnection con = new SqlConnection(cs);
            string q = "SELECT CourseCode FROM Course";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                string s = "";
                while (reader.Read())
                {
                    s = reader["CourseCode"].ToString();
                    list.Add(s);
                }
            }

            return list;
        }

        public string GetCourseName(string cCode)
        {
            string s = "";
            SqlConnection con = new SqlConnection(cs);
            string q = "SELECT CourseName FROM Course WHERE CourseCode = '" + cCode + "' ";

            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    s = reader["CourseName"].ToString();
                }
            }
            return s;
        }

        public string GetCourseCredit(string cCode)
        {
            string s = "";
            SqlConnection con = new SqlConnection(cs);
            string q = "SELECT CourseCredit FROM Course WHERE CourseCode = '" + cCode + "' ";

            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    s = reader["CourseCredit"].ToString();
                }
            }
            return s;
        }



        public int GetNumberOfClasses(Report report)
        {
            int cnt = 0;

            SqlConnection con = new SqlConnection(cs);
            string q = "select count(distinct Date) from Attendances where";
            q += " Date>='" + report.StartDate + "' and Date<='" + report.EndDate + "' and ";
            q += "Program = '" + report.Program + "' and Department = '" + report.Department + "' and ";
            q += "Semester = '" + report.Semester + "' and CourseCode = '" + report.CourseCode + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    cnt = reader.GetInt32(0);
                }
            }

            return cnt;

        }

        public List<string> GetId(Report report)
        {

            List<string> idlist = new List<string>();


            SqlConnection con = new SqlConnection(cs);
            string q = "select distinct StudentId from Attendances where";
            q += " Date>='" + report.StartDate + "' and Date<='" + report.EndDate + "' and ";
            q += "Program = '" + report.Program + "' and Department = '" + report.Department + "' and ";
            q += "Semester = '" + report.Semester + "' and CourseCode = '" + report.CourseCode + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    string id = reader["StudentId"].ToString();

                    idlist.Add(id);

                }
            }

            return idlist;

        }



        public List<string> GetName(Report report)
        {

            List<string> namelist = new List<string>();


            SqlConnection con = new SqlConnection(cs);
            string q = "select distinct StudentName from Attendances where";
            q += " Date>='" + report.StartDate + "' and Date<='" + report.EndDate + "' and ";
            q += "Program = '" + report.Program + "' and Department = '" + report.Department + "' and ";
            q += "Semester = '" + report.Semester + "' and CourseCode = '" + report.CourseCode + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    string name = reader["StudentName"].ToString();

                    namelist.Add(name);

                }
            }

            return namelist;

        }




        public int GetIdCount(string id, Report report)
        {
            int cnt = 0;
            string s = "Present";

            SqlConnection con = new SqlConnection(cs);
            string q = "select count(Date) from Attendances where";
            q += " Date>='" + report.StartDate + "' and Date<='" + report.EndDate + "' and ";
            q += "Program = '" + report.Program + "' and Department = '" + report.Department + "' and ";
            q += "Semester = '" + report.Semester + "' and CourseCode = '" + report.CourseCode + "' and StudentId = '" + id + "' and PorA = '" + s + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    cnt = reader.GetInt32(0);
                }
            }

            return cnt;

        }


        public string GetNameById(string id)
        {
            string name = "";

            SqlConnection con = new SqlConnection(cs);
            string q = "select StudentName from Student WHERE StudentId = '"+id+"'";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    name = reader["StudentName"].ToString();

                }
            }

            return name;
        }





    }
}
