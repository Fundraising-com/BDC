USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_advertisment_type]    Script Date: 02/14/2014 13:06:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Advertisment_Type
CREATE PROCEDURE [dbo].[efrcrm_insert_advertisment_type] @Advertisment_Type_ID int OUTPUT, @Description varchar(200) AS
begin

insert into Advertisment_Type(Description) values(@Description)

select @Advertisment_Type_ID = SCOPE_IDENTITY()

end
GO
