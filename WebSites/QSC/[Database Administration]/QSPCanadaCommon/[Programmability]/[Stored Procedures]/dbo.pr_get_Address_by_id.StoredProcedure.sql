USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_get_Address_by_id]    Script Date: 06/07/2017 09:33:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_get_Address_by_id]
	@address_id int
AS

SELECT 
	*
FROM 
	Address
WHERE 
	address_id = @address_id
GO
