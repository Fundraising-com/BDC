USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Incident_SelectAll]    Script Date: 06/07/2017 09:20:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will select all rows from the table 'Incident'
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Incident_SelectAll]

AS
SET NOCOUNT ON
-- SELECT all rows from the table.
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
ORDER BY 
	[IncidentInstance] ASC
GO
