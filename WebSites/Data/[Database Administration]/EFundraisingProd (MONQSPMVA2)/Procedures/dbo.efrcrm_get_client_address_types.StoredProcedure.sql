USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_client_address_types]    Script Date: 02/14/2014 13:04:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Client_address_type
CREATE PROCEDURE [dbo].[efrcrm_get_client_address_types] AS
begin

select Address_type, Address_type_desc from Client_address_type

end
GO
