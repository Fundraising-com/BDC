USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_GetMatchJobsToRun]    Script Date: 06/07/2017 09:20:01 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[pr_GetMatchJobsToRun] 
	@status int
AS
	--select * from MatchJob where Status=@status
	select * from MatchJob where  status=@status
	
	--create table ##Test (d datetime)
	--insert into ##Test values (GETDATE())
	--select * from ##Test order by d desc
GO
