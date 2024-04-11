using static VendorTesting.Models.Models;

namespace VendorTesting.Service
{
    public class InnerService
    {
        private readonly TestJsonService _testJsonService;
        public InnerService(TestJsonService testJsonService)
        {
            _testJsonService = testJsonService;
        }

        public async Task<Test> Execute(Test test)
        {

            Validations.Validations.ValidateVendorResponseIs200(ref test);
            await ReadContentFromVendorResponse(test);
            Validations.Validations.ValidateVendorResponseHasContent(ref test);
            CallTestJson(test);
            Validations.Validations.ValidateTestJsonResponseIs200(ref test);
            //Validations.Validations.ValidateVendorCase(ref test);


            return test;
        }

        public async Task<Test> ReadContentFromVendorResponse(Test test)
        {
            string stringObj = string.Empty;

            test.TestPassed.ForEach(async casee =>
            {
                stringObj = await casee.VendorResponse!.Content.ReadAsStringAsync();
                if (stringObj == string.Empty)
                {

                }
                casee.VendorResponseContent = stringObj;
            });

            return test;
        }

        public  Test CallTestJson(Test test)
        {
            test.TestPassed.ForEach( casee => 
            {
                var response = _testJsonService.CallTestJson(casee.VendorResponseContent).Result;
                casee.TestJsonResponse = response;
            } );

            return test;
        }
    }
}
