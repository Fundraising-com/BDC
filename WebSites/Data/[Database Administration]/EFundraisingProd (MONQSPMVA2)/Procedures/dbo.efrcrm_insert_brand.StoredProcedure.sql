USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_brand]    Script Date: 02/14/2014 13:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Brand
CREATE PROCEDURE [dbo].[efrcrm_insert_brand] @Brand_ID int OUTPUT, @Name varchar(50), @Promotion varchar(255) AS
begin

insert into Brand(Name, Promotion) values(@Name, @Promotion)

select @Brand_ID = SCOPE_IDENTITY()

end
GO
