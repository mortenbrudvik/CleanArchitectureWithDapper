
# Clean Architecture Template with Dapper


DI: AutoFac
Persistence: Dapper, Dapper.Contrib, SqlKata and SQLLite, FluentMigrator

### Pending
- [ ] SeriLog


### Issues

TaskManager.UI
- [x] Fail to build React project
- [ ] Setting PORT in .env file does not work


## React Client


Visual Studio React Project
[Tutorial: Create a Node.js and React app in Visual Studio](https://learn.microsoft.com/en-us/visualstudio/javascript/tutorial-nodejs-with-react-and-jsx?view=vs-2022)


---



## Tech dependencies

### Abstractions

```shell
dotnet add package Microsoft.Extensions.DependencyInjection.Abstractions
dotnet add package Microsoft.Extensions.Configuration.Abstractions
```

### MediatR
https://github.com/jbogard/MediatR
```shell
dotnet add package MediatR
```

### SqlKata
https://sqlkata.com/

```shell
dotnet add package SqlKata
dotnet add package SqlKata.Execution
```

### Dapper
https://github.com/DapperLib/Dapper
https://github.com/DapperLib/Dapper.Contrib

```shell
dotnet add package dapper
dotnet add package dapper.contrib
```

### SQLite
https://learn.microsoft.com/en-us/dotnet/standard/data/sqlite/?tabs=netcore-cli

```shell
dotnet add package Microsoft.Data.Sqlite
```

### Fluent Migrator

https://github.com/fluentmigrator/fluentmigrator

https://fluentmigrator.github.io/index.html

```shell
# For migration development
dotnet add package FluentMigrator

# For migration execution
dotnet add package FluentMigrator.Runner

# For database support
dotnet add package FluentMigrator.Runner.SQLite
```

### AutoFac

https://autofac.readthedocs.io/en/latest/index.html

https://autofac.readthedocs.io/en/latest/integration/aspnetcore.html


```shell
dotnet add package Autofac
```
