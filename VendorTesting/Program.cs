using Microsoft.Extensions.DependencyInjection;
using VendorTesting.DBConnections;
using VendorTesting.Service;

namespace VendorTesting

{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            
            serviceCollection.AddSingleton<ProvidersContext>();
            serviceCollection.AddSingleton<CasesContext>();
            serviceCollection.AddSingleton<InstitutionService>();
            serviceCollection.AddSingleton<OutterService>();
            serviceCollection.AddSingleton<TestJsonService>();
            serviceCollection.AddSingleton<InnerService>();
            serviceCollection.AddSingleton<ValidateRefferalLink>();
            serviceCollection.AddSingleton<Main>();

            
            var serviceProvider = serviceCollection.BuildServiceProvider();

            
            var startEngine = serviceProvider.GetService<Main>();

            try
            {
                Task.Run(() => startEngine!.Run()).Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }
    }
}