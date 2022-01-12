using Raw_SQL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Raw_SQL
{
    class Program
    {
        public static string connectionstring = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=RawSqlDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static void AddModel(Child child1)
        {
            SqlConnection MainConnection = new SqlConnection(connectionstring);
            MainConnection.Open();
            string Command = "Insert Into [Table] (Id,Text) values ('" + child1.Id + "','" + child1.Text + "')";
            SqlCommand SqlCommand = new SqlCommand(Command, MainConnection);
            SqlCommand.ExecuteNonQuery();
            MainConnection.Close();
        }

        public static List<Child> GetAllChildren()
        {
            SqlConnection MainConnection = new SqlConnection(connectionstring);
            MainConnection.Open();
            string Command = "Select * from [Table]";
            SqlCommand SqlCommand = new SqlCommand(Command, MainConnection);
            var myReader = SqlCommand.ExecuteReader();
            List<Child> children = new List<Child>();
            while (myReader.Read())
            {
                var child2 = new Child();
                child2.Id = new Guid(myReader.GetValue(0).ToString());
                child2.Text = myReader.GetValue(1).ToString();
                children.Add(child2);
            }
            MainConnection.Close();
            return children;
        }

        public static void UpdateChild(Child child, string textvalue)
        {
            SqlConnection MainConnection = new SqlConnection(connectionstring);
            MainConnection.Open();
            string Command = "Update [Table] set Text = '" + textvalue + "' where Id = '" + child.Id + "'";
            SqlCommand SqlCommand = new SqlCommand(Command, MainConnection);
            SqlCommand.ExecuteNonQuery();
            MainConnection.Close();
        }
        static void Main(string[] args)
        {
            var child1 = new Child()
            {
                Text = "From Child"
            };

            var parent1 = new Parent()
            {
                Child = child1,
                Text = "From Parent"
            };

            var grandparent1 = new GrandParent()
            {
                Parents = new System.Collections.Generic.List<Parent>()
                {
                    parent1
                },
                Text = "From GrandParent"
            };
            SqlConnection MainConnection = new SqlConnection(connectionstring);
            MainConnection.Open();
            var child = GetAllChildren().FirstOrDefault();
            string Command = "Delete from [Table] where ";
            SqlCommand SqlCommand = new SqlCommand(Command, MainConnection);
            SqlCommand.ExecuteNonQuery();
            MainConnection.Close();
            foreach (var item in children)
            {
                Console.WriteLine($"id : {item.Id}  text : {item.Text}");
            }
        }
    }
}
