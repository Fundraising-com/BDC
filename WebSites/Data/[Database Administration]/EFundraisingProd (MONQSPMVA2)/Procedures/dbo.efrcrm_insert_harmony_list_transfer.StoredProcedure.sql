USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_harmony_list_transfer]    Script Date: 02/14/2014 13:06:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Harmony_list_transfer
CREATE PROCEDURE [dbo].[efrcrm_insert_harmony_list_transfer] @Id int OUTPUT, @List_name varchar(100), @List_desc varchar(100) AS
begin

insert into Harmony_list_transfer(List_name, List_desc) values(@List_name, @List_desc)

select @Id = SCOPE_IDENTITY()

end
GO
