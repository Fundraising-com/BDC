USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[crm_get_lead_comments]    Script Date: 02/14/2014 13:03:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[crm_get_lead_comments] --10000
           @lead_id as int
   
           
as

Select entry_date,
       comments 
from   comments 
where  Lead_Id = @lead_id
order by entry_date desc
GO
