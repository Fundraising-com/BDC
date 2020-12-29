USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_newsletters]    Script Date: 02/14/2014 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Newsletter
CREATE PROCEDURE [dbo].[efrcrm_get_newsletters] AS
begin

select Newsletter_ID, Referrer, Email, Fullname, Unsubscribed from Newsletter

end
GO
