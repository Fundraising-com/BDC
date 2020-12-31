USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_IncidentAction_Insert]    Script Date: 06/07/2017 09:20:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
---------------------------------------------------------------------------------
-- Stored procedure that will insert 1 row in the table 'IncidentAction'
-- Gets: @iIncidentInstance int
-- Gets: @iActionInstance int
-- Gets: @sComments varchar(255)
-- Gets: @sUserIDCreated varchar(4)
-- Gets: @daDateCreated datetime
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_IncidentAction_Insert]
	@iInstance int output,
	@iIncidentInstance int,
	@iActionInstance int,
	@sComments varchar(500),
	@sUserIDCreated varchar(4)
	
AS

DECLARE 	@iCustomerOrderHeaderInstance int,
		@iTransID int,
		@iCustomerRemitHistoryInstance int

select @iCustomerOrderHeaderInstance=customerorderheaderinstance, @iTransID=transid from incident where incidentinstance=@iIncidentInstance

select @iCustomerRemitHistoryInstance=customerremithistoryinstance from vw_getsubinfo where customerorderheaderinstance=@iCustomerOrderHeaderInstance and transid=@iTransID
-- INSERT a new row in the table.
INSERT [dbo].[IncidentAction]
(
	[IncidentInstance],
	[ActionInstance],
	[Comments],
	[UserIDCreated],
	[DateCreated],
	customerremithistoryinstance
)
VALUES
(
	@iIncidentInstance,
	@iActionInstance,
	@sComments,
	@sUserIDCreated,
	getdate(),
	@iCustomerRemitHistoryInstance
)

set @iInstance = scope_identity()
GO
