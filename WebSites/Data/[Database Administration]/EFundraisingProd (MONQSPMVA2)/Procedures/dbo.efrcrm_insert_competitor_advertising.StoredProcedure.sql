USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_competitor_advertising]    Script Date: 02/14/2014 13:06:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Competitor_Advertising
CREATE PROCEDURE [dbo].[efrcrm_insert_competitor_advertising] @Competitor_Advertising_ID int OUTPUT, @Competitor_ID int, @Description varchar(50), @Publicity_Duration varchar(25), @Comments varchar(255) AS
begin

insert into Competitor_Advertising(Competitor_ID, Description, Publicity_Duration, Comments) values(@Competitor_ID, @Description, @Publicity_Duration, @Comments)

select @Competitor_Advertising_ID = SCOPE_IDENTITY()

end
GO
