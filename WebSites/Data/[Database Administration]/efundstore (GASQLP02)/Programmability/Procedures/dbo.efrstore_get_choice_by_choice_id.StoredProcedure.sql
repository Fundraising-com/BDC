USE [eFundstore]
GO
/****** Object:  StoredProcedure [dbo].[efrstore_get_choice_by_choice_id]    Script Date: 02/14/2014 13:05:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[efrstore_get_choice_by_choice_id]
	-- Add the parameters for the stored procedure here
	@Choice_id int
AS
BEGIN
	

    select choice_id, choice_desc, location, image
	from choice
	where [choice_id] = @Choice_id
	
	
END
GO
