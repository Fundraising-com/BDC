USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_proposals]    Script Date: 02/14/2014 13:05:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Proposal
CREATE PROCEDURE [dbo].[efrcrm_get_proposals] AS
begin

select Proposal_ID, Fax_Name, Email_Name, Description from Proposal

end
GO
