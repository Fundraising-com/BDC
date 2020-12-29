USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_carrier]    Script Date: 02/14/2014 13:05:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Carrier
CREATE PROCEDURE [dbo].[efrstore_insert_carrier] @Carrier_id int OUTPUT, @Description varchar(50) AS
begin

insert into Carrier(Description) values(@Description)

select @Carrier_id = SCOPE_IDENTITY()

end
GO
