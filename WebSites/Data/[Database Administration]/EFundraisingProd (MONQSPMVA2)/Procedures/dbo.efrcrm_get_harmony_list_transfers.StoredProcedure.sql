USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_harmony_list_transfers]    Script Date: 02/14/2014 13:04:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Harmony_list_transfer
CREATE PROCEDURE [dbo].[efrcrm_get_harmony_list_transfers] AS
begin

select Id, List_name, List_desc from Harmony_list_transfer

end
GO
