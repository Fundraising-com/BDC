USE [esubs_global_v2]
GO
/****** Object:  StoredProcedure [dbo].[es_get_invitation_member_hierarchy]    Script Date: 02/14/2014 13:05:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO
/*
	Created by: Philippe Girard
	Project: eSubs v2
	Date: 4 August 2005
	
	Description: return member_hierarchy for an invitation
*/
CREATE PROC [dbo].[es_get_invitation_member_hierarchy]
	@invitation_id int
	, @member_hierarchy_id int OUTPUT
AS
BEGIN
	SELECT @member_hierarchy_id = mh.member_hierarchy_id
	FROM member_hierarchy as mh
		inner join event_participation as ep
			on ep.member_hierarchy_id = mh.member_hierarchy_id
		inner join invitation as i
			on i.event_participation_id = ep.event_participation_id
	WHERE i.invitation_id = @invitation_id
	
	IF (@@error <> 0)
	BEGIN
  		ROLLBACK TRANSACTION
		RETURN -1
	END

	RETURN 0
END
GO
