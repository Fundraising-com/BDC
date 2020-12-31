USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_CUserProfile_CreateUser_Resolve]    Script Date: 06/07/2017 09:33:17 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[pr_CUserProfile_CreateUser_Resolve]

	@Firstname		varchar(50),
	@Lastname		varchar(50),
	@CreatedByID	int

AS

DECLARE	@Instance	int

EXEC pr_CUserProfile_CreateUser @Firstname = @Firstname, @Lastname = @Lastname, @FMID = '9999', @CreatedByID = @CreatedByID, @Instance = @Instance OUTPUT

EXEC pr_upd_UserPermissions @ProfileID = @Instance, @PName = 'CustomerService';
EXEC pr_upd_UserPermissions @ProfileID = @Instance, @PName = 'Resolve';
GO
