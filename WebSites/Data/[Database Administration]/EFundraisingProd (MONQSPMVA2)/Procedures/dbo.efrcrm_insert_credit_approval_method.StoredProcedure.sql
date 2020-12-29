USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_credit_approval_method]    Script Date: 02/14/2014 13:06:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Credit_Approval_Method
CREATE PROCEDURE [dbo].[efrcrm_insert_credit_approval_method] @Credit_Approval_Method_ID int OUTPUT, @Description char(50) AS
begin

insert into Credit_Approval_Method(Description) values(@Description)

select @Credit_Approval_Method_ID = SCOPE_IDENTITY()

end
GO
