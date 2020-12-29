USE [fastfundraising]
GO
/****** Object:  StoredProcedure [dbo].[efr_store_get_items_by_catid]    Script Date: 02/14/2014 13:06:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[efr_store_get_items_by_catid]
	@catid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

select i.*, cat.categoryname 
from ffitems i inner join category cat on cat.categoryid=i.categoryid 
where i.status=1 and cat.categoryid=@catid 
order by itemname
END
GO
