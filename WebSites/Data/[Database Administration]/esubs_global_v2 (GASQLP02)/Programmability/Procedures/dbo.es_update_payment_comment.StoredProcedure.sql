USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_payment_comment]    Script Date: 02/14/2014 13:07:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Payment_comment
CREATE PROCEDURE [dbo].[es_update_payment_comment] @Payment_comment_id int, @Payment_id int, @Comment varchar(4000), @Nt_login varchar(256), @Create_date datetime AS
begin

update Payment_comment set Payment_id=@Payment_id, Comment=@Comment, Nt_login=@Nt_login, Create_date=@Create_date where Payment_comment_id=@Payment_comment_id

end
GO
