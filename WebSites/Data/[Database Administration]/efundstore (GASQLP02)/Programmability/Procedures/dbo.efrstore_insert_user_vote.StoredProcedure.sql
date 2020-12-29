USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_insert_user_vote]    Script Date: 02/14/2014 13:06:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[efrstore_insert_user_vote]
	
	@Session_id varchar(20),
	@Choice_id int,
	@Survey_id int
AS
BEGIN
	insert into user_vote(session_id, choice_id, survey_id) values(@Session_id, @Choice_id, @Survey_id)

END
GO
