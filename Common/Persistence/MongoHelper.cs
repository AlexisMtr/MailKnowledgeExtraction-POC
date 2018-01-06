using MongoDB.Driver;
using System;

namespace Common.Persistence
{
    public class MongoHelper : IDisposable
    {
        private IMongoDatabase database;
        private readonly string collectionName;

        public MongoHelper(string connectionString = "mongodb://127.0.0.1:27017", string dbName = "ViseoGC", string collectionName = "mails")
        {
            this.database = new MongoClient(connectionString).GetDatabase(dbName);
            this.collectionName = collectionName;
        }

        public void Dispose()
        {
            if (this.database != null)
                this.database = null;
        }

        public void Save<T>(T item)
        {
            this.database.GetCollection<T>(collectionName).InsertOne(item);
        }
    }
}
