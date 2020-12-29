USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_credit_card_refund_request]    Script Date: 02/14/2014 13:06:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--select * from dbo.credit_card_refund_request
-- Generate update stored proc for Local_Sponsor_Activity
CREATE      PROCEDURE [dbo].[efrcrm_insert_credit_card_refund_request] --69747,111,null,10,''
                  @sale_id int,
                  @bpps_id int,
                  @request_date datetime = null,
                  @refund_amount money,
                  @status_code varchar(10),
                  @credit_card_type_id int
                   
AS
begin

if @request_date is null
   set @request_date = getdate()

insert into dbo.credit_card_refund_request 
(sale_id, bpps_id, request_date, status_code, refund_amount, credit_card_type_id,cancelled)
values (
      
       @sale_id,
       @bpps_id,
       @request_date,
       @status_code,
       @refund_amount,
       @credit_card_type_id,0)

end

--select * from credit_card_types
GO
