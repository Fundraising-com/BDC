USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_commentss]    Script Date: 02/14/2014 13:04:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Comments
CREATE PROCEDURE [dbo].[efrcrm_get_commentss] AS
begin

select Comments_ID, Priority_ID, Sales_ID, Consultant_ID, Lead_ID, Department_ID, Entry_Date, Comments from Comments

end
GO
