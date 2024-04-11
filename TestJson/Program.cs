using static VendorTesting.Models.VendorCaseModel;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();


app.MapPost("/TestJson", (PatientCaseRecordsWrapperWithoutPatient model) =>
{
    return Results.Ok(model);
});

app.Run();