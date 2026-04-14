# blazor-legacy (BlazorAppTest)

## Description

Old and legacy code of Blazor application, taken from real enterprise project

## Deployment

```
docker build -t blazor-test-app:latest . && docker run -d -p 4088:8088 --name blazor-test-app-container blazor-test-app:latest
```

## Cleanup

```
docker stop blazor-test-app-container && docker rm blazor-test-app-container && docker rmi blazor-test-app
```
