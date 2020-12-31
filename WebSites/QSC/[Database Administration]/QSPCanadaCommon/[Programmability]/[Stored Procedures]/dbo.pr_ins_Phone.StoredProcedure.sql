USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_ins_Phone]    Script Date: 06/07/2017 09:33:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE  PROCEDURE [dbo].[pr_ins_Phone]
	@Type int,
	@PhoneListID int,
	@PhoneNumber varchar(50),
	@BestTimeToCall varchar(2000),
	@Phone_ID int output
AS
INSERT INTO Phone (
	Type,
	PhoneListID,
	PhoneNumber,
	BestTimeToCall
)VALUES(
	@Type,
	@PhoneListID,
	@PhoneNumber,
	@BestTimeToCall)

select @Phone_ID = @@Identity
GO
