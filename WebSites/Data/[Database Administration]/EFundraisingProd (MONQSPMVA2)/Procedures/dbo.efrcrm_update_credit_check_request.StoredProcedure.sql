USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_credit_check_request]    Script Date: 02/14/2014 13:07:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Local_Sponsor_Activity
CREATE          PROCEDURE [dbo].[efrcrm_update_credit_check_request]
                
                  @credit_check_id int,
                  @lead_id int,
                  @consultant_id int,
                  @request_date datetime,
                  @order_date datetime,
                  @amount_requested money,
                  @credit_score int,
                  @credit_status_id int,
                  @amount_approved money,
                  @last_name varchar(75),
                  @first_name varchar(75),
                  @mid_init varchar(75),
                  @address varchar(150),
                  @city varchar(50),
                  @state varchar(5),
                  @zip varchar(10),
                  @ssn varchar(20),
                  @result_date datetime,
                  @result_confirmation_date datetime,
                  @credit_report as text
                   
AS
begin

update dbo.credit_check_request set
      
       lead_id = @lead_id,
       consultant_id = @consultant_id,
       order_date = @order_date,
       request_date = @request_date,
       amount_requested = @amount_requested,
       credit_status_id = @credit_status_id,
       credit_score = @credit_score,
       amount_approved = @amount_approved,
       last_name = @last_name,
       first_name = @first_name,
       mid_init =  @mid_init,
       address =  @address,
       city =  @city,
       state =  @state,
       zip = @zip,
       ssn = @ssn,
       result_date = @result_date,
       result_confirmation_date = @result_confirmation_date,
       credit_report = @credit_report

where credit_check_id = @credit_check_id
end

select * from credit_check_request
GO
