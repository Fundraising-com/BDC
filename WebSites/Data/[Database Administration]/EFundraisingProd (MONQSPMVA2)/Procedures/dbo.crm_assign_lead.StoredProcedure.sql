USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[crm_assign_lead]    Script Date: 02/14/2014 13:03:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE         procedure [dbo].[crm_assign_lead]
           @lead_id as int,
           @consultant_id int,
           @assigner_id int,
           @kit_id int,
           @transfer_date datetime = null
   
           
as


--declare @transfer_date datetime

--if @transfer
  -- set @transfer_date = getdate()

DECLARE @errorCode int	
declare @assignment_date datetime



--Check if lead is unassigned
if exists 
        (select * from lead where lead_id = @lead_id and consultant_id = 0)
begin
 
--update lead set fk_kit_type_id = @kit_id where lead_id = @Lead_id

SELECT @assignment_date = Lead_Assignment_Date FROM Lead WHERE lead_id = @lead_id

   if @assignment_date is null
   begin
      set @assignment_date = getdate()
   end

   UPDATE Lead 
   SET    consultant_Id = @consultant_id,
          Lead_Assignment_Date = @assignment_date,
          Assigner_ID = @assigner_id,
          Lead_Priority_Id = 1,
          transfered_date = @transfer_date 
   WHERE  Lead_ID = @lead_id

   SET @errorCode = @@error
	
   IF (@errorCode <> 0)
   begin
	return -2
   END
   else
   begin
      return 0
   end

end
else
begin
   return -1
end
GO
