USE [eFundraisingProd]
GO
/****** Object:  StoredProcedure [dbo].[efrcrm_insert_proposal]    Script Date: 02/14/2014 13:07:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Generate insert stored proc for Proposal
CREATE PROCEDURE [dbo].[efrcrm_insert_proposal] @Proposal_ID int OUTPUT, @Fax_Name varchar(200), @Email_Name varchar(200), @Description varchar(200) AS
begin

insert into Proposal(Fax_Name, Email_Name, Description) values(@Fax_Name, @Email_Name, @Description)

select @Proposal_ID = SCOPE_IDENTITY()

end
GO
