USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_proposal_by_id]    Script Date: 02/14/2014 13:05:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get by id stored proc for Proposal
CREATE PROCEDURE [dbo].[efrcrm_get_proposal_by_id] @Proposal_ID int AS
begin

select Proposal_ID, Fax_Name, Email_Name, Description from Proposal where Proposal_ID=@Proposal_ID

end
GO
