USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_confirmation_method]    Script Date: 02/14/2014 13:06:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Confirmation_Method
CREATE PROCEDURE [dbo].[efrcrm_insert_confirmation_method] @Confirmation_Method_ID int OUTPUT, @Description varchar(50) AS
begin

insert into Confirmation_Method(Description) values(@Description)

select @Confirmation_Method_ID = SCOPE_IDENTITY()

end
GO
