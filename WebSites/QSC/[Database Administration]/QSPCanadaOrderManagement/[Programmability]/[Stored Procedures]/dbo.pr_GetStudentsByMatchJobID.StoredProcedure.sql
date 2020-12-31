USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_GetStudentsByMatchJobID]    Script Date: 06/07/2017 09:20:02 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_GetStudentsByMatchJobID]

	@MatchJobID int
 AS
	
SELECT  StudentInput.Instance, TeacherInstance, StudentInput.LastName, StudentInput.FirstName,Classroom FROM 
       		MatchJob,StudentInput, Teacher
				 WHERE ID =@MatchJobID and MatchJobID = ID
		and Teacher.instance=TeacherInstance
order by StudentInput.LastName, StudentInput.FirstName
GO
