USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[crm_get_unassigned_girls_scout_leads]    Script Date: 02/14/2014 13:03:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		JF Lavigne
-- ALTER  date: <ALTER  Date,,>
-- Description:	<Description,,>
--
-- =============================================
CREATE        procedure [dbo].[crm_get_unassigned_girls_scout_leads]
	
as

SELECT  l.lead_id
       , left(l.country_code,2) as country_code
        ,l.fk_kit_type_id
       , l.lead_entry_date
       , l.lead_assignment_date AS Assignment_Date
       , l.organization
       , c.ext_consultant_id
       , c.name as ext_consultant,
          l.state_code + ' (' + left(l.country_code,2) + ')' as country_code 
       , l.participant_count AS Part,
         l.day_phone,
         l.evening_phone, 
         l.lead_status_id, 
         l.channel_code AS Channel
    

FROM Lead l LEFT JOIN
     consultant c on l.ext_consultant_id = c.consultant_id
               
where l.consultant_id = 0 and lead_entry_date > '2005-09-01' and (
      organization like '%scout%' or 
      organization like '%brownie%' or
      organization like '%troop%')
     and organization not like '%boy%'
     and 1=0 -- Done by Javier Arellano to accomplish US # 13043
GO
