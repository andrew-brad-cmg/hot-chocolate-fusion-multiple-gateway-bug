# hot-chocolate-fusion-multiple-gateway-bug

This repo is linked to a Github issue filed here: <>

It is an updated form of the sample repo for Fusion located here: https://github.com/ChilliCream/hotchocolate-examples/tree/master/misc/Fusion1

What code changes have been made to best capture the repro:

- Update to HC v13.9.10
- Fixup Program.cs to accomodate latest syntax changes
- Remove some schema extensions files to eliminate other variables (though our internal repro still utilizes them)

Repro steps:

- Clone and build this repo. Open a terminal from the root: `dotnet build`
- Run a `dotnet tool restore`
- Pack Accounts subgraph and compose it into `gateway1.fgp`:

```
dotnet fusion subgraph pack -w ./Subgraphs/Accounts/src;
dotnet fusion compose --subgraph-package-file ./Subgraphs/Accounts/src/Account.fsp --package-file ./Gateway/Gateway1/gateway1;
```

- Observe/validate the produced `gateway1.fgp` file.
- Pack Reviews subgraph and compose it into `gateway2.fgp`:

```
dotnet fusion subgraph pack -w ./Subgraphs/Reviews/;
dotnet fusion compose --subgraph-package-file ./Subgraphs/Reviews/Review.fsp --package-file ./Gateway/Gateway2/gateway2;
```

- Observe/validate the produced `gateway2.fgp` file.
- Run the gateway (running the subgraph projects is not technically necessary)
- Visit `http://localhost:5000/graphql/` and ensure Banana Cake Pop UI loads. Ensure schema endpoint: `http://localhost:5000/graphql/`
- Loading the schema in BCP, observe that it is incorrectly loading the `Reviews` schema. If you load the other schema endpoint `http://localhost:5000/api/`, you will see the correct schema for that, which is `Reviews`.
- We have determined that order of operations influences this, which is to say that you can invoke `AddFusionGatewayServer()` with gateway 2 first, followed by gateway1. In that scenario, the `Accounts` subgraph will "win" (i.e. show for both schema endpoints).
- `fgp` files can be rebuilt and inspected as desired for reproducton, to the best of our knowledge they are correctly packaging all artifacts.

```
rm -rf ./Gateway/bin;rm -rf ./Gateway/Gateway1/*;rm -rf ./Gateway/Gateway2/*; dotnet build;
(then repeat previous commands)
```
