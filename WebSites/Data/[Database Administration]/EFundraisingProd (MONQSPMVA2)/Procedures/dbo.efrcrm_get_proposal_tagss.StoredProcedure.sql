USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_get_proposal_tagss]    Script Date: 02/14/2014 13:05:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate get stored proc for Proposal_Tags
CREATE PROCEDURE [dbo].[efrcrm_get_proposal_tagss] AS
begin

select Proposal_ID, Tags_ID from Proposal_Tags

end
GO
