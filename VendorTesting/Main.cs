using System.Diagnostics;
using VendorTesting.Service;

namespace VendorTesting
{
    public class Main
    {
        private readonly OutterService _collectVednorResponses;
        private readonly InnerService _validateVendorReturnedCases;
        private readonly ValidateRefferalLink _validateRefferalLink;

        public Main(OutterService collectVednorResponses, InnerService validateVendorReturnedCases, ValidateRefferalLink validateRefferalLink) 
        {
            _collectVednorResponses = collectVednorResponses;
            _validateVendorReturnedCases = validateVendorReturnedCases;
            _validateRefferalLink = validateRefferalLink;
        }

        public async Task Run() 
        {
            try
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
               
                Console.WriteLine("Started!!!");

                await TokenManager.Instance.SetToken();
                TestJsonHttpClientManager.Instance.SetTestJsonHttpClient(TokenManager.Instance.Token!);

                var test = await _collectVednorResponses.Execute();
                await _validateVendorReturnedCases.Execute(test);
                await _validateRefferalLink.Execute(test);
                
                var excelTestModel = Utility.ConvertTestToExcelModel(test);

                stopwatch.Stop();

                TimeSpan ts = stopwatch.Elapsed;

                XLSXFactoryClosedXML.CreateDocument(excelTestModel, ts);

                Console.WriteLine("Finished!!!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString() + ex.StackTrace);
            }
        }
    }
}
