USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_UpdateStudentMergeForCampaign]    Script Date: 06/07/2017 09:20:42 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE      PROCEDURE [dbo].[pr_UpdateStudentMergeForCampaign]

	@matchJobID int
AS
/*
-- Flip the COH for students
	Update CustomerOrderHeader set StudentInstance = MasterStudentInputInstance
--select *
		from CustomerOrderHeader,studentmatch
			where  CustomerOrderHeader.StudentInstance = SubordinateStudentInputInstance
				and MatchJobID = @matchJobID
				and Score >= 87
				and MasterStudentInputInstance <> 1157289


*/

declare @minOrderid int
declare @maxOrderid int


declare @batchStatus int
declare @distCenter int
declare @RetVal int
declare @msg1 varchar(4000)

--select @matchjobid=192

create table #matcht
(
	id int,
	matchjobid int,
	MasterStudentInputInstance int,
	SubordinateStudentInputInstance int,
	Score int
)
--select @minOrderid = Long1Value, @maxOrderid=Long2Value from SystemOptions where  KeyValue='OrderIDRange'

--subordinate students in the landed order
--no matter what those should win
-- flip the MasterStudentInputInstance and the SubordinateStudentInputInstance
insert #matcht
select * from studentmatch  where matchjobid=@matchJobID
and SubordinateStudentInputInstance in
(
	select student.instance from customerorderheader,batch,student,matchjob where 
	 orderbatchdate=date and orderbatchid=batch.id
	and student.instance=studentinstance
	--and orderid between @minOrderid and @maxOrderid
	and OrderQualifierID in (39001, 39002)
	and batch.campaignid = matchjob.accountcampaignid
	and matchjob.id=@matchJobID
)
and MasterStudentInputInstance not in
(
	select student.instance from customerorderheader,batch,student,matchjob where 
	 orderbatchdate=date and orderbatchid=batch.id
	and student.instance=studentinstance
	--and orderid between @minOrderid and @maxOrderid
	and OrderQualifierID in (39001, 39002)
	and batch.campaignid = matchjob.accountcampaignid
	and matchjob.id=@matchJobID
)

update studentmatch set MasterStudentInputInstance = #matcht.SubordinateStudentInputInstance 
from 
	studentmatch, #matcht where StudentMatch.ID = #matcht.id
							/*where studentmatch.MasterStudentInputInstance= #matcht.MasterStudentInputInstance 
							and StudentMatch.SubordinateStudentInputInstance = #matcht.SubordinateStudentInputInstance*/


update studentmatch set SubordinateStudentInputInstance= #matcht.MasterStudentInputInstance   
 from 
	studentmatch, #matcht where StudentMatch.ID = #matcht.id
							/*where studentmatch.SubordinateStudentInputInstance= #matcht.SubordinateStudentInputInstance
							and StudentMatch.MasterStudentInputInstance = #matcht.MasterStudentInputInstance*/

Update CustomerOrderHeader set StudentInstance = MasterStudentInputInstance
		from CustomerOrderHeader,studentmatch
			where  CustomerOrderHeader.StudentInstance = SubordinateStudentInputInstance
				and MatchJobID = @matchJobID
				and Score >= 87
				and MasterStudentInputInstance <> 1157289

drop table #matcht
GO
