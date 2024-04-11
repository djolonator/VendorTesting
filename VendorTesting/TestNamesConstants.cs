
namespace VendorTesting
{
    public static class TestNamesConstants
    {

        // test names

       // poziv ka instituciji
       public const string VendorCaseIsFound = "Test case za instituciju je pronadjen kod nas";
       public const string VendorResponseExists = "Poziv ka vendoru je vratio odgovor";
       public const string VendorResponseIsValid = "Status kod odgovora je 200";
       public const string VendorResponseHasContent = "Uspesan odgovor ima sadrzaj";
       
       // vaidiranje predmeta
       public const string TestJsonResponseIsValid = "Test json poziv je vratio 200";
       public const string ReferrallinkIsFound = "U list venodrskih predmeta je pornadjeno polje ReferralResponseUrl";



       public const string PatientCasesExists = "Vendorski objekat ima polje patientCases koje nije prazan niz";
       public const string PatientCasesExistsRequestsExists = "Vendorski objekat ima polje patientCases i u njemu requests koje nije prazan niz";
       public const string PatientCasesRequestsResponsesExists = "Vendorski patientCase ima polje requests i u njemu polje responses koje je niz";
       public const string PatientCasesRequestsResponsesIsNotEmpty = "Responses nije prazan niz i prvi clan ima polje referralResponseUrl";

        // validiranje linka
       public const string ReferalLinkResponseExists = "Poziv ka url iz referralResponseUrl je vrato odgovor";
       public const string ReferalLinkResponseIsValid = "Odgovor iz poziva ka url iz referralResponseUrl je dostupan (vratio 200)";
       public const string ReferalLinkResponseContenttypeIsValid = "Content type iz response header-a iz referralResponseUrl linka je tipa pdf";
        
    }
}
