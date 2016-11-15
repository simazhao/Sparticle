using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sparticle.Support.NoSql.MongoDb
{
    public class MongoDbDriver17
    {
        private IDictionary<string, MongoCollection<BsonDocument>> _collections = new Dictionary<string, MongoCollection<BsonDocument>>();
        private readonly MongoDatabase _db;

        static MongoDbDriver17()
        {
            var conns = MongoDbConnections.DbConnections;
        }

        public MongoDbDriver17(string serverTag, string dbName, params string[] prefetchCollections)
        {
            if (MongoDbConnections.DbConnections == null || MongoDbConnections.DbConnections.Keys == null || !MongoDbConnections.DbConnections.Keys.Contains(serverTag))
                throw new ConfigurationErrorsException("MongoDB.Servers #"+ serverTag);

            var server = MongoDbConnections.DbConnections[serverTag].Server;
            var credentials = server.Settings.DefaultCredentials;
            _db = server.GetDatabase(dbName, credentials);

            foreach (var collectionName in prefetchCollections)
            {
                var collection = _db.GetCollection(collectionName);

                if (collection != null)
                {
                    _collections.Add(collectionName, collection);
                }
            }
        }

        public void Save<TData>(string collectionName, TData data)
        {
            var collection = GetCollection(collectionName);

            if (collection == null)
                throw new InvalidOperationException(string.Format("fail to get collection [{0}]", collectionName));

            collection.Save(data);
        }

        public bool Update(string collectionName, IDictionary<string, object> condition, IDictionary<string, object> forUpdate)
        {
            var collection = GetCollection(collectionName);

            if (collection == null)
                throw new InvalidOperationException(string.Format("fail to get collection [{0}]", collectionName));

            if (condition == null || forUpdate == null)
                return false;

            if (!condition.Any() || !forUpdate.Any())
                return false;

            var query = new QueryDocument();
            query.Add(condition, condition.Keys);

            var update = new UpdateDocument();

            var setDictionary = new Dictionary<string, object>();
            foreach (var updatePair in forUpdate)
            {
                var updateDictionary = updatePair.Value as IDictionary<string, object>;

                if (updateDictionary != null)
                {
                    var pushDoc = new BsonDocument(updateDictionary);

                    update.Add("$push", new BsonDocument().Add(updatePair.Key, pushDoc));
                }
                else
                {
                    setDictionary.Add(updatePair.Key, updatePair.Value);
                }
            }

            if (setDictionary.Any())
            {
                var setDoc = new BsonDocument(setDictionary);

                update.Add("$set", setDoc);
            }

            collection.Update(query, update);

            return true;
        }

        private MongoCollection<BsonDocument> GetCollection(string collectionName)
        {
            if (_collections.ContainsKey(collectionName))
            {
                return _collections[collectionName];
            }

            var collection = _db.GetCollection(collectionName);

            return collection;
        }
    }
}
