USE [EFRCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[efrc_get_partner_fax]    Script Date: 02/14/2014 13:05:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[efrc_get_partner_fax] (@partner_id int, @culture_code nvarchar(5))
RETURNS  varchar(255)
AS 
BEGIN
	declare @default_partner_id int = 686 
	declare @partner_fax int = 16			--fax_number
	declare @fax_number varchar(255)			--fax_number
	

	select @fax_number = value from partner_attribute_value where partner_id = @partner_id and partner_attribute_id = @partner_fax  and culture_code = @culture_code
	if @fax_number is null
	begin
			select @fax_number = value from partner_attribute_value where partner_id = @default_partner_id and partner_attribute_id = @partner_fax  and culture_code = @culture_code
	end 	
	
    RETURN @fax_number
END
GO
