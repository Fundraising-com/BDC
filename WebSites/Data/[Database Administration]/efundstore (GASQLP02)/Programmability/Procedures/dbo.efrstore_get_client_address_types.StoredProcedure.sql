USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_client_address_types]    Script Date: 02/14/2014 13:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Client_address_type
CREATE PROCEDURE [dbo].[efrstore_get_client_address_types] AS
begin

select Client_address_type_id, Description from Client_address_type

end
GO
