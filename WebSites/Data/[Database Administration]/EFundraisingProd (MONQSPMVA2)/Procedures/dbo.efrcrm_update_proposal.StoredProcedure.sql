USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_update_proposal]    Script Date: 02/14/2014 13:08:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate update stored proc for Proposal
CREATE PROCEDURE [dbo].[efrcrm_update_proposal] @Proposal_ID int, @Fax_Name varchar(200), @Email_Name varchar(200), @Description varchar(200) AS
begin

update Proposal set Fax_Name=@Fax_Name, Email_Name=@Email_Name, Description=@Description where Proposal_ID=@Proposal_ID

end
GO
