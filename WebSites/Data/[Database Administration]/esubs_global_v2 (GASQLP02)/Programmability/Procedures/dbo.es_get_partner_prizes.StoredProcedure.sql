USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_partner_prizes]    Script Date: 02/14/2014 13:06:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
	Created by:
	Date:
*/
CREATE PROCEDURE [dbo].[es_get_partner_prizes]
	@programID int
	, @partnerID int
AS
BEGIN
SELECT     
	ppp.program_id
	, ppp.partner_id
	, p.prize_id
	, pt.prize_type_id
	, p.prize_name
	, pt.prize_type_name
FROM         
	prize p
	INNER JOIN prize_type pt
	ON p.prize_type_id = pt.prize_type_id 
	INNER JOIN program_partner_prize ppp
	ON p.prize_id = ppp.prize_id
where 
	ppp.program_id = @programID 
and 	ppp.partner_id = @partnerID
END
GO
