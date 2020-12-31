USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_Season_Insert]    Script Date: 06/07/2017 09:33:29 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will insert 1 row in the table 'Season'
-- Gets: @sCountry varchar(10)
-- Gets: @sName varchar(50)
-- Gets: @iFiscalYear int
-- Gets: @sSeason char(1)
-- Gets: @daStartDate datetime
-- Gets: @daEndDate datetime
-- Gets: @iUserIDChanged UserID_UDDT
-- Gets: @nDefaultConversionRate numeric(10,2)
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Season_Insert]

	@sCountry varchar(10),
	@sName varchar(50),
	@iFiscalYear int, 
	@sSeason char(1),
	@daStartDate datetime,
	@daEndDate datetime,
	@iUserIDChanged UserID_UDDT,
	@nDefaultConversionRate numeric(10,2)
AS
-- INSERT a new row in the table.
INSERT INTO [dbo].[Season]
(
	[Country]
	,[Name]
	,[FiscalYear]
	,[Season]
	,[StartDate]
	,[EndDate]
	,[DateChanged]
	,[UserIDChanged]
	,[DefaultConversionRate]
)
VALUES
(
	@sCountry
	,@sName
	,@iFiscalYear
	,@sSeason
	,@daStartDate
	,@daEndDate
	,GETDATE()
	,@iUserIDChanged
	,@nDefaultConversionRate
)
GO
