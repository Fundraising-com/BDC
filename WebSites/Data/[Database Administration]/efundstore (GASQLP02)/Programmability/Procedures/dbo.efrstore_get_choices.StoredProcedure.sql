USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_choices]    Script Date: 02/14/2014 13:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[efrstore_get_choices]
	
AS
BEGIN
	select choice_id, choice_desc, location, image
	from choice
END
GO
