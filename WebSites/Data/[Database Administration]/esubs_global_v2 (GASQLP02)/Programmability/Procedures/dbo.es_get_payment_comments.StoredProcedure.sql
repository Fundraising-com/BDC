USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payment_comments]    Script Date: 02/14/2014 13:06:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Payment_comment
CREATE PROCEDURE [dbo].[es_get_payment_comments] AS
begin

select Payment_comment_id, Payment_id, Comment, Nt_login, Create_date from Payment_comment

end
GO
