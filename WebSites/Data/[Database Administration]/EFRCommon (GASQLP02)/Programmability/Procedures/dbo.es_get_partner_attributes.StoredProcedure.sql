USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[es_get_partner_attributes]    Script Date: 02/14/2014 13:05:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
	Created by:
	Date:
*/
CREATE PROCEDURE [dbo].[es_get_partner_attributes]
	@partnerID int
	, @cultureCode varchar(10) = 'en-US'
AS
BEGIN
SELECT     
	dbo.partner_attribute.partner_attribute_id
	, dbo.partner_attribute.partner_attribute_name
	, dbo.partner_attribute_value.culture_code
	, dbo.partner_attribute_value.[value]
FROM         
	dbo.partner_attribute 
	INNER JOIN dbo.partner_attribute_value 
	ON dbo.partner_attribute.partner_attribute_id = dbo.partner_attribute_value.partner_attribute_id
WHERE     
	(dbo.partner_attribute_value.partner_id = @partnerID) 
AND 	(dbo.partner_attribute_value.culture_code = @cultureCode)
END
GO
