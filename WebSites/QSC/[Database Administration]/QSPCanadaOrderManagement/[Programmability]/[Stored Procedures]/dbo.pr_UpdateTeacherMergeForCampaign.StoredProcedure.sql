USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdateTeacherMergeForCampaign]    Script Date: 06/07/2017 09:20:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE   procedure [dbo].[pr_UpdateTeacherMergeForCampaign]
	@matchJobID int
as

	-- Take all the kids from the subordinate teacher and flip over 
	-- to master teacher

	update Student Set TeacherInstance = MasterTeacherInputInstance 
--select *
		 from Student, TeacherMatch where TeacherInstance = SubordinateTeacherInputInstance 
			and Score >= 87 and MatchJobID=@matchJobID

	
	Update MatchJob Set Status=1 where ID=@matchJobID

--exec pr_GenerateStudentMergePurgeList @campaignID
GO
