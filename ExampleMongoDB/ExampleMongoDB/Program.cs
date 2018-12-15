
using DataSource; // Add using directive.
using System.Collections.Generic;

namespace ExampleMongoDB
{
    class Program
    {
        static void Main(string[] args)
        {
            Data Source = Data.GetDatabase(); // Get the Data instance (singleton) which will 
                                               // connect us to the IoT-AI cluster
                                               // I will add ways to change the connection string 
                                               // so you can connect to your own database even on localhost
                                               // I will also change GetDatabase to GetCluster

            var Database = Source.CurrentDatabase; // Returns current default Database => application-data

            var collection = Source.CurrentCollection; // Returns current default Database => user-data

            Collection Collection = new Collection();

            var NewCollection = Collection["student-data-ml"]; // Returns a new collection given a string in the 
                                                               // current database

            Document Document = new Document();

            string query = "{age : 15}";

            var document = Document[query]; //Returns a document (Bson) with the query from the current collection
                                            // You can also use a Filter eg. Document[Filter]

            Dictionary<string, string> Student = new Dictionary<string, string>(5);
            Student.Add("Name", "Kenneth");
            Student.Add("Name", "Piet");

            Source.add("student-data-ml",Student); //adds a dictionary to the student-data-ml


            // To do - add ways to add using Bson Document maybe even json
            //       - add ways to create new collections and databases
            //       - please edit here if you want any other features!
           

        }
    }
}
