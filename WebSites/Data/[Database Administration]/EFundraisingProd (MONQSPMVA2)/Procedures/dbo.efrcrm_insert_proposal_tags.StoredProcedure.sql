USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_proposal_tags]    Script Date: 02/14/2014 13:07:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Proposal_Tags
CREATE PROCEDURE [dbo].[efrcrm_insert_proposal_tags] @Proposal_ID int OUTPUT, @Tags_ID int AS
begin

insert into Proposal_Tags(Tags_ID) values(@Tags_ID)

select @Proposal_ID = SCOPE_IDENTITY()

end
GO
