USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_newsletter]    Script Date: 02/14/2014 13:08:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Newsletter
CREATE PROCEDURE [dbo].[efrcrm_update_newsletter] @Newsletter_ID int, @Referrer varchar(255), @Email varchar(100), @Fullname varchar(100), @Unsubscribed bit AS
begin

update Newsletter set Referrer=@Referrer, Email=@Email, Fullname=@Fullname, Unsubscribed=@Unsubscribed where Newsletter_ID=@Newsletter_ID

end
GO
