using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System.Configuration;
using System.Linq.Expressions;
using System.Net;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DDAC2
{
    public class CosmoHelper<T> where T : class
    {
        private static readonly string DatabaseId = "Reservation";
        private static readonly string CollectionId = "Items";
        private static DocumentClient client;
        public static void Initialize()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");
            IConfigurationRoot Configuration = builder.Build();
            client = new DocumentClient(new
           Uri(Configuration["Setting1:url"]),
           Configuration["Setting1:key"]);
            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync().Wait();
        }
        private static async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await
               client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId))
               ;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode ==
               System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDatabaseAsync(new Database { Id = DatabaseId });
                }
                else
                {
                    throw;
                }
            }
        }
        private static async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {

                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId));
            }

            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentCollectionAsync(UriFactory.CreateDatabaseUri(DatabaseId),
                    new DocumentCollection
                    {
                        Id = CollectionId
                    },
                    new RequestOptions
                    {
                        OfferThroughput = 1000
                    });
                }
                else
                {
                    throw;
                }
            }
        }
        public static async Task<IEnumerable<T>> GetItemAsync(Expression<Func<T, bool>> predicate)
        {
            IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId))
                .Where(predicate)
                .AsDocumentQuery();

            List<T> result = new List<T>();
            while (query.HasMoreResults)
            {
                result.AddRange(await query.ExecuteNextAsync<T>());
            }
            return result;
        }
        public static async Task<Document> CreateItemAsync(T item)
        {
            return await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), item);
        }
        public static async Task<Document> UpdateItemAsync(string id, T item)
        {
            return await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id), item);
        }
        public static async Task<T> GetItemAsync(string id)
        {
            try
            {
                Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId,id));
                return (T)(dynamic)document;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }
    }
}

