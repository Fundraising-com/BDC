USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_direct_mail_letters]    Script Date: 02/14/2014 13:05:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[es_get_direct_mail_letters]
AS
BEGIN
	select direct_mail_letter_id
		, direct_mail_id
		, letter_bar_code_1
		, letter_bar_code_2
		, letter_type
		, create_date
	from direct_mail_letter;
END
GO
