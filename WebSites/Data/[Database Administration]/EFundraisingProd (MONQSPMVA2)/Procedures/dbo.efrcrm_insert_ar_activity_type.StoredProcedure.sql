USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_ar_activity_type]    Script Date: 02/14/2014 13:06:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for AR_Activity_Type
CREATE PROCEDURE [dbo].[efrcrm_insert_ar_activity_type] @AR_Activity_Type_Id int OUTPUT, @Description varchar(50) AS
begin

insert into AR_Activity_Type(Description) values(@Description)

select @AR_Activity_Type_Id = SCOPE_IDENTITY()

end
GO
