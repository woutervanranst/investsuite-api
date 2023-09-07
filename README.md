https://blue-software.stoplight.io/docs/user-api/

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