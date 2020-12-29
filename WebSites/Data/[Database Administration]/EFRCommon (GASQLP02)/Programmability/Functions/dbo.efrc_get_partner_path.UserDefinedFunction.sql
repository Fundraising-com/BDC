USE [EFRCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[efrc_get_partner_path]    Script Date: 02/14/2014 13:05:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--COLLATE Latin1_General_CI_AS
--select * from partner_attribute

CREATE FUNCTION [dbo].[efrc_get_partner_path] (@partner_id int, @culture_code nvarchar(5))
RETURNS  varchar(255)
AS 
BEGIN
	declare @default_partner_path int = 686 
	declare @partner_path int = 1			--partner path
	declare @value varchar(255)		--partner path

	select @value = value from partner_attribute_value where partner_id = @partner_id and partner_attribute_id = @partner_path  and culture_code = @culture_code 
	if @value is null
	begin
			select  @value = value from partner_attribute_value where partner_id = @default_partner_path and partner_attribute_id = @partner_path  and culture_code = @culture_code
	end 
    RETURN @value
END
GO
