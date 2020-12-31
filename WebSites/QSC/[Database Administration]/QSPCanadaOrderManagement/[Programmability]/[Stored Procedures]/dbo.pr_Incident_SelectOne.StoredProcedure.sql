USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Incident_SelectOne]    Script Date: 06/07/2017 09:20:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select an existing row from the table 'Incident'
-- based on the Primary Key.
-- Gets: @iIncidentInstance int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Incident_SelectOne]
	@iIncidentInstance int
AS
SET NOCOUNT ON
-- SELECT an existing row from the table.
SELECT
	[IncidentInstance],
	[ProblemCodeInstance],
	[CustomerOrderHeaderInstance],
	[TransID],
	[CommunicationChannelInstance],
	[CommunicationSourceInstance],
	[StatusInstance],
	[ReferToIncidentInstance],
	[Comments],
	[UserIDCreated],
	[DateCreated]
FROM [dbo].[Incident]
WHERE
	[IncidentInstance] = @iIncidentInstance
GO
