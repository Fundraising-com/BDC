USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_member_phone_number]    Script Date: 02/14/2014 13:05:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
	Created by: JF Buist
	Date: 
*/
create PROCEDURE [dbo].[es_get_member_phone_number]
	@member_id int
AS
BEGIN
SELECT     
	mpn.phone_number_id
	, mpn.member_id
	, pn.phone_number
	, mpn.active
	, pnt.phone_number_type_id
	, pnt.phone_number_type_name
FROM       
	member_phone_number mpn
	INNER JOIN phone_number pn
	ON mpn.phone_number_id = pn.phone_number_id
	INNER JOIN phone_number_type pnt
	ON mpn.phone_number_type_id = pnt.phone_number_type_id
where 
	mpn.member_id = @member_id
END
GO
