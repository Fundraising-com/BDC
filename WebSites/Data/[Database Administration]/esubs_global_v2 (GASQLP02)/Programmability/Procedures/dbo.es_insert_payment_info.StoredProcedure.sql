USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_payment_info]    Script Date: 02/14/2014 13:06:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  PROCEDURE [dbo].[es_insert_payment_info]
    
      @payment_info_id int OUTPUT
    , @group_id int
    , @event_id int
    , @ship_to_name varchar(100)
    , @phone_number_id int
    , @ssn varchar(50)
    , @postal_address_id int
    , @on_behalf_of_name nvarchar(50) 
    , @payment_name  nvarchar(50)
    , @active bit
    , @create_date datetime
AS
BEGIN

insert into payment_info (event_id
                      , group_id 
                      , ship_to_name
                      ,  phone_number_id
                       , ssn  
                       , postal_address_id
                       , on_behalf_of_name
                       , payment_name  
                       , active
                       , create_date)
values (@event_id, @group_id, @ship_to_name, @phone_number_id
                       , @ssn
                       , @postal_address_id
                       , @on_behalf_of_name
                       , @payment_name  
                       , @active
                       , @create_date)

select @payment_info_id = SCOPE_IDENTITY()

END
GO
