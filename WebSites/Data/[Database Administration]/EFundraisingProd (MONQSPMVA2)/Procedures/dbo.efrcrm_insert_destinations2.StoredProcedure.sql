USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_destinations2]    Script Date: 02/14/2014 13:06:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Destinations2
CREATE PROCEDURE [dbo].[efrcrm_insert_destinations2] @Destination_ID int OUTPUT, @Web_Site_Id int, @URL varchar(200) AS
begin

insert into Destinations2(Web_Site_Id, URL) values(@Web_Site_Id, @URL)

select @Destination_ID = SCOPE_IDENTITY()

end
GO
