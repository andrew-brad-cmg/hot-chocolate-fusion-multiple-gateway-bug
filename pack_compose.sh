# dotnet run --project Subgraphs/Reviews -- schema export --output schema.graphql

# if necessary, rimraf the bin folder and gateway artifacts to ensure a clean setup:
rm -rf ./Gateway/bin;rm -rf ./Gateway/Gateway1/*;rm -rf ./Gateway/Gateway2/*; dotnet build;

# For Gateway1, we pack and compose Accounts:
dotnet fusion subgraph pack -w ./Subgraphs/Accounts/src;
dotnet fusion compose --subgraph-package-file ./Subgraphs/Accounts/src/Account.fsp --package-file ./Gateway/Gateway1/gateway1;

# For Gateway2, we pack and compose Reviews:
dotnet fusion subgraph pack -w ./Subgraphs/Reviews/;
dotnet fusion compose --subgraph-package-file ./Subgraphs/Reviews/Review.fsp --package-file ./Gateway/Gateway2/gateway2;












dotnet fusion compose --subgraph-package-file ./Subgraphs/Accounts/src/Account.fsp --package-file ./Gateway/Gateway1/gateway1;
dotnet fusion compose --subgraph-package-file ./Subgraphs/Reviews/Review.fsp --package-file ./Gateway/Gateway1/gateway1;

dotnet fusion compose --subgraph-package-file ./Subgraphs/Reviews/Review.fsp --package-file ./Gateway/Gateway2/gateway2;
