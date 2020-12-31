USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_AddressList]    Script Date: 06/07/2017 09:33:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_AddressList]
  @ListId int
AS


SELECT 
	address_id, 
	address_type,
	street1,
	street2,
	city,
	stateProvince,
	postal_code,
	zip4,
	country
  FROM   
	Address
 WHERE
	AddressListID = @ListId
ORDER BY
	case address_type
		when 54001 then 1--Ship To
		when 54002 then 2 --BillTo
		when 54000 then 3 --Undefined
		else address_type + 100 --Then everything else
	end ASC
GO
