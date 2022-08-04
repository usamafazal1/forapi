using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Data.SqlClient;
using System.Configuration;
namespace forapi
{
    public class Students
    {
 
        public string Name { get; set; }
        public int ID { get; set; }
        public string Role { get; set; }

        public Students(string name, int id, string role)
        {
            Name = name;
            ID = id;
            Role = role;
        }

        public List<Students> StudentsList()
        {

            string connectionString = ConfigurationManager.ConnectionStrings["ProjectDB"].ConnectionString; ;
            SqlConnection cnn;

            cnn = new SqlConnection(connectionString);
            cnn.Open();
            SqlCommand command;
            SqlDataReader dataReader;
            string sql = "Select * From STAFF";
            command = new SqlCommand(sql, cnn);
            dataReader = command.ExecuteReader();
            List<Students> accountsList = new List<Students>(); //list object of type students//
            while (dataReader.Read()) //reads line by line//
            {
                string Name = dataReader.GetValue(0).ToString(); //reads first column//
                int ID = Convert.ToInt32(dataReader.GetValue(1));             
                string Role = dataReader.GetValue(2).ToString(); //reads third column//
                Students student = new Students(Name, ID, Role);
                accountsList.Add(student); //account list has all the student objects added//
            }
            dataReader.Close();
            command.Dispose();
            cnn.Close();
            return accountsList;
        }
    }
}