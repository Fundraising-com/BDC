USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Season_Update]    Script Date: 06/07/2017 09:33:30 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will update an existing row in the table 'Season'
-- Gets: @iID int
-- Gets: @sCountry varchar(10)
-- Gets: @sName varchar(50)
-- Gets: @iFiscalYear int
-- Gets: @sSeason char(1)
-- Gets: @daStartDate datetime
-- Gets: @daEndDate datetime
-- Gets: @iUserIDChanged UserID_UDDT
-- Gets: @nDefaultConversionRate numeric(10,2)
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Season_Update]
	@iID int,
	@sCountry varchar(10),
	@sName varchar(50),
	@iFiscalYear int,
	@sSeason char(1),
	@daStartDate datetime,
	@daEndDate datetime,
	@iUserIDChanged UserID_UDDT,
	@nDefaultConversionRate numeric(10,2)
AS
SET NOCOUNT ON
-- UPDATE an existing row in the table.
UPDATE 	[dbo].[Season]
SET 	[Country] 			= @sCountry
	,[Name] 			= @sName
	,[FiscalYear]			= @iFiscalYear
	,[Season]			= @sSeason
	,[StartDate]			= @daStartDate
	,[EndDate]			= @daEndDate
	,[DateChanged] 			= GETDATE()
	,[UserIDChanged]		= @iUserIDChanged
	,[DefaultConversionRate]	= @nDefaultConversionRate
WHERE	[ID] = @iID
GO
