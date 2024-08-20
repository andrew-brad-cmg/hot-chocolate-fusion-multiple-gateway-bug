var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

builder.Services
    // for v13.2.1:
    //.AddFusionGatewayServer(fusionGraphFile: "./Gateway1/gateway1.fgp", watchFileForUpdates: true, graphName: "gateway1")
    // for v13.9.10:
    .AddFusionGatewayServer(graphName: "gateway1")
    .ConfigureFromFile("./Gateway1/gateway1.fgp", watchFileForUpdates: true)
    
    // for v13.2.1:
    //.AddFusionGatewayServer(fusionGraphFile: "./Gateway2/gateway2.fgp", watchFileForUpdates: true, graphName: "gateway2");
    .CoreBuilder.Services
    // for v13.9.10:
    .AddFusionGatewayServer("gateway2")
    .ConfigureFromFile("./Gateway2/gateway2.fgp", watchFileForUpdates: true);


var app = builder.Build();
//app.MapGraphQL();
app.MapGraphQL(schemaName: "gateway1");
app.MapGraphQL("/api" , "gateway2");

app.RunWithGraphQLCommands(args);


