using System;
using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataSource
{
    public  class Data
    {

        public  static Data instance;// need to make private

        private static MongoClient client;

        private static IMongoDatabase Mongo_Database;
        private static IMongoCollection<BsonDocument> Mongo_Collection;

        public  IMongoCollection<BsonDocument> CurrentCollection
        {
            get { return Mongo_Collection; }

            internal set
            {
                Mongo_Collection = value;
            }
        }

        public  IMongoDatabase CurrentDatabase
        {
            get { return Mongo_Database; }

            internal set
            {
                Mongo_Database = value;
            }
        }
    

        static readonly string connectingString = "mongodb://Admin:<PASSWORD>@iot-ai-shard-00-00-rjyq3.mongodb.net:27017" +
                                                  ",iot-ai-shard-00-01-rjyq3.mongodb.net:27017" +
                                                  ",iot-ai-shard-00-02-rjyq3.mongodb.net:27017" +
                                                  "/application-data" +
                                                  "?ssl=true&replicaSet=IoT-AI-shard-0&authSource=admin&retryWrites=true";

        private Data()
        {
            client = Connect();
            if (client is null)
                Console.WriteLine("Could not connect to database.");
            else
            {
               
                CurrentDatabase = this["application-data"]; //Default database
                CurrentCollection = this["user-data", Mongo_Database]; //Default collection

                Console.WriteLine("Connected to " + " >> " + CurrentCollection.CollectionNamespace);
                
            }
               
        }


       public static Data GetDatabase()
        {
            if (instance is null)
                instance = new Data();

            return instance;
            
        }


       private MongoClient Connect()
        {
            client = new MongoClient(connectingString);

            if (client is null)
            {
                Console.WriteLine("User/password is incorrect.");
                client = null;// Need to change.
            }

            return client;
        }

       public void add(string collectionName, IDictionary contents, string databaseName = "application-data")
        {
            try
            {
                IMongoDatabase db = client.GetDatabase(databaseName);

                IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>(collectionName);

                BsonDocument doc = new BsonDocument(contents);

                collection.InsertOne(doc);

                Console.WriteLine("Added given dictionary to " + collectionName);

            }
            catch (Exception ex)
            {

                Console.WriteLine("Could not add given dictionary. Error : " + ex.Message);
            }

        }

       public void Print(string conversion )//Dont need this method
        {
            if (conversion == "database")
            {
                foreach (var item in getDatabaseList())
                {
                    Console.WriteLine(item.ToJson());
                }
            }

            if (conversion == "collection")
            {
                foreach (var item in getCollectionList(this["application-data"]))
                {
                    Console.WriteLine(item.ToJson());
                }
            }

          
        }

        public IMongoDatabase this[string database]
        {
            get
            {
                CurrentDatabase = client.GetDatabase(database);
                return CurrentDatabase;
            }
        }

        public IMongoCollection<BsonDocument> this[string collection, IMongoDatabase database ]
        {
            get
            {

                CurrentDatabase = database;
                CurrentCollection = CurrentDatabase.GetCollection<BsonDocument>(collection);

                return CurrentCollection;
            }
        }

        public BsonDocument this[string query , IMongoCollection<BsonDocument> collection ]
        {
            get
            {

                CurrentCollection = collection;

                return CurrentCollection.Find(query).ToBsonDocument();
            }
        }

        static public IList getCollectionList(IMongoDatabase database) => database.ListCollections().ToList();

        static public IList getDatabaseList() => client.ListDatabases().ToList();
    }
}
