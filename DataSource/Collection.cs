
using MongoDB.Bson;
using MongoDB.Driver;

namespace DataSource
{
    public class Collection
    {
       public IMongoCollection<BsonDocument> this[string collection]
        {
            
            get
            {
                Data data = Data.GetDatabase();
                IMongoCollection<BsonDocument>  collection_ = data.CurrentDatabase.GetCollection<BsonDocument>(collection);
                data.CurrentCollection = collection_;
                
                return collection_;
            }
        }
    }

    public class Document //Add to own file.
    {
        public BsonDocument this[string query]
        {

            get
            {
                FilterDefinition<BsonDocument> filter = null;
                Data data = Data.GetDatabase();
                if (query.Contains("ObjectId"))
                {
                    filter = Builders<BsonDocument>.Filter.Eq("id", ObjectId.Parse("5c0ef22221ae6515dc1fe880")); //Need to change.
                    return data.CurrentCollection.Find(filter).ToBsonDocument();
                }
                else
                {
                    return data.CurrentCollection.Find(query).ToBsonDocument();
                }
               
               
              
            }
        }

        public BsonDocument this[FilterDefinition<BsonDocument> query]
        {

            get
            {
                Data data = Data.GetDatabase();

                return data.CurrentCollection.Find(query).ToBsonDocument();
            }
        }
    }
}
