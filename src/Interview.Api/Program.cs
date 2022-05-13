using Interview.Api;
using Interview.Domain;
using Interview.Domain.Entities;
using Interview.Domain.Interfaces;
using Interview.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDomain();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
await app.Services.EnsureCreated();

app.MapPost("/api/v1/companies", async (
    ICompanyRepository repository, 
    CompanyModel model, 
    CancellationToken cancellation) =>
    {
        var isin = new CompanyIsin(model.Isin);
        var company = new Company(model.Name, model.Exchange, model.Ticker, isin, model.WebSite);
        var result = await repository.CreateCompanyAsync(company, cancellation);

        return result is not null
            ? Results.CreatedAtRoute("GetCompanyById", new { ID = result.Id })
            : Results.BadRequest("Error to save company!");
    })
    .Produces<Company>(StatusCodes.Status201Created)
    .Produces<Company>(StatusCodes.Status400BadRequest)
    .WithName("CreateCompany")
    .WithTags("Companies");

app.MapGet("/api/v1/companies/{id:int}", async (
    ICompanyRepository repository, 
    int id, 
    CancellationToken cancellation) =>
    {
        var result = await repository.FindByIdAsync(id, cancellation);

        return result == null ? Results.NotFound() : Results.Ok(result);
    })
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound)
    .WithName("GetCompanyById")
    .WithTags("Companies");

app.MapGet("/api/v1/companies/isin/{isin}", async (
    ICompanyRepository repository, 
    string isin, 
    CancellationToken cancellation) =>
    {
        var result = await repository.FindByIsinAsync(isin, cancellation);

        return result == null ? Results.NotFound() : Results.Ok(result);
    })
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound)
    .WithName("GetCompanyByIsin")
    .WithTags("Companies");

app.MapGet("/api/v1/companies", (
    ICompanyRepository repository, 
    CancellationToken cancellation) =>
    {
        var result = repository.GetAllCompanies(cancellation);
        return Results.Ok(result);
    })
    .Produces(StatusCodes.Status200OK)
    .WithName("GetCompanies")
    .WithTags("Companies");

app.MapPut("/api/v1/companies/{id}", async (
    ICompanyRepository repository, 
    int id, CompanyModel model, 
    CancellationToken cancellation) =>
    {
        var isin = new CompanyIsin(model.Isin);
        var company = new Company(model.Name, model.Exchange, model.Ticker, isin, model.WebSite);
        var result = await repository.SaveChangesAsync(id, company, cancellation);

        return result == null ? Results.BadRequest("Problem to save this record!") : Results.Ok(company);

    })
    .Produces(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status400BadRequest)
    .WithName("ChangeCompany")
    .WithTags("Companies");

app.Run();