

```wsl

docker run --rm \
  -v ${PWD}:/local openapitools/openapi-generator-cli generate   -i /local/user-api.yaml   -g aspnetcore   -o /local/out/csharp

  ```