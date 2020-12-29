USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_competitor_advertising]    Script Date: 02/14/2014 13:07:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Competitor_Advertising
CREATE PROCEDURE [dbo].[efrcrm_update_competitor_advertising] @Competitor_Advertising_ID int, @Competitor_ID int, @Description varchar(50), @Publicity_Duration varchar(25), @Comments varchar(255) AS
begin

update Competitor_Advertising set Competitor_ID=@Competitor_ID, Description=@Description, Publicity_Duration=@Publicity_Duration, Comments=@Comments where Competitor_Advertising_ID=@Competitor_Advertising_ID

end
GO
