USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_email_iwon1s]    Script Date: 02/14/2014 13:04:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for EMail_iwon1
CREATE PROCEDURE [dbo].[efrcrm_get_email_iwon1s] AS
begin

select ID, GoodEmail from EMail_iwon1

end
GO
