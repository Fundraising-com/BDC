USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_service_types]    Script Date: 02/14/2014 13:06:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Service_type
CREATE PROCEDURE [dbo].[efrcrm_get_service_types] AS
begin

select Service_type_id, Description from Service_type

end
GO
