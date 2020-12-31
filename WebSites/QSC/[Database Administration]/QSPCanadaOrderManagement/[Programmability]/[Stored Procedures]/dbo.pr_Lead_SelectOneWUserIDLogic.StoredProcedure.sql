USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Lead_SelectOneWUserIDLogic]    Script Date: 06/07/2017 09:20:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select one or more existing rows from the table 'Lead'
-- based on a foreign key field.
-- Gets: @iUserID int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Lead_SelectOneWUserIDLogic]
	@iUserID int

AS
SET NOCOUNT ON
-- SELECT one or more existing rows from the table.
SELECT
	[UserID],
	[Date],
	[DateSent],
	[ContactName],
	[ContactHomePhoneNumber],
	[ContactWorkPhoneNumber],
	[ContactFaxNumber],
	[ContactEMail],
	[SchoolGroup],
	[CityTown],
	[Province],
	[InterestedInWhat],
	[WhereHearAboutQSP],
	[FMID],
	[Comments],
	Instance
FROM [dbo].[Lead]
WHERE
	[Instance] = @iUserID
GO
