USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_kit_type]    Script Date: 02/14/2014 13:06:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Kit_Type
CREATE PROCEDURE [dbo].[efrcrm_insert_kit_type] @Kit_Type_ID int OUTPUT, @Description varchar(50), @Delivery_Time datetime, @Comments text, @Is_Default bit AS
begin

insert into Kit_Type(Description, Delivery_Time, Comments, Is_Default) values(@Description, @Delivery_Time, @Comments, @Is_Default)

select @Kit_Type_ID = SCOPE_IDENTITY()

end
GO
