SELECT [CustomerID]
      ,[AddressID]
      ,[AddressType]
  FROM [SSIS].[dbo].[CustomerAddress]
  Where Row_INSRT_DT = CAST(GETDATE() AS DATE)


CREATE PROCEDURE loadCustomerAddress
  AS
  BEGIN
 SELECT [CustomerID]
      ,[AddressID]
      ,[AddressType]
  FROM [SSIS].[dbo].[CustomerAddress]
  Where Row_INSRT_DT = CAST(GETDATE() AS DATE)
  END

EXEC loadCustomerAddress