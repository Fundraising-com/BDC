USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_production_statuss]    Script Date: 02/14/2014 13:05:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Production_Status
CREATE PROCEDURE [dbo].[efrcrm_get_production_statuss] AS
begin

select Production_Status_ID, Description from Production_Status

end
GO
