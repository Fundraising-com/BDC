USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_postponed_statuss]    Script Date: 02/14/2014 13:05:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Postponed_Status
CREATE PROCEDURE [dbo].[efrcrm_get_postponed_statuss] AS
begin

select Postponed_Status_ID, Description from Postponed_Status

end
GO
