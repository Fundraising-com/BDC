USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_lead_promotion]    Script Date: 02/14/2014 13:07:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Lead_Promotion
CREATE PROCEDURE [dbo].[efrcrm_insert_lead_promotion] @Lead_Promotion_Id int OUTPUT, @Lead_Id int, @Promotion_Id int, @Entry_Date smalldatetime AS
begin

insert into Lead_Promotion(Lead_Id, Promotion_Id, Entry_Date) values(@Lead_Id, @Promotion_Id, @Entry_Date)

select @Lead_Promotion_Id = SCOPE_IDENTITY()

end
GO
