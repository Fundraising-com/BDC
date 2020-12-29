USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_payments_comment_by_nt_login]    Script Date: 02/14/2014 13:06:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Payment_comment
CREATE PROCEDURE [dbo].[es_get_payments_comment_by_nt_login] @nt_login varchar(256) AS
begin

select Payment_comment_id, Payment_id, Comment, Nt_login, Create_date 
from Payment_comment 
where nt_login like @nt_login
order by Create_date 

end
GO
