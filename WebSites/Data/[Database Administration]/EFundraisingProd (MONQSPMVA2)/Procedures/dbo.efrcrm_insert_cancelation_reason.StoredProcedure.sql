USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_cancelation_reason]    Script Date: 02/14/2014 13:06:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Cancelation_Reason
CREATE PROCEDURE [dbo].[efrcrm_insert_cancelation_reason] @Cancelation_Reason_Id int OUTPUT, @Description varchar(100) AS
begin

insert into Cancelation_Reason(Description) values(@Description)

select @Cancelation_Reason_Id = SCOPE_IDENTITY()

end
GO
