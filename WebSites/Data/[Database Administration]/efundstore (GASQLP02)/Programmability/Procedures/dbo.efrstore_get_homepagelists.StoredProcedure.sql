USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_homepagelists]    Script Date: 02/14/2014 13:05:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		jason Farrell
-- Create date: November 11, 2010
-- Description:	Get catagory list and links info for special products section on hompage
-- =============================================
CREATE PROCEDURE [dbo].[efrstore_get_homepagelists]
	-- Add the parameters for the stored procedure here
	@package_category_id AS int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT 
		ppc.package_id
		,ppc.package_category_id
		,ppc.display_order
		,p.name
		,pd.page_name
From package_package_category ppc 
	INNER JOIN  dbo.package_desc pd on ppc.package_id = pd.package_id
	INNER JOIN  dbo.package p on ppc.package_id = p.package_id
where ppc.package_category_id = @package_category_id order by ppc.display_order 
END
GO
