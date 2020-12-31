USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_Lead_Update]    Script Date: 06/07/2017 09:20:12 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
---------------------------------------------------------------------------------
-- Stored procedure that will insert 1 row in the table 'Lead'
-- Gets: @daDate datetime
-- Gets: @daDateSent
-- Gets: @sContactName varchar(14)
-- Gets: @sContactHomePhoneNumber varchar(14)
-- Gets: @sContactWorkPhoneNumber varchar(14)
-- Gets: @sContactFaxNumber varchar(14)
-- Gets: @sContactEMail varchar(50)
-- Gets: @sSchoolGroup varchar(50)
-- Gets: @sCityTown varchar(50)
-- Gets: @sProvince varchar(20)
-- Gets: @sInterestedInWhat varchar(250)
-- Gets: @sWhereHearAboutQSP varchar(250)
-- Gets: @sFMID varchar(4)
-- Returns: @iUserID int
---------------------------------------------------------------------------------
CREATE PROCEDURE [dbo].[pr_Lead_Update]
	
	@daDateSent datetime,
	@sContactName varchar(100),
	@sContactHomePhoneNumber varchar(14),
	@sContactWorkPhoneNumber varchar(14),
	@sContactFaxNumber varchar(14) = null,
	@sContactEMail varchar(50),
	@sSchoolGroup varchar(50),
	@sCityTown varchar(50),
	@sProvince varchar(20),
	@sInterestedInWhat varchar(250),
	@sWhereHearAboutQSP varchar(250),
	@sComments varchar(250),
	@sFMID varchar(4),
	@iUserID int,
	@iInstance int
AS
-- INSERT a new row in the table.
update [dbo].[Lead]

set
	[DateSent] = @daDateSent,
	[ContactName] =@sContactName,
	[ContactHomePhoneNumber]=@sContactHomePhoneNumber,
	[ContactWorkPhoneNumber]=@sContactWorkPhoneNumber,
	[ContactFaxNumber]=@sContactFaxNumber,
	[ContactEMail]=@sContactEMail,
	[SchoolGroup]=@sSchoolGroup,
	[CityTown]=@sCityTown,
	[Province]=@sProvince,
	[InterestedInWhat]=@sInterestedInWhat,
	[WhereHearAboutQSP]=@sWhereHearAboutQSP,
	[Comments]=@sComments,
	[FMID]=@sFMID
where Instance = @iInstance
GO
