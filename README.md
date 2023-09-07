# InvestSuite API

This repository demonstrates a reimplementation of the InvestSuite API in ASP.NET Core.

![](http://www.plantuml.com/plantuml/proxy?cache=no&src=https://raw.githubusercontent.com/woutervanranst/investsuite-api/master/architecture.puml)

Features:
- Design-first API approach

## API Design First Approach

### API Definition

https://blue-software.stoplight.io/docs/user-api/

### With NSwagStudio

https://github.com/RicoSuter/NSwag


https://srikargandhi.medium.com/api-design-first-approach-d02505306b3

### With OpenApiGeneratorCli (not used)

https://openapi-generator.tech/docs/generators/csharp/

https://github.com/OpenAPITools/openapi-generator


  ```wsl
docker run --rm \
  -v ${PWD}:/local openapitools/openapi-generator-cli generate  \
  -i /local/user-api.yaml \
  -g aspnetcore \
  -o /local/src/user-api-base \
  --additional-properties=aspnetCoreVersion=6.0,packageName=UserApi,netCoreProjectFile=true

  ```

## With SourceAPI (not used)

https://github.com/alekshura/SourceApi

## Secrets

https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=windows

dotnet user-secrets set "CosmosDb:Key" "12345"