USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_tagss]    Script Date: 02/14/2014 13:06:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Tags
CREATE PROCEDURE [dbo].[efrcrm_get_tagss] AS
begin

select Tags_ID, Label, Control_Name from Tags

end
GO
