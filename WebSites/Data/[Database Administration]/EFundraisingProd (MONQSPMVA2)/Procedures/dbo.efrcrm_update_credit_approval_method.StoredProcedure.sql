USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_credit_approval_method]    Script Date: 02/14/2014 13:07:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Credit_Approval_Method
CREATE PROCEDURE [dbo].[efrcrm_update_credit_approval_method] @Credit_Approval_Method_ID int, @Description char(50) AS
begin

update Credit_Approval_Method set Description=@Description where Credit_Approval_Method_ID=@Credit_Approval_Method_ID

end
GO
