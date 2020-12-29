USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_lead_visit]    Script Date: 02/14/2014 13:08:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[es_insert_lead_visit]
	@lead_id int
	, @promotion_id int
	, @temp_lead_id int = NULL
as

declare @intErrorCode int
declare @intlead_visit_id int
declare @channel_code as varchar(5)

BEGIN TRANSACTION

 SET @intErrorCode = @@ERROR

 EXEC @intlead_visit_id = sp_newid 'lead_visit_id', 'all' 
 SET @intErrorCode = @@ERROR

 IF @intErrorCode = 0 AND @intlead_visit_id  > 0 
 begin
  	SET @intErrorCode = 0
 end
 ELSE 
 begin
  	SET @intErrorCode = -1
	rollback transaction
	select @intErrorCode as  lead_visit_id
 end


 select 
	@channel_code = channel_code
 from 
	lead
 where 
  	lead_id = @lead_id

 SET @intErrorCode = @@ERROR

 IF @intErrorCode = 0  
 begin
  	SET @intErrorCode = 0
 end
 ELSE 
 begin
  	SET @intErrorCode = -1
	rollback transaction
	select @intErrorCode as  lead_visit_id
 end

  INSERT INTO lead_visit(
    lead_visit_id
    , promotion_id	
    , lead_id
    , temp_lead_id
    , visit_date
    , channel_code
  )
  Values (
    @intlead_visit_id
    ,@promotion_id
    , @lead_id
    , @temp_lead_id
    , getdate()
    , @channel_code
  )

 SET @intErrorCode = @@ERROR
 IF @intErrorCode = 0 
 begin
  	SET @intErrorCode = 0
	commit transaction
	select @intlead_visit_id as lead_visit_id
	return @intlead_visit_id
 end
 ELSE 
 begin
  	SET @intErrorCode = -1
	rollback transaction
	select @intErrorCode as lead_visit_id
 end
GO
