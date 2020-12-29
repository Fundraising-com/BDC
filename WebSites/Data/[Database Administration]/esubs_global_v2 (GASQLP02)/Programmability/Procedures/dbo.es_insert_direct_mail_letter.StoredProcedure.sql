USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_insert_direct_mail_letter]    Script Date: 02/14/2014 13:06:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_insert_direct_mail_letter]
	@direct_mail_letter_id int out
	, @direct_mail_id int
	, @letter_bar_code_1 varchar(256)
	, @letter_bar_code_2 varchar(256)
	, @letter_type int
	, @create_date datetime
AS
BEGIN

	begin transaction;

	insert into direct_mail_letter (
		direct_mail_id
		, letter_bar_code_1
		, letter_bar_code_2
		, letter_type
		, create_date
	)values(
		@direct_mail_id 
		, @letter_bar_code_1 
		, @letter_bar_code_2 
		, @letter_type 
		, @create_date 
	);
	
	select @direct_mail_letter_id = SCOPE_IDENTITY();

	commit transaction;
END
GO
