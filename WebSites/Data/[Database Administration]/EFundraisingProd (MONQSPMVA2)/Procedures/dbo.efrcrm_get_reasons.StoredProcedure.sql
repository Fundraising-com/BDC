USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_reasons]    Script Date: 02/14/2014 13:05:44 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Reason
CREATE PROCEDURE [dbo].[efrcrm_get_reasons] AS
begin

select Reason_ID, Description, Is_Active from Reason

end
GO
