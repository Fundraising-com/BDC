USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_postal_address_to_member]    Script Date: 02/14/2014 13:06:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Payment
CREATE PROCEDURE [dbo].[es_insert_postal_address_to_member] @Member_postal_address_id int out, @postal_address_id int, @postal_address_type_id int, @member_id int, @Create_date datetime AS
begin

insert into member_postal_address
(postal_address_id, member_id, postal_address_type_id, active, create_date)
  values (@postal_address_id, @member_id, @postal_address_type_id, 1, @Create_date)

select @Member_postal_address_id = SCOPE_IDENTITY()

end
GO
