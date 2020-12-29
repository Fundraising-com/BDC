USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[crm_unassign_lead]    Script Date: 02/14/2014 13:03:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[crm_unassign_lead]
           @lead_id as int,
           @consultant_id int,
           @user_id as int
     
          
           
as


UPDATE Lead SET Consultant_Id = 0 WHERE lead_id = @lead_id

declare @id int
exec @id = newid 'UnAssignLogin_ID', 'All'

INSERT INTO UnAssignLogin 
VALUES  (@id,
        @user_id,
        @consultant_id,
        @lead_id,
        getdate())
GO
