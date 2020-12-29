USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_reason]    Script Date: 02/14/2014 13:07:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Reason
CREATE PROCEDURE [dbo].[efrcrm_insert_reason] @Reason_ID int OUTPUT, @Description varchar(50), @Is_Active bit AS
begin

insert into Reason(Description, Is_Active) values(@Description, @Is_Active)

select @Reason_ID = SCOPE_IDENTITY()

end
GO
