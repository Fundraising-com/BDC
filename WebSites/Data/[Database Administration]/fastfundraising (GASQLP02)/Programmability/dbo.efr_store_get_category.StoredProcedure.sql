USE [fastfundraising]
GO
/****** Object:  StoredProcedure [dbo].[efr_store_get_category]    Script Date: 02/14/2014 13:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efr_store_get_category]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	select * from category where status=1 order by categoryname

END
GO
