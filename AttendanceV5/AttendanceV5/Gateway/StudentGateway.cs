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
    class StudentGateway
    {
        string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;


        public bool CheckHas(Student student)
        {
            int id = student.StudentId;
            SqlConnection con = new SqlConnection(cs);
            string q = "select * from Student where StudentId = " + id + "";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                con.Close();
                return true;
            }
            else
            {
                con.Close();
                return false;
            }
        }



        public string Insert(Student student)
        {
            string flag = "";
            if (CheckHas(student))
            {
                flag = "This Student is already exist";
            }
            else
            {
                SqlConnection con = new SqlConnection(cs);
                string q = "insert into Student (StudentId, StudentName, StudentAddress, StudentDepartment, StudentSemester, StudentProgram, GuardianPhone) values (" + student.StudentId + ",'" + student.StudentName + "', '" + student.StudentAddress + "','" + student.StudentDepartment + "','" + student.StudentSemester + "','" + student.StudentProgram + "','"+student.GuardianPhone+"')";
                SqlCommand cmd = new SqlCommand(q, con);
                con.Open();
                int reader = cmd.ExecuteNonQuery();
                if (reader > 0) flag = "Information is saved";
                else flag = "Couldn't save";
            }
            return flag;
        }

        public bool Delete(string sId)
        {

            SqlConnection con = new SqlConnection(cs);
            string q = "DELETE FROM Student WHERE StudentId = '" + sId + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            int rowAffected = cmd.ExecuteNonQuery();
            if (rowAffected > 0) return true;
            else return false;

        }

        public bool Update(Student student)
        {

            SqlConnection con = new SqlConnection(cs);
            string q = "UPDATE Student SET StudentName = '" + student.StudentName + "', StudentAddress = '" + student.StudentAddress + "' ,StudentDepartment = '" + student.StudentDepartment + "', StudentSemester = '" + student.StudentSemester + "',StudentProgram = '" + student.StudentProgram + "',GuardianPhone = '"+student.GuardianPhone+"' Where StudentId = " + student.StudentId + " ";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            int rowAffected = cmd.ExecuteNonQuery();

            if (rowAffected > 0) return true;
            else return false;

        }

        public bool Clear()
        {

            SqlConnection con = new SqlConnection(cs);
            string q = "DELETE FROM Student";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            int rowAffected = cmd.ExecuteNonQuery();
            if (rowAffected > 0) return true;
            else return false;

        }
    }
}
