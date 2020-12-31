USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Incident_Update]    Script Date: 06/07/2017 09:20:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will update an existing row in the table 'Incident'
-- Gets: @iIncidentInstance int
-- Gets: @iProblemCodeInstance int
-- Gets: @iCustomerOrderHeaderInstance int
-- Gets: @iTransID int
-- Gets: @iCommunicationChannelInstance int
-- Gets: @iCommunicationSourceInstance int
-- Gets: @iStatusInstance int
-- Gets: @iReferToIncidentInstance int
-- Gets: @sComments varchar(255)
-- Gets: @sUserIDCreated varchar(4)
-- Gets: @daDateCreated datetime
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Incident_Update]
	@iIncidentInstance int,
	@iProblemCodeInstance int,
	@iCustomerOrderHeaderInstance int,
	@iTransID int,
	@iCommunicationChannelInstance int,
	@iCommunicationSourceInstance int,
	@iStatusInstance int,
	@iReferToIncidentInstance int,
	@sComments varchar(500),
	@sUserIDCreated varchar(4)
	
AS
SET NOCOUNT ON
-- UPDATE an existing row in the table.
UPDATE [dbo].[Incident]
SET 
	[ProblemCodeInstance] = @iProblemCodeInstance,
	[CustomerOrderHeaderInstance] = @iCustomerOrderHeaderInstance,
	[TransID] = @iTransID,
	[CommunicationChannelInstance] = @iCommunicationChannelInstance,
	[CommunicationSourceInstance] = @iCommunicationSourceInstance,
	[StatusInstance] = @iStatusInstance,
	[ReferToIncidentInstance] = @iReferToIncidentInstance,
	[Comments] = @sComments,
	[UserIDCreated] = @sUserIDCreated
	
WHERE
	[IncidentInstance] = @iIncidentInstance
GO
