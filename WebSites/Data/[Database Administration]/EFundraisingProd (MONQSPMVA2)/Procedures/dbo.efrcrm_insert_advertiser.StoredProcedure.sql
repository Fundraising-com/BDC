USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_advertiser]    Script Date: 02/14/2014 13:06:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Advertiser
CREATE PROCEDURE [dbo].[efrcrm_insert_advertiser] @Advertiser_ID int OUTPUT, @Advertisment_Type_ID int, @Contact_ID int, @Advertiser_Name varchar(200) AS
begin

insert into Advertiser(Advertisment_Type_ID, Contact_ID, Advertiser_Name) values(@Advertisment_Type_ID, @Contact_ID, @Advertiser_Name)

select @Advertiser_ID = SCOPE_IDENTITY()

end
GO
