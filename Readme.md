
#   JoysCakeShop 
### A webapp built on ASP .NET Core MVC using Entity Framework Core.

There are a couple of interesting pieces to ponder upon here:
  - the application uses factory design pattern and repository design pattern. 
  - the application can be run on a container

### Execution Instructions
```
docker build -t joyscakeshop -f Dockerfile .
docker run -it joyscakeshop
```
