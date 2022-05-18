using Interview.Api;
using Interview.Domain;
using Interview.Domain.Entities;
using Interview.Domain.Interfaces;
using Interview.Infrastructure;
using Interview.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
  {
    {
      new OpenApiSecurityScheme
      {
        Reference = new OpenApiReference
        {
          Type = ReferenceType.SecurityScheme,
          Id = "Bearer"
        },
        Scheme = "oauth2",
        Name = "Bearer",
        In = ParameterLocation.Header,

      },
      new List<string>()
    }});
});

builder.Services.AddDomain();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddSingleton<ITokenService>(new TokenService());

builder.Services.AddAuthorization();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

await app.Services.EnsureCreated();

app.MapPost("/api/v1/companies", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async (
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
    .Produces(StatusCodes.Status401Unauthorized)
    .WithName("CreateCompany")
    .WithTags("Companies")
    .RequireAuthorization();

app.MapGet("/api/v1/companies/{id:int}", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async (
    ICompanyRepository repository, 
    int id, 
    CancellationToken cancellation) =>
    {
        var result = await repository.FindByIdAsync(id, cancellation);

        return result == null ? Results.NotFound() : Results.Ok(result);
    })
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound)
    .Produces(StatusCodes.Status401Unauthorized)
    .WithName("GetCompanyById")
    .WithTags("Companies")
    .RequireAuthorization();

app.MapGet("/api/v1/companies/isin/{isin}", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async (
    ICompanyRepository repository, 
    string isin, 
    CancellationToken cancellation) =>
    {
        var result = await repository.FindByIsinAsync(isin, cancellation);

        return result == null ? Results.NotFound() : Results.Ok(result);
    })
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound)
    .Produces(StatusCodes.Status401Unauthorized)
    .WithName("GetCompanyByIsin")
    .WithTags("Companies")
    .RequireAuthorization();

app.MapGet("/api/v1/companies", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] (
    ICompanyRepository repository,
    CancellationToken cancellation) =>
    {
        var result = repository.GetAllCompanies(cancellation);
        return Results.Ok(result);
    })
    .Produces(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status401Unauthorized)
    .WithName("GetCompanies")
    .WithTags("Companies")
    .RequireAuthorization();

app.MapPut("/api/v1/companies/{id}", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async (
    ICompanyRepository repository, 
    int id, 
    CompanyModel model, 
    CancellationToken cancellation) =>
    {
        var isin = new CompanyIsin(model.Isin);
        var company = new Company(model.Name, model.Exchange, model.Ticker, isin, model.WebSite);
        var result = await repository.SaveChangesAsync(id, company, cancellation);

        return result == null ? Results.BadRequest("Problem to save this record!") : Results.Ok(company);

    })
    .Produces(StatusCodes.Status204NoContent)
    .Produces(StatusCodes.Status400BadRequest)
    .Produces(StatusCodes.Status401Unauthorized)
    .WithName("ChangeCompany")
    .WithTags("Companies")
    .RequireAuthorization();


app.MapGet("/login", [AllowAnonymous] async (
    HttpContext http,
    ITokenService tokenService,
    IUserRepository userRepository,
    CancellationToken cancellation) =>
    {
        var user = await http.Request.ReadFromJsonAsync<User>();
        var result = await userRepository.ValidateLoginAsync(user, cancellation);
        if (result == null)
        {
            return Results.Unauthorized();
        }

        var token = tokenService.BuildToken(builder.Configuration["Jwt:Key"], builder.Configuration["Jwt:Issuer"], result);
        await http.Response.WriteAsJsonAsync(new { token = token });
        return Results.Ok();
    })
    .Produces(StatusCodes.Status401Unauthorized)
    .Produces(StatusCodes.Status200OK)
    .WithName("Login")
    .WithTags("Auth");

app.Run();