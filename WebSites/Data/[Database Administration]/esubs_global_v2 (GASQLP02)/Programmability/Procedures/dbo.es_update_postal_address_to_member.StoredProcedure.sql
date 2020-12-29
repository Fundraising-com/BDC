USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_postal_address_to_member]    Script Date: 02/14/2014 13:08:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Payment
CREATE PROCEDURE [dbo].[es_update_postal_address_to_member] @Member_postal_address_id int, @postal_address_id int, @postal_address_type_id int, @member_id int, @Create_date datetime, @active int AS
begin

update member_postal_address
set member_id = @member_id
	, postal_address_type_id = @postal_address_type_id
	, active = @active
	, create_date = @create_date
where postal_address_id = @postal_address_id;

end
GO
