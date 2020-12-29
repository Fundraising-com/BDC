USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_competitor_advertising_support]    Script Date: 02/14/2014 13:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Competitor_Advertising_Support
CREATE PROCEDURE [dbo].[efrcrm_insert_competitor_advertising_support] @Advertising_Support_ID int OUTPUT, @Competitor_Advertising_ID int AS
begin

insert into Competitor_Advertising_Support(Competitor_Advertising_ID) values(@Competitor_Advertising_ID)

select @Advertising_Support_ID = SCOPE_IDENTITY()

end
GO
