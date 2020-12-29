USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_lead_activity]    Script Date: 02/14/2014 13:08:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[es_insert_lead_activity]
	@lead_id int
	, @lead_activity_type_id int
as

declare @intErrorCode int
declare @intlead_activity_id int

BEGIN TRANSACTION

 SET @intErrorCode = @@ERROR

 EXEC @intlead_activity_id = sp_newid 'lead_activity_id', 'all' 
 SET @intErrorCode = @@ERROR

 IF @intErrorCode = 0 AND @intlead_activity_id  > 0 
 begin
  	SET @intErrorCode = 0
 end
 ELSE 
 begin
  	SET @intErrorCode = -1
	rollback transaction
	return @intErrorCode
 end



  INSERT INTO lead_activity(
    lead_activity_id
    , lead_id
    , lead_activity_type_id
    , lead_activity_date
  )
  Values (
    @intlead_activity_id
    , @lead_id
    , @lead_activity_type_id
    , getdate()
  )

 SET @intErrorCode = @@ERROR
 IF @intErrorCode = 0 
 begin
  	SET @intErrorCode = 0
	commit transaction
	select @intlead_activity_id as lead_activity_id
 end
 ELSE 
 begin
  	SET @intErrorCode = -1
	rollback transaction
	select @intErrorCode as lead_activity_id
 end
GO
