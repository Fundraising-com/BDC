USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_ar_consultants]    Script Date: 02/14/2014 13:03:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for AR_Consultant
CREATE PROCEDURE [dbo].[efrcrm_get_ar_consultants] AS
begin

select AR_Consultant_ID, Name, Email, Phone_Ext, Is_Active, Nt_Login from AR_Consultant

end
GO
