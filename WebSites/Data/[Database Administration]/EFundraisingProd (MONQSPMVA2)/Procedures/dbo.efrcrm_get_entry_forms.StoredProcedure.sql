USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_entry_forms]    Script Date: 02/14/2014 13:04:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Entry_Form
CREATE PROCEDURE [dbo].[efrcrm_get_entry_forms] AS
begin

select Entry_Form_ID, Entry_Form_Desc, Master_Template, Content_Template, Web_Site_ID from Entry_Form

end
GO
