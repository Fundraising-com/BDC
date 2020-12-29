USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_new_lead]    Script Date: 02/14/2014 13:08:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[es_insert_new_lead]
	@promotion_id as int	
	, @first_name as varchar(50)
	, @last_name as varchar(50)
	, @organization as varchar(100)
	, @street_address as varchar(100)
	, @city as varchar(50)
	, @state_code as varchar(10)
	, @country_code as varchar(10)
	, @zip_code as varchar(10)
	, @day_phone as varchar(20)
	, @email as varchar (50)
	, @participant_count int
as 
declare @intErrorCode INT
declare @intLeadID int

if @state_code is null
begin
	set @state_code = 'n/a'
end

if not exists (select * from state where state_code = @state_code)
begin
	set @state_code = 'n/a'
end



BEGIN TRANSACTION

 SET @intErrorCode = @@ERROR

 EXEC @intLeadID = newid 'Lead_ID', 'ALL'
 SET @intErrorCode = @@ERROR

 IF @intErrorCode = 0 AND @intLeadID > 0 
 begin
  	SET @intErrorCode = 0
 end
 ELSE 
 begin
  	SET @intErrorCode = -1
	rollback transaction
	--select @intErrorCode as lead_id
	return @intErrorCode
 end



  INSERT INTO Lead
  (
   lead_id
   , lead_priority_id
   , promotion_id
   , channel_code
   , first_name
   , last_name
   , organization
   , street_address
   , city
   , state_code
   , country_code
   , zip_code
   , day_phone
   , email
   , participant_count
  )
  VALUES
  (
    @intLeadID
    , 1
    , @promotion_id 
    , 'INT'
    , @first_name
    , @last_name 
    , @organization 
    , @street_address 
    , @city 
    , @state_code 
    , @country_code 
    , @zip_code 
    , @day_phone 
    , @email 
    , @participant_count 
  )
 SET @intErrorCode = @@ERROR
 IF @intErrorCode = 0 
 begin
  	SET @intErrorCode = 0
	commit transaction
	--select @intLeadID as lead_id
	--return @intErrorCode
	return  @intLeadID
 end
 ELSE 
 begin
  	SET @intErrorCode = -1
	rollback transaction
	--select @intErrorCode as lead_id
	return @intErrorCode
 end
GO
