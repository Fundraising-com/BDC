USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_GenerateBatchMergePurgeList]    Script Date: 06/07/2017 09:19:55 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_GenerateBatchMergePurgeList] 

	@campaignID int
AS
	exec pr_GenerateTeacherMergePurgeList  @campaignID
--	exec pr_GenerateStudentMergePurgeList @campaignID
GO
