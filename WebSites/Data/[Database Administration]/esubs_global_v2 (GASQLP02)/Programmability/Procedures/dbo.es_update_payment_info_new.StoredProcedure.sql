USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_payment_info_new]    Script Date: 02/14/2014 13:07:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_update_payment_info_new]
    
      @payment_info_id int
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

update payment_info set event_id = @event_id
                      , group_id = @group_id 
                      , ship_to_name = @ship_to_name
                      ,  phone_number_id = @phone_number_id
                       , ssn = @ssn
                       , postal_address_id = @postal_address_id
                       , on_behalf_of_name = @on_behalf_of_name
                       , payment_name = @payment_name  
                       , active = @active
                       , create_date = @create_date
where payment_info_id = @payment_info_id

END
GO
