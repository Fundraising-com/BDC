USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[CreateTeacher]    Script Date: 06/07/2017 09:19:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[CreateTeacher]
	@accountid int,
	@classroom varchar(50),
	@title varchar(10),
	@first varchar(50),
	@middle varchar(10),
	@last varchar(50),
	@instance int OUTPUT,
	@UserIDCreated varchar(4) = 'ADMI'

as
	declare @fullname varchar(200)

	if(len(rtrim(@first)) <> 0)
	begin
		select   @fullname= ltrim(@first) + ' ' +ltrim(@last)
	end
	else
	begin
		select   @fullname=ltrim(@last)
	

	end

	create table #temp
	(
		 NextInstance int
	)


	insert into #temp exec qspcanadaordermanagement..InsertNextInstance 1
	select @instance=nextinstance from #temp
	truncate table #temp

	insert teacher ( Instance,
		AccountID,
		Name,
		Classroom,
		DateCreated,
		UserIDCreated,
		DateChanged,
		UserIDChanged,
		Title,
		FirstName,
		MiddleInitial,
		LastName)

	values ( @instance, @accountid,@fullname, 
			@classroom,GetDate(),@UserIDCreated,NULL,NULL,
		rTrim(ltrim(@title)), RTrim(ltrim(@first)), RTrim(ltrim(@middle)), rtrim(ltrim(@last)))
	
if (@@error > 0) 
begin
print @accountid
print @last 
print @classroom

end
	drop table #temp
GO
