USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_postponed_sale]    Script Date: 02/14/2014 13:07:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Postponed_Sale
CREATE PROCEDURE [dbo].[efrcrm_insert_postponed_sale] @Sales_ID int OUTPUT, @Postponed_Status_ID int, @Comments varchar(255) AS
begin

insert into Postponed_Sale(Postponed_Status_ID, Comments) values(@Postponed_Status_ID, @Comments)

select @Sales_ID = SCOPE_IDENTITY()

end
GO
