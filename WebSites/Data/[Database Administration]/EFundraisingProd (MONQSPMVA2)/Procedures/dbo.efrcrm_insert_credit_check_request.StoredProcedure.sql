USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_credit_check_request]    Script Date: 02/14/2014 13:06:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Local_Sponsor_Activity
CREATE        PROCEDURE [dbo].[efrcrm_insert_credit_check_request]
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

insert into dbo.credit_check_request values (
      
       @lead_id,
       @consultant_id,
       @request_date,
       @order_date,
       @amount_requested,
       @credit_status_id,
       @credit_score,
       @amount_approved,
       @last_name,
       @first_name,
       @mid_init,
       @address,
       @city,
       @state,
       @zip,
       @ssn,
       @result_date,
       @result_confirmation_date,
       @credit_report)

end
GO
