USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_newsletter]    Script Date: 02/14/2014 13:07:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Newsletter
CREATE PROCEDURE [dbo].[efrcrm_insert_newsletter] @Newsletter_ID int OUTPUT, @Referrer varchar(255), @Email varchar(100), @Fullname varchar(100), @Unsubscribed bit AS
begin

insert into Newsletter(Referrer, Email, Fullname, Unsubscribed) values(@Referrer, @Email, @Fullname, @Unsubscribed)

select @Newsletter_ID = SCOPE_IDENTITY()

end
GO
