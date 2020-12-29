USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_partner]    Script Date: 02/14/2014 13:06:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*

 Created by: JF Buist

 Date:

*/

CREATE  PROCEDURE [dbo].[es_get_partner]

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
