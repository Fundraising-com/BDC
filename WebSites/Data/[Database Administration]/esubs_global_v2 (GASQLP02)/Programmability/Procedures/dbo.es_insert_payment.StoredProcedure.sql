USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_payment]    Script Date: 02/14/2014 13:06:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[es_insert_payment] 
@Payment_id INT OUTPUT, 
@Payment_type_id INT, 
@Payment_info_id INT, 
@Payment_period_id INT, 
@Cheque_number INT, 
@Cheque_date DATETIME, 
@Paid_amount MONEY, 
@Name VARCHAR(100), 
@Phone_number VARCHAR(50), 
@Address_1 VARCHAR(100), 
@Address_2 VARCHAR(100), 
@City varchar(100), 
@Zip_code varchar(10), 
@Country_code NVARCHAR(4), 
@Subdivision_code NVARCHAR(14), 
@Create_date DATETIME,
@Payment_batch_id INT = NULL,
@Is_validated bit = 0,
@Is_processed bit = 0
 AS
BEGIN

	INSERT INTO Payment(Payment_type_id, 
		Payment_info_id, 
		Payment_period_id, 
		Cheque_number, 
		Cheque_date, 
		Paid_amount, 
		Name, 
		Phone_number, 
		Address_1, 
		Address_2, 
		City, 
		Zip_code, 
		Country_code, 
		Subdivision_code, 
		Create_date,
		Payment_batch_id,
		Is_validated,
		Is_processed
) 
	VALUES(@Payment_type_id, 
		@Payment_info_id, 
		@Payment_period_id, 
		@Cheque_number, 
		@Cheque_date, 
		@Paid_amount, 
		@Name, 
		@Phone_number, 
		@Address_1, 
		@Address_2, 
		@City, 
		@Zip_code, 
		@Country_code, 
		@Subdivision_code, 
		@Create_date,
		@Payment_batch_id,
		@Is_validated,
		@Is_processed)

	SELECT @Payment_id = SCOPE_IDENTITY()

    update payment set cheque_number = @payment_id WHERE payment_id = @payment_id

END
GO
