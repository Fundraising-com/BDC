USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_competitor]    Script Date: 02/14/2014 13:07:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Competitor
CREATE PROCEDURE [dbo].[efrcrm_update_competitor] @Competitor_ID int, @Business_Name varchar(100), @Comments varchar(255) AS
begin

update Competitor set Business_Name=@Business_Name, Comments=@Comments where Competitor_ID=@Competitor_ID

end
GO
