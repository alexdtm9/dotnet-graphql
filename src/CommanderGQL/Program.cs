using CommanderGQL.Data;
using CommanderGQL.GraphQL;
using CommanderGQL.GraphQL.Commands;
using CommanderGQL.GraphQL.Platforms;
using GraphQL.Server.Ui.Voyager;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPooledDbContextFactory<AppDbContext>(opt => opt.UseSqlServer
    (builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddGraphQLServer()
    .RegisterDbContext<AppDbContext>(DbContextKind.Pooled)
    .AddQueryType<Query>()
    .AddType<PlatformType>()
    .AddType<CommandType>()
    .AddProjections();

var app = builder.Build();
app.MapGraphQL();
app.UseGraphQLVoyager(options: new VoyagerOptions()
{
    GraphQLEndPoint = "/graphql",
    
});

app.Run();