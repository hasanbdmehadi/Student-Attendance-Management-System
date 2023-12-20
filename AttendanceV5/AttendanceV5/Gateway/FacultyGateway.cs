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
    class FacultyGateway
    {
        string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        public bool CheckHas(Faculty faculty)
        {
            string s = faculty.id;
            SqlConnection con = new SqlConnection(cs);
            string q = "select * from Faculty where Id = '" + s + "'";
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

        public bool CheckShortNameHas(Faculty faculty)
        {
            string s = faculty.shortName;
            SqlConnection con = new SqlConnection(cs);
            string q = "select * from Faculty where ShortName = '" + s + "'";
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

        public string Insert(Faculty faculty)
        {
            bool chk = CheckHas(faculty);
            string flag = "";
            if (chk)
            {
                flag = "This information is already exist";
            }
            else
            {
                bool chksn = CheckShortNameHas(faculty);
                if (chksn)
                {
                    flag = "This Short Name is already taken. Try Another.";
                }
                else
                {
                    //Faculty Table insertion
                    SqlConnection con = new SqlConnection(cs);
                    string q = "insert into Faculty (Id, Name, ShortName, Password, Department, Pgdit, Pmit, Msc, Bsc, Phone) values ('" + faculty.id + "','" + faculty.name + "','" + faculty.shortName + "','" + faculty.password + "','" + faculty.department + "','" + faculty.Pgdit + "','" + faculty.Pmit + "','" + faculty.Msc + "','" + faculty.Bsc + "','"+faculty.phone+"')";
                    SqlCommand cmd = new SqlCommand(q, con);
                    con.Open();
                    int flag1 = cmd.ExecuteNonQuery();

                    if (flag1 > 0) flag = "Faculty Information is Saved";
                    else flag = "Faculty Information is not Saved";
                }


            }

            return flag;

        }

        public bool Update(Faculty faculty)
        {


            SqlConnection con = new SqlConnection(cs);
            string q = "UPDATE Faculty SET Name = '" + faculty.name + "', ShortName = '" + faculty.shortName + "', Password = '" + faculty.password + "', Pgdit = '" + faculty.Pgdit + "', Pmit = '" + faculty.Pmit + "', Msc = '" + faculty.Msc + "', Bsc = '" + faculty.Bsc + "' Where Id = '" + faculty.id + "' ";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            int rowAffected = cmd.ExecuteNonQuery();

            if (rowAffected > 0) return true;
            else return false;

        }

        public bool Delete(string fId)
        {

            SqlConnection con = new SqlConnection(cs);
            string q = "DELETE FROM Faculty WHERE Id = '" + fId + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            int rowAffected = cmd.ExecuteNonQuery();
            if (rowAffected > 0) return true;
            else return false;

        }

        public bool Clear()
        {

            SqlConnection con = new SqlConnection(cs);
            string q = "DELETE FROM Faculty";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            int rowAffected = cmd.ExecuteNonQuery();
            if (rowAffected > 0) return true;
            else return false;

        }

        public List<string> GetFacultyIdList()
        {
            List<string> list = new List<string>();

            SqlConnection con = new SqlConnection(cs);
            string q = "SELECT Id FROM Faculty";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                string s = "";
                while (reader.Read())
                {
                    s = reader["Id"].ToString();
                    list.Add(s);
                }
            }

            return list;
        }



        public List<string> GetFacultyId()
        {
            List<string> list = new List<string>();

            SqlConnection con = new SqlConnection(cs);
            string q = "SELECT Id FROM Faculty";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                string s = "";
                while (reader.Read())
                {
                    s = reader["Id"].ToString();
                    list.Add(s);
                }
            }

            return list;
        }

        public string GetFacultyProgramById(string id)
        {
            string programs = "";

            SqlConnection con = new SqlConnection(cs);
            string q = "SELECT ProgramName FROM ProgramFaculty WHERE FId = '" + id + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {

                string s = "";
                while (reader.Read())
                {
                    s = reader["ProgramName"].ToString();
                    programs += s;
                    //Console.WriteLine(programs);

                }
            }

            return programs;
        }

        public List<string> GetFacultyInfoWithoutId(string id)
        {
            List<string> list = new List<string>();

            SqlConnection con = new SqlConnection(cs);
            string q = "SELECT * FROM Faculty WHERE Id = '" + id + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows)
            {
                string s = "";
                while (reader.Read())
                {
                    s = reader["Name"].ToString();
                    list.Add(s);
                    s = reader["ShortName"].ToString();
                    list.Add(s);
                    s = reader["Password"].ToString();
                    list.Add(s);
                    s = reader["Department"].ToString();
                    list.Add(s);
                    //Console.WriteLine(s);
                    s = GetFacultyProgramById(id);
                    list.Add(s);
                }
            }


            return list;
        }
    }
}
