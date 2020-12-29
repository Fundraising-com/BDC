USE [EFRCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[efrc_get_partner_folder]    Script Date: 02/14/2014 13:05:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[efrc_get_partner_folder] (@partner_id int, @culture_code nvarchar(5))
RETURNS  varchar(255)
AS 
BEGIN
	declare @default_partner_folder int = 686 
	declare @partner_folder int = 11			--partner path
	declare @value varchar(255)		--partner path

	select @value = value from partner_attribute_value where partner_id = @partner_id and partner_attribute_id = @partner_folder  and culture_code = @culture_code 
	if @value is null
	begin
			select  @value = value from partner_attribute_value where partner_id = @default_partner_folder and partner_attribute_id = @partner_folder  and culture_code = @culture_code
	end 
    RETURN @value
END
GO
