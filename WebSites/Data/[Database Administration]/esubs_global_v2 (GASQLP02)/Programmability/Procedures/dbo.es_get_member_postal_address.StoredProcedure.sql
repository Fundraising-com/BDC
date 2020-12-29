USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_member_postal_address]    Script Date: 02/14/2014 13:05:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
CREATE PROCEDURE [dbo].[es_get_member_postal_address]
	@member_id int
AS
BEGIN
SELECT
    mpa.member_postal_address_id 
    , mpa.member_id
    , mpa.postal_address_id
    , mpa.active
	, pa.address_1
	, pa.address_2
	, pa.city
	, pa.zip_code
	, pa.country_code
	, pa.subdivision_code
	, pat.postal_address_type_id
	, pat.postal_address_type_name
FROM member_postal_address as mpa
	INNER JOIN postal_address AS pa
		ON mpa.postal_address_id = pa.postal_address_id
	INNER JOIN postal_address_type AS pat
		ON pat.postal_address_type_id = mpa.postal_address_type_id
	INNER JOIN country as c
		ON c.country_code = pa.country_code
	INNER JOIN subdivision sub
		ON sub.subdivision_code = pa.subdivision_code
WHERE mpa.member_id = @member_id
  --AND mpa.active = 1
END
GO
