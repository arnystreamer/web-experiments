# NGINX

## Description

Emulation of fronting Nginx which acts like reverse proxy for other applications

## Deployment

```
docker build -t nginx-proxy .
docker run -d -p 4080:80 --name nginx-container nginx-proxy
```
