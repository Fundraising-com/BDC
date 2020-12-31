USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_GenerateStudentMergePurgeList]    Script Date: 06/07/2017 09:19:55 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_GenerateStudentMergePurgeList]
	@campaignID int
as

	declare @matchJobID int

	Insert MatchJob (DateTime, Status, ErrorStatus,Type, AccountCampaignID) values( GetDate(), 0,0,1,@campaignID)

	select @matchJobID=Scope_Identity()

	Insert StudentInput

SELECT distinct @matchJobID, Student.Instance, Student.TeacherInstance, Student.LastName, Student.FirstName FROM 
       		Student, Teacher, QSPCanadaCommon..Campaign C, CustomerOrderHeader, Batch
			 WHERE Teacher.AccountID =C.BillToAccountID  AND Student.TeacherInstance = Teacher.Instance
				and C.ID = @campaignID and CustomerOrderheader.studentinstance=student.Instance and
				Batch.CampaignID = C.ID
				and orderbatchdate = date
				and orderbatchid = batch.id
GO
