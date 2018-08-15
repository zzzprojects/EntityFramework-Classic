# BatchSaveChanges

## Description
You can reduce the number of database roundtrip by batching multiple command in the same command. The BatchSaveChanges and BatchSaveChangesAsync methods work exactly like SaveChanges but way faster.
If the provider doesn’t support multiple statement, the logic will automatically fall back to SaveChanges.

### Provider Supported
- SQL Server

## Examples
```csharp
```
[Try it](https://dotnetfiddle.net/tuONVZ)

## Options

### IsEnabled
When disabled, the BatchSaveChanges will use SaveChanges instead.

### UseBatchForSaveChanges
When enabled, the SaveChanges will use BatchSaveChanges if the provider support multiple statements.

## Limitations
- Stored Procedure will continue to use SaveChanges
