USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_payment]    Script Date: 02/14/2014 13:07:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Payment
CREATE PROCEDURE [dbo].[es_update_payment]  @Payment_id int, @Payment_type_id int, @Payment_info_id int, @Payment_period_id int, @Cheque_number int, @Cheque_date datetime, @Paid_amount money, @Name varchar(100), @Phone_number varchar(50), @Address_1 varchar(100), @Address_2 varchar(100), @City varchar(100), @Zip_code varchar(10), @Country_code nvarchar(4), @Subdivision_code nvarchar(14), @Create_date datetime, @Payment_Batch_id int = NULL, @Is_validated  bit = 0,@Is_processed bit = 0  AS
begin

update Payment set Payment_type_id=@Payment_type_id, Payment_info_id=@Payment_info_id, Payment_period_id=@Payment_period_id, Cheque_number=@Cheque_number, Cheque_date=@Cheque_date, Paid_amount=@Paid_amount, Name=@Name, Phone_number=@Phone_number, Address_1=@Address_1, Address_2=@Address_2, City=@City, Zip_code=@Zip_code, Country_code=@Country_code, Subdivision_code=@Subdivision_code, Create_date=@Create_date , Payment_Batch_id = @Payment_Batch_id, Is_validated =@Is_validated, Is_processed = @Is_processed where Payment_id=@Payment_id
--
end
GO
