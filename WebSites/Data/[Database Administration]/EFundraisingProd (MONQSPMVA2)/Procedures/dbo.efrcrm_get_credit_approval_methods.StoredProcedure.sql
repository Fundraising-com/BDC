USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_credit_approval_methods]    Script Date: 02/14/2014 13:04:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Credit_Approval_Method
CREATE PROCEDURE [dbo].[efrcrm_get_credit_approval_methods] AS
begin

select Credit_Approval_Method_ID, Description from Credit_Approval_Method

end
GO
