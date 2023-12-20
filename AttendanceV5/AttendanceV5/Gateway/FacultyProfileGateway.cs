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
    class FacultyProfileGateway
    {
        string cs = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        FacultyGateway fg = new FacultyGateway();

        public bool Update(Faculty faculty)
        {
            //bool isHasShort = fg.CheckShortNameHas(faculty);
            //if (isHasShort) return false;


            SqlConnection con = new SqlConnection(cs);
            string q = "UPDATE Faculty SET Name = '" + faculty.name + "', ShortName = '" + faculty.shortName + "', Password = '" + faculty.password + "', Username = '" + faculty.username + "' , Path = '" + faculty.path + "' Where Id = '" + faculty.id + "' ";
            SqlCommand cmd = new SqlCommand(q, con);
            //cmd.CommandTimeout = 1000;
            con.Open();
            //cmd.Parameters.AddWithValue("@img",faculty.image);
            //cmd.Parameters.Add(new SqlParameter("@img", iimmgg));
            int rowAffected = cmd.ExecuteNonQuery();
            con.Close();
            if (rowAffected > 0) return true;
            else return false;

        }
        public Faculty GetFacultyInformation(string fid)
        {
            Faculty faculty = new Faculty();

            SqlConnection con = new SqlConnection(cs);
            string q = "select * from Faculty where Id = '" + fid + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    faculty.name = reader["Name"].ToString();
                    faculty.shortName = reader["ShortName"].ToString();
                    faculty.password = reader["Password"].ToString();
                    faculty.username = reader["Username"].ToString();
                    faculty.path = reader["Path"].ToString();

                }
            }
            con.Close();
            return faculty;
        }

        public byte[] GetFacultyImage(string fid)
        {
            byte[] img = null;

            SqlConnection con = new SqlConnection(cs);
            string q = "select * from Faculty where Id = '" + fid + "'";
            SqlCommand cmd = new SqlCommand(q, con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {

                while (reader.Read())
                {
                    img = (byte[])reader["Image"];
                }
            }
            con.Close();
            return img;
        }
    }
}
