USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_ar_activity]    Script Date: 02/14/2014 13:07:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for AR_Activity
CREATE PROCEDURE [dbo].[efrcrm_update_ar_activity] @AR_Activity_ID int, @AR_Activity_Type_ID int, @Sales_ID int, @AR_Consultant_ID int, @AR_Activity_Date smalldatetime, @Completed_Date smalldatetime, @Comments varchar(255) AS
begin

update AR_Activity set AR_Activity_Type_ID=@AR_Activity_Type_ID, Sales_ID=@Sales_ID, AR_Consultant_ID=@AR_Consultant_ID, AR_Activity_Date=@AR_Activity_Date, Completed_Date=@Completed_Date, Comments=@Comments where AR_Activity_ID=@AR_Activity_ID

end
GO
