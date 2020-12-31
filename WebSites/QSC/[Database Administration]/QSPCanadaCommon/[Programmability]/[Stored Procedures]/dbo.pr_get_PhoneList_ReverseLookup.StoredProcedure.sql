USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_PhoneList_ReverseLookup]    Script Date: 06/07/2017 09:33:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_PhoneList_ReverseLookup]
 @PhoneListID int
AS

	select 
	 1 as [OrderNumber],
	 'PhoneList' AS [TableName], 
	 cast([ID] as varchar) AS [Id], 
	 'NA' AS Type, 
	 ' CreateDate: ' + cast(CreateDate as varchar) AS [Field1]
	from QSPCanadaCommon.dbo.PhoneList WHERE [ID] = @PhoneListID
UNION
	select 
	 2 as [OrderNumber],
	 'Phone' AS [TableName], 
	 cast([Id] as varchar) AS [Id], 
	 cast([Type] as varchar) AS Type, 
	 'PhoneNumber: ' + [PhoneNumber] AS [Field1]
	from QSPCanadaCommon.dbo.Phone WHERE [PhoneListID] = @PhoneListID
UNION
	select 
	 3 as [OrderNumber],
	 'CAccount' AS [TableName], 
	 cast([Id] as varchar) AS [Id],  
	 'NA' AS Type, 
	 '       Name: ' + [Name] AS [Field1]
	from QSPCanadaCommon.dbo.CAccount WHERE [PhoneListID] = @PhoneListID
UNION
	select 
	 4 as [OrderNumber],
	 'Campaign' AS [TableName], 
	 cast([Id] as varchar) AS [Id], 
	 'NA' AS Type, 
	 '  StartDate: ' + cast([StartDate] as varchar) AS [Field1]
	from QSPCanadaCommon.dbo.Campaign WHERE [PhoneListID] = @PhoneListID
UNION
	select 
	 5 as [OrderNumber],
	 'FieldManager' AS [TableName], 
	 [FMID] AS [Id], 
	 'NA' AS Type, 
	 '       Name: ' + [FirstName] + ' ' + [LastName] AS [Field1]
	from QSPCanadaCommon.dbo.FieldManager WHERE [PhoneListID] = @PhoneListID
UNION
	select 
	 6 as [OrderNumber],
	 'Contact' AS [TableName], 
	 cast([Id] as varchar) AS [Id], 
	 'NA' AS Type, 
	 --'test' as [Field1]
	 '       Name: ' + [FirstName] + ' ' + [LastName] AS [Field1]
	from QSPCanadaCommon.dbo.Contact WHERE [PhoneListID] = @PhoneListID
ORDER BY [OrderNumber] ASC;
GO
