USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_destinations]    Script Date: 02/14/2014 13:06:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Destinations
CREATE PROCEDURE [dbo].[efrcrm_insert_destinations] @Destination_ID int OUTPUT, @Web_Site_ID int, @URL varchar(200) AS
begin

insert into Destinations(Web_Site_ID, URL) values(@Web_Site_ID, @URL)

select @Destination_ID = SCOPE_IDENTITY()

end
GO
