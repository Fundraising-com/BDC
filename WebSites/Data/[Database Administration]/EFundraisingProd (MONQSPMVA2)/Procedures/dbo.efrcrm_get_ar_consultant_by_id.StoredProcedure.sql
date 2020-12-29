USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_ar_consultant_by_id]    Script Date: 02/14/2014 13:03:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for AR_Consultant
CREATE PROCEDURE [dbo].[efrcrm_get_ar_consultant_by_id] @AR_Consultant_ID int AS
begin

select AR_Consultant_ID, Name, Email, Phone_Ext, Is_Active, Nt_Login from AR_Consultant where AR_Consultant_ID=@AR_Consultant_ID

end
GO
