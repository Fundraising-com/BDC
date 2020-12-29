USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_ar_activity]    Script Date: 02/14/2014 13:06:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for AR_Activity
CREATE PROCEDURE [dbo].[efrcrm_insert_ar_activity] @AR_Activity_ID int OUTPUT, @AR_Activity_Type_ID int, @Sales_ID int, @AR_Consultant_ID int, @AR_Activity_Date smalldatetime, @Completed_Date smalldatetime, @Comments varchar(255) AS

declare @id int
exec @id = sp_NewID  'AR_Activity_ID', 'All'

begin

insert into AR_Activity(AR_Activity_ID, AR_Activity_Type_ID, Sales_ID, AR_Consultant_ID, AR_Activity_Date, Completed_Date, Comments) values(@id, @AR_Activity_Type_ID, @Sales_ID, @AR_Consultant_ID, @AR_Activity_Date, @Completed_Date, @Comments)

end
GO
