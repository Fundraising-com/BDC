USE [fastfundraising]
GO
/****** Object:  StoredProcedure [dbo].[efr_store_find_text_in_ffitems]    Script Date: 02/14/2014 13:06:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efr_store_find_text_in_ffitems]
	@searchtxt varchar(40)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from ffitems where status=1 and (itemname like '%' + @searchtxt + '%' OR description like '%' + @searchtxt + '%' OR flavors like '%'+ @searchtxt + '%');
END
GO
