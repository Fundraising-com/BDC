USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_collection_statuss]    Script Date: 02/14/2014 13:04:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Collection_Status
CREATE PROCEDURE [dbo].[efrcrm_get_collection_statuss] AS
begin

select Collection_Status_ID, Description from Collection_Status

end
GO
