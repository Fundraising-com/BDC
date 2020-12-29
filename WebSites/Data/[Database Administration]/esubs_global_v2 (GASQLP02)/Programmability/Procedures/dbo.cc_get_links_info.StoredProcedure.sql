USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_get_links_info]    Script Date: 03/06/2015 01:58:27 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
Created by :    jf lavigne
Created on : 	2005-01-25
Description:	This stored procedure gets all the info of an account
*/

--sinon on utilise le lien par le member_address. Il peut y avoir 2 addresses pour le membere donc un fait un max du type (pour avoir le business address en priorite)


ALTER   PROCEDURE [dbo].[cc_get_links_info] 
	@event_participation_id INT
AS

SELECT    p.redirect,
          ep.event_id,
          ep.member_hierarchy_id
FROM      dbo.event_participation ep left join
          dbo.personalization p on ep.event_participation_id=p.event_participation_id
WHERE    ep.event_participation_id = @event_participation_id