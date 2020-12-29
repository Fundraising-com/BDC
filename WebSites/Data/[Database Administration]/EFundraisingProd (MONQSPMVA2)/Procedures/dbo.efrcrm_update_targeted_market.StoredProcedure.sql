USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_targeted_market]    Script Date: 02/14/2014 13:08:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Targeted_Market
CREATE PROCEDURE [dbo].[efrcrm_update_targeted_market] @Targeted_Market_ID int, @Targeted_Market_Type_ID int, @Advertising_Support_ID int, @Target_Market_Type_ID int, @Seasoner bit, @Age_Range varchar(25), @Education_Level varchar(25), @Description varchar(50), @Comments varchar(255) AS
begin

update Targeted_Market set Targeted_Market_Type_ID=@Targeted_Market_Type_ID, Advertising_Support_ID=@Advertising_Support_ID, Target_Market_Type_ID=@Target_Market_Type_ID, Seasoner=@Seasoner, Age_Range=@Age_Range, Education_Level=@Education_Level, Description=@Description, Comments=@Comments where Targeted_Market_ID=@Targeted_Market_ID

end
GO
