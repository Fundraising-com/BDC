USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_targeted_market]    Script Date: 02/14/2014 13:07:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Targeted_Market
CREATE PROCEDURE [dbo].[efrcrm_insert_targeted_market] @Targeted_Market_ID int OUTPUT, @Targeted_Market_Type_ID int, @Advertising_Support_ID int, @Target_Market_Type_ID int, @Seasoner bit, @Age_Range varchar(25), @Education_Level varchar(25), @Description varchar(50), @Comments varchar(255) AS
begin

insert into Targeted_Market(Targeted_Market_Type_ID, Advertising_Support_ID, Target_Market_Type_ID, Seasoner, Age_Range, Education_Level, Description, Comments) values(@Targeted_Market_Type_ID, @Advertising_Support_ID, @Target_Market_Type_ID, @Seasoner, @Age_Range, @Education_Level, @Description, @Comments)

select @Targeted_Market_ID = SCOPE_IDENTITY()

end
GO
