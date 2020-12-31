USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_Contact]    Script Date: 06/07/2017 09:33:22 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_get_Contact]
	@ContactID int
AS

SELECT 
	Contact.[Id] AS [ContactID],
	Contact.[ContactListID],
	Contact.[CAccountID],
	Contact.[Title],
	Contact.[FirstName],
	Contact.[LastName],
	Contact.[MiddleInitial],
	Contact.[TypeId],
	Contact.[Function],
	Contact.[Email],
	Contact.[AddressID],
	Contact.[PhoneListID]
FROM 
	[QSPCanadaCommon].[dbo].[Contact]

WHERE 
	Contact.[Id] = @ContactID
	AND Contact.[DeletedTF] <> 1
GO
