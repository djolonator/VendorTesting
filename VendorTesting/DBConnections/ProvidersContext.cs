using MongoDB.Driver;
using VendorTesting.Models;

namespace VendorTesting.DBConnections
{
    public class ProvidersContext
    {
        private readonly IMongoCollection<ExternalProviderStorage> _collection;

        public ProvidersContext()
        {
            const string _connectionString = "";
            const string providersDatabaseName = "Providers";
            const string providersCollectionNmae = "ExternalProviders";

            _collection = new MongoClient(_connectionString).
                GetDatabase(providersDatabaseName).
                GetCollection<ExternalProviderStorage>(providersCollectionNmae);

        }

        public async Task<List<ExternalProviderStorage>> GetProviders()
        {
            var providers = new List<ExternalProviderStorage>();

            try
            {
                providers = await _collection.Find(e => true).ToListAsync();
            }
            catch (Exception ex)
            {

            }
            return providers;
        }
    }
}
