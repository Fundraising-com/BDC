USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[crm_insert_new_activity]    Script Date: 02/14/2014 13:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE      procedure [dbo].[crm_insert_new_activity]
           @lead_id as int,
           @activity_type_id int
           
           
as
declare @id int
exec @id = sp_NewID  'Lead_Activity_Id','ALL'


INSERT INTO Lead_Activity
           (Lead_Activity_id,
            Lead_Id,
            Lead_Activity_Type_Id,
            comments,
            Lead_Activity_Date)
VALUES      (@id,
            @lead_id, 
            @activity_type_id,
            'Dispatcher',  
            getdate())
GO
