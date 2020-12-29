USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[cc_update_account_info]    Script Date: 02/14/2014 13:04:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  procedure [dbo].[cc_update_account_info]
	@event_id int,
        @active bit,
        @payment_name nvarchar(50),
        @event_name nvarchar(50),
        @payment_info_changed bit = null
as

begin transaction 

declare @postal_address_id int
declare @on_behalf_of_name varchar(50)
declare @ship_to_name varchar(50)
declare @ssn varchar(50)
declare @groupID int
select @groupid = group_id from event_group where event_id = @event_id



UPDATE    event
SET     event_name = @event_name,   
        active = @active

WHERE   
	 event_ID = @event_id

if @@error <> 0
begin
	rollback transaction
	return -1 --une erreur
end

--if @payment_info_changed = 1
--begin
   

   ---REGARDE SI IL YA QQCHOSE AVANT
   select @postal_address_id = postal_address_id,
          @on_behalf_of_name = on_behalf_of_name,
          @ship_to_name = ship_to_name,
          @ssn = ssn
   from payment_info
   where group_id = @groupID
             

   update payment_info set active = 0 where group_id = @groupID


   insert into payment_info 
   (
        group_id
        ,postal_address_id
        ,payment_name
        ,on_behalf_of_name
        ,ship_to_name
        ,ssn
        ,active
        ,create_date
   )
   values (@groupID,
        @postal_address_id,
        @payment_name,
        @on_behalf_of_name,
        @ship_to_name, 
        @ssn,
        1,
        getdate())



if @@error <> 0
begin
	rollback transaction
	return -1 --une erreur
end
--end
commit transaction
return  0
GO
