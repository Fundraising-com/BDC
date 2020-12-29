USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_update_direct_mail_letter]    Script Date: 02/14/2014 13:07:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_update_direct_mail_letter]
	@direct_mail_letter_id int
	, @direct_mail_id int
	, @letter_bar_code_1 varchar(256)
	, @letter_bar_code_2 varchar(256)
	, @letter_type int
	, @create_date datetime
AS
BEGIN

	update direct_mail_letter 
	set 
		direct_mail_id = @direct_mail_id
		, letter_bar_code_1 = @letter_bar_code_1
		, letter_bar_code_2 = @letter_bar_code_2
		, letter_type = @letter_type
		, create_date = @create_date
	where direct_mail_letter_id = @direct_mail_letter_id;

END
GO
