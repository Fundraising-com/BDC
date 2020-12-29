USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_payment_comment]    Script Date: 02/14/2014 13:06:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Payment_comment
CREATE PROCEDURE [dbo].[es_insert_payment_comment] @Payment_comment_id int OUTPUT, @Payment_id int, @Comment varchar(4000), @Nt_login varchar(256), @Create_date datetime AS
begin

insert into Payment_comment(Payment_id, Comment, Nt_login, Create_date) values(@Payment_id, @Comment, @Nt_login, @Create_date)

select @Payment_comment_id = SCOPE_IDENTITY()

end
GO
