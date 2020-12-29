USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_partner_culture_link]    Script Date: 02/14/2014 13:06:08 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
/*

 Created by: Jiro Hidaka

 Date:          November 14, 2008

*/

CREATE  PROCEDURE [dbo].[es_get_partner_culture_link]

 @partnerID int

AS

BEGIN



SELECT     

 partner_id

 ,culture_code

 ,linked_partner_id

FROM partner_culture_link

where 

 partner_id = @partnerID

END
GO
