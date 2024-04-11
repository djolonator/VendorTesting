using MongoDB.Driver;
using VendorTesting.Models;

namespace VendorTesting.DBConnections
{
    public class CasesContext
    {
        private readonly IMongoCollection<PatientCasesV3> _collection;

        public CasesContext()
        {
            const string _connectionString = "";
            const string providersDatabaseName = "ExternalData";
            const string providersCollectionNmae = "ExternalPatientCasesV3";

            _collection = new MongoClient(_connectionString).
                GetDatabase(providersDatabaseName).
                GetCollection<PatientCasesV3>(providersCollectionNmae);

        }

        public async Task<List<PatientCasesV3>> GetNumberOfCases(string institutionCode, int numberOfCases)
        {
            var cases = new List<PatientCasesV3>();

            var builder = Builders<PatientCasesV3>.Filter;
            var filter = builder.Empty;

            var institutionCodeFilter = builder.Eq(x => x.InstitutionCode, institutionCode);
            filter &= institutionCodeFilter;

            try
            {
                cases = await _collection.Find(filter).Limit(numberOfCases).ToListAsync();

            }
            catch (Exception ex)
            {

            }
            return cases;
        }
    }
}
