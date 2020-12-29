USE [EFRCommon]
GO
/****** Object:  StoredProcedure [dbo].[efrc_get_partner_by_id]    Script Date: 02/14/2014 13:05:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[efrc_get_partner_by_id]

 @partnerID int
AS
BEGIN
SELECT     

 p.partner_id
 , p.partner_type_id
 , p.partner_name
 , p.has_collection_site
 , p.guid
 , pt.partner_type_name 

FROM partner p 

 inner join partner_type pt

  on pt.partner_type_id = p.partner_type_id

where 

 partner_id = @partnerID

END
GO
