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

        public static void AddModelParent(Parent parent1)
        {
            try
            {
                AddModel(parent1.Child);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            SqlConnection MainConnection = new SqlConnection(connectionstring);
            MainConnection.Open();
            string Command = "Insert Into [ParentTable] (Id,ChildId,Text) values ('" + parent1.Id + "','" + parent1.Child.Id + "','" + parent1.Text + "')";
            SqlCommand SqlCommand = new SqlCommand(Command, MainConnection);
            SqlCommand.ExecuteNonQuery();
            MainConnection.Close();
        }

        public static Child GetChildById(Guid Id)
        {
            SqlConnection MainConnection = new SqlConnection(connectionstring);
            MainConnection.Open();
            string Command = "Select Id, Text from [Table]" + "Where Id = '" + Id.ToString() + "'";
            SqlCommand SqlCommand = new SqlCommand(Command, MainConnection);
            var myReader = SqlCommand.ExecuteReader();
            List<Child> children2 = new List<Child>();
            while (myReader.Read())
            {
                var child2 = new Child();
                child2.Id = new Guid(myReader.GetValue(0).ToString());
                child2.Text = myReader.GetValue(1).ToString();
                children2.Add(child2);
            }
            MainConnection.Close();
            return children2.FirstOrDefault();
        }

        public static Parent GetParentById(Guid Id)
        {
            SqlConnection MainConnection = new SqlConnection(connectionstring);
            MainConnection.Open();
            string Command = "Select Id,ChildId, Text from [ParentTable]" + "Where Id = '" + Id.ToString() + "'";
            SqlCommand SqlCommand = new SqlCommand(Command, MainConnection);
            var myReader = SqlCommand.ExecuteReader();
            List<Parent> parents = new List<Parent>();
            while (myReader.Read())
            {
                var parent = new Parent();
                parent.Id = new Guid(myReader.GetValue(0).ToString());
                try
                {
                    parent.Child = GetChildById(new Guid(myReader.GetValue(1).ToString()));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                parent.Text = myReader.GetValue(2).ToString();
                parents.Add(parent);
            }
            MainConnection.Close();
            return parents.FirstOrDefault();
        }

        public static List<Parent> GetAllParents()
        {
            SqlConnection MainConnection = new SqlConnection(connectionstring);
            MainConnection.Open();
            string Command = "Select * from [ParentTable]";
            SqlCommand SqlCommand = new SqlCommand(Command, MainConnection);
            var myReader = SqlCommand.ExecuteReader();
            List<Parent> parents = new List<Parent>();
            while (myReader.Read())
            {
                var parent = new Parent();
                parent.Id = new Guid(myReader.GetValue(0).ToString());
                try
                {
                    parent.Child = GetChildById(new Guid(myReader.GetValue(1).ToString()));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                parent.Text = myReader.GetValue(2).ToString();
                parents.Add(parent);
            }
            MainConnection.Close();
            return parents;
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

        public static void DeleteChild(Child child)
        {
            SqlConnection MainConnection = new SqlConnection(connectionstring);
            MainConnection.Open();
            string Command = "Delete from [Table] where Id='" + child.Id + "'";
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


            var item = GetParentById(GetAllParents().FirstOrDefault().Id);

            Console.WriteLine($"id : {item.Id}  text : {item.Text} childid : {item.Child.Id}");

            //foreach (var item in children2)
            //{
            //    Console.WriteLine($"id : {item.Id}  text : {item.Text}");
            //}
        }
    }
}
