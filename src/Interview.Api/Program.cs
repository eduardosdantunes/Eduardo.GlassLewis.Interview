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

app.MapPost("/api/v1/companies", async (ICompanyRepository repository, CompanyModel model, CancellationToken cancellation) =>
{
    var isin = new CompanyIsin(model.Isin);
    var company = new Company(model.Name, model.Exchange, model.Ticker, isin);
    var result = await repository.CreateCompanyAsync(company, cancellation);

    return Results.CreatedAtRoute("GetCompanyById", new { result.Id });
}).WithName("CreateCompany");

app.MapGet("/api/v1/companies/{id:int}", async (ICompanyRepository repository, int id, CancellationToken cancellation) =>
{
    var result = await repository.FindByIdAsync(id, cancellation);

    return result == null ? Results.NotFound() : Results.Ok(result);
}).WithName("GetCompanyById");

app.MapGet("/api/v1/companies/isin/{isin}", async (ICompanyRepository repository, string isin, CancellationToken cancellation) =>
{
    var result = await repository.FindByIsinAsync(isin, cancellation);

    return result == null ? Results.NotFound() : Results.Ok(result);
}).WithName("GetCompanyByIsin");

app.MapGet("/api/v1/companies", (ICompanyRepository repository, CancellationToken cancellation) =>
{
    var result = repository.GetAllCompanies(cancellation);
    return Results.Ok(result);

}).WithName("GetCompanies");

app.Run();