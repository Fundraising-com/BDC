USE [EFRCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[efrc_get_partner_phone]    Script Date: 02/14/2014 13:05:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[efrc_get_partner_phone] (@partner_id int, @culture_code nvarchar(5))
RETURNS  varchar(255)
AS 
BEGIN
	declare @default_partner_id int = 686 
	declare @partner_phone  int = 9			--phone_number
	declare @phone_number  varchar(255)		--phone_number

	
	select @phone_number = value from partner_attribute_value where partner_id = @partner_id and partner_attribute_id = @partner_phone  and culture_code = @culture_code
	if @phone_number is null
	begin
			select @phone_number = value from partner_attribute_value where partner_id = @default_partner_id and partner_attribute_id = @partner_phone  and culture_code = @culture_code
	end 
	
    RETURN @phone_number
END
GO
