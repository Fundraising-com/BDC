USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Incident_Insert]    Script Date: 06/07/2017 09:20:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will insert 1 row in the table 'Incident'
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
CREATE PROCEDURE [dbo].[pr_Incident_Insert]
	@iIncidentInstance int output,
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
-- INSERT a new row in the table.
INSERT [dbo].[Incident]
(
	
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
)
VALUES
(
	
	@iProblemCodeInstance,
	@iCustomerOrderHeaderInstance,
	@iTransID,
	@iCommunicationChannelInstance,
	@iCommunicationSourceInstance,
	@iStatusInstance,
	@iReferToIncidentInstance,
	@sComments,
	@sUserIDCreated,
	getdate()
)

SELECT @iIncidentInstance =SCOPE_IDENTITY()
GO
