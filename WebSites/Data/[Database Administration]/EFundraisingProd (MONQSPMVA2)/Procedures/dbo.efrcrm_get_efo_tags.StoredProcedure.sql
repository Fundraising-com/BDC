USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_efo_tags]    Script Date: 02/14/2014 13:04:39 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for EFO_Tag
CREATE PROCEDURE [dbo].[efrcrm_get_efo_tags] AS
begin

select Email_Type_ID, Tag_Name, Tag_ID from EFO_Tag

end
GO
