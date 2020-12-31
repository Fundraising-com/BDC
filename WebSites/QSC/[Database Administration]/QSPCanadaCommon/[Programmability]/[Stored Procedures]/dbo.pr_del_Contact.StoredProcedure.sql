USE [QSPCanadaCommon]
GO
/****** Object:  StoredProcedure [dbo].[pr_del_Contact]    Script Date: 06/07/2017 09:33:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[pr_del_Contact]
	@ContactID int
AS

UPDATE Contact
   SET Contact.[DeletedTF] = 1
 WHERE Contact.[Id] = @ContactID
GO
