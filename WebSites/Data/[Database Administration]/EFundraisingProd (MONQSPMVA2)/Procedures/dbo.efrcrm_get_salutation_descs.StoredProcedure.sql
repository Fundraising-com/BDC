USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_salutation_descs]    Script Date: 02/14/2014 13:06:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Salutation_desc
CREATE PROCEDURE [dbo].[efrcrm_get_salutation_descs] AS
begin

select Salutation_id, Language_id, Description from Salutation_desc

end
GO
