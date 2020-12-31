USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[CreateStudent]    Script Date: 06/07/2017 09:19:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[CreateStudent]
	@teacherinstance int,
	@first varchar(50),
	@last varchar(50),
	@instance int OUTPUT,
	@UserIDCreated varchar(4) = 'ADMI'
as
	

	create table #temp
	(
		 NextInstance int
	)


	insert into #temp exec qspcanadaordermanagement..InsertNextInstance 2
	select @instance=nextinstance from #temp
	truncate table #temp

	insert Student ( Instance,
			TeacherInstance,
			LastName,
			FirstName,
			DateCreated,
			UserIDCreated,
			DateChanged,
			UserIDChanged
			)

	values ( @instance, @teacherinstance,@last, @first,
			GetDate(),@UserIDCreated,NULL,NULL)
	

	drop table #temp
GO
