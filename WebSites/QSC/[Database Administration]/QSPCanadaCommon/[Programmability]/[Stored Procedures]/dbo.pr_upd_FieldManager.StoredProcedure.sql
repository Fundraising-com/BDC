USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_upd_FieldManager]    Script Date: 06/07/2017 09:33:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_upd_FieldManager]
	@FMID varchar(4),
	@Status int,
	@Country varchar(2),
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

UPDATE 
	FieldManager 
   SET 
	Status          = @Status,
	Country         = @Country,
	FirstName       = @FirstName,
	LastName        = @LastName,
	MiddleInitial   = @MiddleInitial,
	Email           = @Email,
	DMID            = @DMID,
	UserIDModified  = @UserIDModified,
	DateModified    = @DateModified,
	Comment         = @Comment,
	DMIndicator     = case @DMIndicator
				when 1 then 'Y'
				when 0 then 'N'
			  end,
	Lang            = @Lang
WHERE 
	FMID = @FMID
GO
