USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_party_types]    Script Date: 02/14/2014 13:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Party_type
CREATE PROCEDURE [dbo].[efrcrm_get_party_types] AS
begin

select Party_type_id, Party_type_desc from Party_type

end
GO
