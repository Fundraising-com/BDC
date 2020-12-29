USE [EFRCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[efrc_get_partner_name]    Script Date: 02/14/2014 13:05:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[efrc_get_partner_name] (@partner_id int, @culture_code nvarchar(5))
RETURNS  varchar(255)
AS 
BEGIN
	declare @partner_name_id  int = 686			--phone_number
	declare @partner_name  varchar(255)		--phone_number

	
	select @partner_name = value from partner_attribute_value where partner_id = @partner_id and partner_attribute_id = @partner_name_id and culture_code = @culture_code
	if @partner_name is null
	begin
			select @partner_name = partner_name from partner where partner_id = @partner_id 
	end 
	
    RETURN @partner_name
END
GO
