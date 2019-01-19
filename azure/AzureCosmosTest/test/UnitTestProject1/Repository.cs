using Microsoft.Azure.Cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    public sealed class Repository : IDisposable
    {
        const string DatabaseName = "testdb";
        const string ContainerName = "testcol";
        const string PartitionKeyPath = "/partitionKey";

        private bool isDisposed;
        private readonly CosmosClient client;

        public Repository(string endpoint, string key)
        {
            client = new CosmosClient(endpoint, key);
        }

        public async Task Add(Item item)
        {
            CosmosDatabase database = await client.Databases.CreateDatabaseIfNotExistsAsync(DatabaseName);
            CosmosContainer container = await database.Containers.CreateContainerIfNotExistsAsync(ContainerName, PartitionKeyPath);
            await container.Items.CreateItemAsync(item.PartitionKey, item);
        }

        public async Task<Item> Get(string partitionKey, string id)
        {
            CosmosDatabase database = await client.Databases.CreateDatabaseIfNotExistsAsync(DatabaseName);
            CosmosContainer container = await database.Containers.CreateContainerIfNotExistsAsync(ContainerName, PartitionKeyPath);

            var query = new CosmosSqlQueryDefinition("SELECT * FROM x WHERE x.id = @id")
                .UseParameter("@id", id);

            List<Item> results = new List<Item>();
            var resultSetIterator = container.Items.CreateItemQuery<Item>(query, partitionKey: partitionKey);
            while (resultSetIterator.HasMoreResults)
            {
                results.AddRange((await resultSetIterator.FetchNextSetAsync()));
            }

            return results.Any() ? results.ElementAt(0) : null;
        }

        public async Task CleanAll()
        {
            CosmosDatabase database = await client.Databases.CreateDatabaseIfNotExistsAsync(DatabaseName);
            CosmosContainer container = await database.Containers.CreateContainerIfNotExistsAsync(ContainerName, PartitionKeyPath);
            await container.DeleteAsync();
            await database.DeleteAsync();
        }

        public void Dispose()
        {
            if (!isDisposed)
            {
                client.Dispose();
                isDisposed = true;
            }
        }
    }
}
