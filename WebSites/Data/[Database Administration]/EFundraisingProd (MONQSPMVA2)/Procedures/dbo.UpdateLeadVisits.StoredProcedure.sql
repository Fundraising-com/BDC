USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[UpdateLeadVisits]    Script Date: 02/14/2014 13:09:14 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE      PROCEDURE [dbo].[UpdateLeadVisits] 
AS
   
    /*CLOSE rsLead;
    Deallocate rsLead;
*/
   declare @leadID int;
   declare @channelCode varchar(20);
   declare @promotionID int
   declare @date datetime;

   DECLARE @ReturnStatus INT
   DECLARE @id INT

   declare @i int;
   set @i = 0;

   declare rsLead cursor for
      SELECT Lead.Lead_ID, Lead.Channel_Code, Lead.Promotion_ID, Lead.Lead_Assignment_Date
      FROM lead_visit RIGHT JOIN Lead ON lead_visit.Lead_ID = Lead.Lead_ID
      GROUP BY Lead.Lead_ID, Lead.Channel_Code, Lead.Promotion_ID, Lead.Lead_Assignment_Date, lead_visit.Lead_ID
      HAVING (((Lead.Lead_Assignment_Date) Is Not Null) AND ((lead_visit.Lead_ID) Is Null));

     OPEN rsLead;
     fetch next from rsLead into @leadID, @channelCode, @promotionID, @date;
     WHILE @@fetch_status = 0 BEGIN
        set @i = @i + 1;
         -- print @leadID
          EXECUTE @ReturnStatus = dbo.NewID2 @sIDName = 'Lead_Visit_ID', @sContext = 'All',
                  @MyNewID = @id OUTPUT
         
          insert into lead_visit values (@id,@promotionID,@LeadID,null,@date,@channelCode)
     
     fetch next from rsLead into @leadID, @channelCode, @promotionID, @date;        
     END
       
     

    CLOSE rsLead;
    Deallocate rsLead;
GO
