
# Clean Architecture Template with Dapper


- Dapper, Dapper.Contrib, SqlKata and SQLLite

### Pending
- AutoFac, Fluent migrator, SeriLog

---



## Dependencies

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

