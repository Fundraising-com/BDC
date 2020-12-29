USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_advertiser_partner]    Script Date: 02/14/2014 13:06:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Advertiser_Partner
CREATE PROCEDURE [dbo].[efrcrm_insert_advertiser_partner] @Advertiser_Partner_ID int OUTPUT, @Advertiser_ID int, @Description varchar(200) AS
begin

insert into Advertiser_Partner(Advertiser_ID, Description) values(@Advertiser_ID, @Description)

select @Advertiser_Partner_ID = SCOPE_IDENTITY()

end
GO
