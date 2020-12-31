USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_AddressList_ReverseLookup]    Script Date: 06/07/2017 09:33:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_AddressList_ReverseLookup]
  @AddressListID int
AS

	select 
	 1 as [OrderNumber],
	 'AddressList' AS [TableName], 
	 cast([ID] as varchar) AS [Id], 
	 'NA' AS Type, 
	 ' CreateDate: ' + cast(CreateDate as varchar) AS [Field1]
	from QSPCanadaCommon.dbo.AddressList WHERE [ID] = @AddressListID
UNION
	select 
	 2 as [OrderNumber],
	 'Address' AS [TableName], 
	 cast([address_id] as varchar) AS [Id], 
	 cast([address_type] as varchar) AS Type, 
	 'Street1: ' + [Street1] AS [Field1]
	from QSPCanadaCommon.dbo.Address WHERE [AddressListID] = @AddressListID
UNION
	select 
	 3 as [OrderNumber],
	 'CAccount' AS [TableName], 
	 cast([Id] as varchar) AS [Id],  
	 'NA' AS Type, 
	 '       Name: ' + [Name] AS [Field1]
	from QSPCanadaCommon.dbo.CAccount WHERE [AddressListID] = @AddressListID
UNION
	select 
	 5 as [OrderNumber],
	 'FieldManager' AS [TableName], 
	 [FMID] AS [Id], 
	 'NA' AS Type, 
	 '       Name: ' + [FirstName] + ' ' + [LastName] AS [Field1]
	from QSPCanadaCommon.dbo.FieldManager WHERE [AddressListID] = @AddressListID
ORDER BY [OrderNumber] ASC;
GO
