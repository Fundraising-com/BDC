USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_kazoomster_leagues_050103$s]    Script Date: 02/14/2014 13:04:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Kazoomster_leagues_050103$
CREATE PROCEDURE [dbo].[efrcrm_get_kazoomster_leagues_050103$s] AS
begin

select GoodEmail from Kazoomster_leagues_050103$

end
GO
