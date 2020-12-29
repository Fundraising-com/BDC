USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_waitstatss]    Script Date: 02/14/2014 13:06:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Waitstats
CREATE PROCEDURE [dbo].[efrcrm_get_waitstatss] AS
begin

select [Wait type], [Requests], [Wait time], [Signal wait time], Now from Waitstats

end
GO
