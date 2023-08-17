SELECT [CustomerID]
      ,[NameStyle]
      ,[Title]
      ,[FirstName]
      ,[MiddleName]
      ,[LastName]
      ,[Suffix]
      ,[CompanyName]
      ,[SalesPerson]
      ,[EmailAddress]
      ,[Phone]
  FROM [SSIS].[dbo].[Customer]
  Where Row_INSRT_DT = CAST(GETDATE() AS DATE)


  Create Procedure loadCustomers
  as
  Begin
  SELECT [CustomerID]
      ,[NameStyle]
      ,[Title]
      ,[FirstName]
      ,[MiddleName]
      ,[LastName]
      ,[Suffix]
      ,[CompanyName]
      ,[SalesPerson]
      ,[EmailAddress]
      ,[Phone]
  FROM [SSIS].[dbo].[Customer]
  Where Row_INSRT_DT = CAST(GETDATE() AS DATE)
end

exec loadCustomers