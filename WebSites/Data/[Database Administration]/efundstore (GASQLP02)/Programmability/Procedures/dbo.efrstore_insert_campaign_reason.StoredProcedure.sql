USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_campaign_reason]    Script Date: 02/14/2014 13:05:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Campaign_reason
CREATE PROCEDURE [dbo].[efrstore_insert_campaign_reason] @Campaign_reason_id int OUTPUT, @Party_type_id tinyint, @Description varchar(50) AS
begin

insert into Campaign_reason(Party_type_id, Description) values(@Party_type_id, @Description)

select @Campaign_reason_id = SCOPE_IDENTITY()

end
GO
