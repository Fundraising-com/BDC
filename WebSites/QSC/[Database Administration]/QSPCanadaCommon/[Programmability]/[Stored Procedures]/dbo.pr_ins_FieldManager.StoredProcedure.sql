USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_ins_FieldManager]    Script Date: 06/07/2017 09:33:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_ins_FieldManager]
	@FMID varchar(4) OUT,
	@Status int,
	@Country varchar(2),
	@PhoneListID int OUT,
	@AddressListID int OUT,
	@FirstName varchar(50),
	@LastName varchar(50),
	@MiddleInitial varchar(10),
	@Email varchar(60),
	@DMID varchar(4),
	@UserIDModified UserID_UDDT,
	@DateModified datetime,
	@Comment varchar(256),
	@DMIndicator bit,
	@Lang varchar(10)
AS

--convert to int, get the next FMID
DECLARE @fmidInt int
SELECT @fmidInt = max(cast(@FMID as int)) + 1
  FROM QSPCanadaCommon.dbo.FieldManager

--convert back to varchar
SELECT @FMID = cast(@fmidInt AS varchar)
while(len(@FMID) < 4)
begin
	SELECT @FMID = '0' + @FMID
end

-----------------------------------------------
--  Get a new PhoneListID   ---
-----------------------------------------------
insert into PhoneList(CreateDate) values(GetDate())	
SELECT @PhoneListID = @@Identity

-------------------------------------------------
--  Get a new AddressListID   ---
-------------------------------------------------
insert into AddressList(CreateDate) values(GetDate())	
SELECT @AddressListID = @@Identity


INSERT INTO FieldManager (
	FMID,
	Status,
	Country,
	PhoneListID,
	AddressListID,
	FirstName,
	LastName,
	MiddleInitial,
	Email,
	DMID,
	UserIDModified,
	DateModified,
	Comment,
	DMIndicator,
	Lang
)VALUES(
	@FMID,
	@Status,
	@Country,
	@PhoneListID,
	@AddressListID,
	@FirstName,
	@LastName,
	@MiddleInitial,
	@Email,
	@DMID,
	@UserIDModified,
	@DateModified,
	@Comment,
	case @DMIndicator
		when 1 then 'Y'
		when 0 then 'N'
	end,
	@Lang
);
GO
