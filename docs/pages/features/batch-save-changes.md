# BatchSaveChanges

## Description
You can reduce the number of database roundtrip by batching multiple command in the same command. The BatchSaveChanges and BatchSaveChangesAsync methods work exactly like SaveChanges but way faster.
If the provider doesn't support multiple statement, the logic will automatically fall back to SaveChanges.

> For provider supporting `BatchSaveChanges`, we recommand to always use `BatchSaveChanges` over `SaveChanges` or to enable the option `UseBatchForSaveChanges`

### Provider Supported
- SQL Server

## Examples
```csharp
// context.SaveChanges();	
context.BatchSaveChanges();	
```
[Try it](https://dotnetfiddle.net/dJK5Vr)

## Options

### IsEnabled
When disabled, the BatchSaveChanges will use SaveChanges instead.
```csharp
public EntityContext() : base(@"Data Source=ZZZ_Projects.sdf")
{
	// Disable BatchSaveChanges
	this.Configuration.BatchSaveChanges.IsEnabled = false;
}

// ...code...

// The BatchSaveChanges will automatically use SaveChanges because the features have been disabled in the constructor.
context.BatchSaveChanges();	
```
[Try it](https://dotnetfiddle.net/jo6QN1)

### UseBatchForSaveChanges
When enabled, the SaveChanges will use BatchSaveChanges if the provider support multiple statements.
```csharp
public EntityContext() : base(@"Data Source=ZZZ_Projects.sdf")
{
	// Force to BatchSaveChanges instead of SaveChanges
	this.Configuration.BatchSaveChanges.UseBatchForSaveChanges = true;
}

// ...code...

// The SaveChanges will automatically use BeachSaveChanges because the features have been forced in the constructor.
context.SaveChanges();	
```
[Try it](https://dotnetfiddle.net/ceeM0J)

## Limitations
- Stored Procedure will continue to use SaveChanges
