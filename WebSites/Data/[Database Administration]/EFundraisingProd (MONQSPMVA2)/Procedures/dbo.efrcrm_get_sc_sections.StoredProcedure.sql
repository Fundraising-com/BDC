USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_sc_sections]    Script Date: 02/14/2014 13:06:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for SC_SECTION
CREATE PROCEDURE [dbo].[efrcrm_get_sc_sections] AS
begin

select Section_Id, Section_Title, Section_Image, Section_Text, Section_Template, Section_Sub_Title from SC_SECTION

end
GO
