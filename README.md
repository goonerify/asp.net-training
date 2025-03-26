# asp.net-training

## Run entity framework migrations
### Create the migration files
In the NuGet Package Manager Console, run the following commands:
```Add-Migration "Description of the migration" -Context "Name of the DBContext to run"```

### Create the database schemas, tables, indexes etc., and seed records into the database
```Update-Database -Context "Name of the DBContext to run"```
