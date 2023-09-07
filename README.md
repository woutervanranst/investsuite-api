https://blue-software.stoplight.io/docs/user-api/


```wsl

docker run --rm \
  -v ${PWD}:/local openapitools/openapi-generator-cli generate  \
  -i /local/user-api.yaml \
  -g aspnetcore \
  -o /local/src/user-api-base
  --additional-properties packageName=YourPackageName

  ```