USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_efo_group_type]    Script Date: 02/14/2014 13:06:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for EFO_Group_Type
CREATE PROCEDURE [dbo].[efrcrm_insert_efo_group_type] @Group_Type_ID int OUTPUT, @Description varchar(50) AS
begin

insert into EFO_Group_Type(Description) values(@Description)

select @Group_Type_ID = SCOPE_IDENTITY()

end
GO
