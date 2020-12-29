USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_harmony_list_transfer]    Script Date: 02/14/2014 13:08:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Harmony_list_transfer
CREATE PROCEDURE [dbo].[efrcrm_update_harmony_list_transfer] @Id int, @List_name varchar(100), @List_desc varchar(100) AS
begin

update Harmony_list_transfer set List_name=@List_name, List_desc=@List_desc where Id=@Id

end
GO
