USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[crm_get_assigned_leads]    Script Date: 02/14/2014 13:03:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE       procedure [dbo].[crm_get_assigned_leads]
           @consultant_id as int,
           @entry_start_date as datetime = null,
           @entry_end_date as datetime = null,
           @assign_start_date as datetime = null,
           @assign_end_date as datetime = null

as




if (@consultant_id > 0)
begin




if @entry_start_date is null
begin
   set @entry_start_date = '2001-01-01'
end
if @entry_end_date is null
begin
   set @entry_end_date = getdate()
end

if @assign_start_date is null
begin
   set @assign_start_date = '2001-01-01'
end
if @assign_end_date is null
begin
   set @assign_end_date = getdate()
end


SELECT   l.lead_id
       , case when v.Lead_ID is null then 'Yes' else 'No' end as Called
       , l.lead_entry_date
       , l.lead_assignment_date AS Assignment_Date
       , l.organization
       , pt.promotion_type_name  AS Promo_Type
       , p.promotion_name AS Promo
       , gt.description AS Group_Type

FROM Lead l INNER JOIN
     EFRCommon..promotion  p ON l.promotion_id = p.promotion_id INNER JOIN 
     efrcommon..promotion_type  pt ON p.promotion_type_code = pt.Promotion_Type_Code LEFT JOIN 
     Group_Type gt ON l.group_type_id = gt.group_type_id LEFT JOIN 
           (SELECT DISTINCT l.lead_id 
               FROM Lead l INNER JOIN
               Lead_Activity la ON l.lead_id = la.lead_id
            WHERE la.lead_activity_type_id=2 AND 
                la.completed_date Is Null
                ) v on l.lead_id = v.Lead_ID 
where l.consultant_id = @consultant_id
      and  (l.lead_entry_date between @entry_start_date and @entry_end_date + '23:23:59')
      and  (l.lead_assignment_date between @assign_start_date and @assign_end_date + '23:23:59')
ORDER BY l.lead_assignment_date DESC
        ,l.lead_id DESC

end
else
begin
   select * from consultant where 1=2
end
GO
