USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_call_qspecommerce_sp_ValidateAccountID]    Script Date: 02/14/2014 13:05:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_call_qspecommerce_sp_ValidateAccountID]
	@ifulf_account_id int , 
	@ibusiness_division_id int 

AS

exec QSPEcommerce.dbo.sp_ValidateAccountID @ifulf_account_id, @ibusiness_division_id
GO
