USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_lead_visit]    Script Date: 02/14/2014 13:07:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Lead_Visit
CREATE PROCEDURE [dbo].[efrcrm_insert_lead_visit] @Lead_Visit_ID int OUTPUT, @Promotion_ID int, @Lead_ID int, @Temp_Lead_ID int, @Visit_Date datetime, @Channel_Code varchar(4) AS

declare @id int
exec @id = sp_NewID  'Lead_Visit_ID', 'All'

begin

insert into Lead_Visit(Lead_Visit_ID, Promotion_ID, Lead_ID, Temp_Lead_ID, Visit_Date, Channel_Code) values(@id, @Promotion_ID, @Lead_ID, @Temp_Lead_ID, @Visit_Date, @Channel_Code)

end
GO
