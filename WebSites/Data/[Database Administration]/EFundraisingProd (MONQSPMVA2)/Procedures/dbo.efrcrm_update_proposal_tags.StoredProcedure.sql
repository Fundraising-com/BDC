USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_proposal_tags]    Script Date: 02/14/2014 13:08:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Proposal_Tags
CREATE PROCEDURE [dbo].[efrcrm_update_proposal_tags] @Proposal_ID int, @Tags_ID int AS
begin

update Proposal_Tags set Tags_ID=@Tags_ID where Proposal_ID=@Proposal_ID

end
GO
