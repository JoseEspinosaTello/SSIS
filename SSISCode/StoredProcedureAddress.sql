SELECT [AddressID]
      ,[AddressLine1]
      ,[AddressLine2]
      ,[City]
      ,[StateProvince]
      ,[CountryRegion]
      ,[PostalCode]
  FROM [SSIS].[dbo].[Address]
  Where Row_INSRT_DT = CAST(GETDATE() AS DATE)

  CREATE PROCEDURE loadAddress
  AS
  BEGIN
  SELECT [AddressID]
      ,[AddressLine1]
      ,[AddressLine2]
      ,[City]
      ,[StateProvince]
      ,[CountryRegion]
      ,[PostalCode]
  FROM [SSIS].[dbo].[Address]
  Where Row_INSRT_DT = CAST(GETDATE() AS DATE)
  END

  EXEC loadAddress