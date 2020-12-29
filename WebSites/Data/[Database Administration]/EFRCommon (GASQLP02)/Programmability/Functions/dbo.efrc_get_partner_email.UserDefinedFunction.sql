USE [EFRCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[efrc_get_partner_email]    Script Date: 02/14/2014 13:05:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[efrc_get_partner_email] (@partner_id int, @culture_code nvarchar(5))
RETURNS  varchar(255)
AS 
BEGIN
	declare @default_partner_id int = 686 
	declare @partner_email int = 13			--email_address
	declare @email_address varchar(255)			--email_address

	
	select @email_address = value from partner_attribute_value where partner_id = @partner_id and partner_attribute_id = @partner_email  and culture_code = @culture_code
	if @email_address is null
	begin
			select @email_address = value from partner_attribute_value where partner_id = @default_partner_id and partner_attribute_id = @partner_email  and culture_code = @culture_code
	end 

	
    RETURN @email_address
END
GO
