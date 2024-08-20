var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:59092");

builder.Services
    .AddDbContextPool<ReviewContext>(
        o => o.UseSqlite("Data Source=review.db"));

builder.Services
    .AddGraphQLServer()
    .AddTypes()
    .AddQueryType()
    .RegisterDbContext<ReviewContext>();

var app = builder.Build();

await DatabaseHelper.SeedDatabaseAsync(app);

app.UseWebSockets();

app.MapGraphQL();

app.RunWithGraphQLCommands(args);
