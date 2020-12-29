USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_lead_visit]    Script Date: 02/14/2014 13:08:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Lead_Visit
CREATE PROCEDURE [dbo].[efrcrm_update_lead_visit] @Lead_Visit_ID int, @Promotion_ID int, @Lead_ID int, @Temp_Lead_ID int, @Visit_Date datetime, @Channel_Code varchar(4) AS
begin

update Lead_Visit set Promotion_ID=@Promotion_ID, Lead_ID=@Lead_ID, Temp_Lead_ID=@Temp_Lead_ID, Visit_Date=@Visit_Date, Channel_Code=@Channel_Code where Lead_Visit_ID=@Lead_Visit_ID

end
GO
