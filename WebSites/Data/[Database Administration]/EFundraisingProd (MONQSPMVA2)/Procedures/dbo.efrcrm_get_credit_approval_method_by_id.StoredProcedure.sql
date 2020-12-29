USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_credit_approval_method_by_id]    Script Date: 02/14/2014 13:04:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Credit_Approval_Method
CREATE PROCEDURE [dbo].[efrcrm_get_credit_approval_method_by_id] @Credit_Approval_Method_ID int AS
begin

select Credit_Approval_Method_ID, Description from Credit_Approval_Method where Credit_Approval_Method_ID=@Credit_Approval_Method_ID

end
GO
