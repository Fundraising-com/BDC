USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_advertising_support_type]    Script Date: 02/14/2014 13:06:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Advertising_Support_Type
CREATE PROCEDURE [dbo].[efrcrm_insert_advertising_support_type] @Advertising_Support_Type_ID int OUTPUT, @Description varchar(50), @Comments varchar(255) AS
begin

insert into Advertising_Support_Type(Description, Comments) values(@Description, @Comments)

select @Advertising_Support_Type_ID = SCOPE_IDENTITY()

end
GO
