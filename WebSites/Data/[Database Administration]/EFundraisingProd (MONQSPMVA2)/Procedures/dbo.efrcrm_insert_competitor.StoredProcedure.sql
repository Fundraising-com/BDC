USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_competitor]    Script Date: 02/14/2014 13:06:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Competitor
CREATE PROCEDURE [dbo].[efrcrm_insert_competitor] @Competitor_ID int OUTPUT, @Business_Name varchar(100), @Comments varchar(255) AS
begin

insert into Competitor(Business_Name, Comments) values(@Business_Name, @Comments)

select @Competitor_ID = SCOPE_IDENTITY()

end
GO
