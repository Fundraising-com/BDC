USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_harmony_list_transfer_by_id]    Script Date: 02/14/2014 13:04:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Harmony_list_transfer
CREATE PROCEDURE [dbo].[efrcrm_get_harmony_list_transfer_by_id] @Id int AS
begin

select Id, List_name, List_desc from Harmony_list_transfer where Id=@Id

end
GO
