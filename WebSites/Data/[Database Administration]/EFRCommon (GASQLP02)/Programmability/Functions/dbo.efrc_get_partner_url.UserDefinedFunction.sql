USE [EFRCommon]
GO
/****** Object:  UserDefinedFunction [dbo].[efrc_get_partner_url]    Script Date: 02/14/2014 13:05:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[efrc_get_partner_url] (@partner_id int, @culture_code nvarchar(5))
RETURNS  varchar(255)
AS 
BEGIN
	declare @default_partner_id int = 686 
	declare @partner_url int = 15			--url
	declare @url varchar(255)		--url

	select @url = value from partner_attribute_value where partner_id = @partner_id and partner_attribute_id = @partner_url and culture_code = @culture_code 
	if @url is null
	begin
			select  @url = value from partner_attribute_value where partner_id = @default_partner_id and partner_attribute_id = @partner_url  and culture_code = @culture_code
	end 
    RETURN @url
END
GO
