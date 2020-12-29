USE [fastfundraising]
GO
/****** Object:  StoredProcedure [dbo].[efr_store_get_ffitemprice_by_itemid_tiercode]    Script Date: 02/14/2014 13:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efr_store_get_ffitemprice_by_itemid_tiercode]
	-- Add the parameters for the stored procedure here
	@itemid int,
	@tierid int
AS
BEGIN

	SET NOCOUNT ON;

 	select * from ffitemprice where itemid=@itemid and tierid=@tierid

END
GO
