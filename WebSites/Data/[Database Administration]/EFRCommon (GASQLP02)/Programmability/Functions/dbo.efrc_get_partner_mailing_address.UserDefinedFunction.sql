USE [EFRCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[efrc_get_partner_mailing_address]    Script Date: 02/14/2014 13:05:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[efrc_get_partner_mailing_address] (@partner_id int, @culture_code nvarchar(5))
RETURNS  varchar(255)
AS 
BEGIN
	declare @default_partner_id int = 686 
	declare @partner_mailing_address int = 17 --mailing_address	
	declare @mailing_address varchar(255)--mailing_address	

	select @mailing_address = value from partner_attribute_value where partner_id = @partner_id and partner_attribute_id = @partner_mailing_address  and culture_code = @culture_code
	if @mailing_address is null
	begin
			select @mailing_address = value from partner_attribute_value where partner_id = @default_partner_id and partner_attribute_id = @partner_mailing_address  and culture_code = @culture_code
	end 
    RETURN @mailing_address
END
GO
