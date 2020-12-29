USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[qsppr_reserved_account_Assign]    Script Date: 02/14/2014 13:08:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[qsppr_reserved_account_Assign]
	@business_division_id int
	,@prefix varchar(10)
	,@clone_account_id int
	,@clone_fulf_account_id int
	,@user_id int
	,@out_account_id int output
	,@out_fulf_account_id int output
AS
BEGIN
	
	DECLARE @RC int
	DECLARE @x_out_account_id int
	DECLARE @x_out_fulf_account_id int

	

	EXECUTE @RC = [OEPROD].[QSPFulfillment].[dbo].[pr_reserved_account_Assign] 
	@business_division_id = @business_division_id
	, @prefix = @prefix
	, @clone_account_id = @clone_account_id
	, @clone_fulf_account_id = @clone_fulf_account_id
	, @user_id = @user_id
	, @out_account_id = @out_account_id OUTPUT 
	, @out_fulf_account_id = @out_fulf_account_id OUTPUT 

END
GO
