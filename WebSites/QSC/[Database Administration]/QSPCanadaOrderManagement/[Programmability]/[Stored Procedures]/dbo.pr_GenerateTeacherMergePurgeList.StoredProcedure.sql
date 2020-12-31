USE [QSPCanadaOrderManagement]
GO
/****** Object:  StoredProcedure [dbo].[pr_GenerateTeacherMergePurgeList]    Script Date: 06/07/2017 09:19:56 ******/
SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[pr_GenerateTeacherMergePurgeList]
	@campaignID int
as
	declare @matchJobID int
	declare @inputCount int

	Insert MatchJob (DateTime, Status, ErrorStatus,Type, AccountCampaignID) values( GetDate(), 0,0,0, @campaignID)

	select @matchJobID=Scope_Identity()

	Insert TeacherInput
	select distinct @matchJobID, C.ID, Teacher.Instance, dbo.[UDF_RemoveTitle](Name),Classroom from Teacher,QSPCanadaCommon..Campaign c, Student s, CustomerOrderHeader coh , Batch B where
		c.id=	@campaignID and Teacher.AccountID = BilltoAccountid
		and Teacher.Instance=s.Teacherinstance
		and B.Date = OrderBatchDate
		and B.ID = orderbatchid
		and b.CampaignID = @campaignID
		and coh.studentinstance=s.instance

	select @inputCount = @@rowcount

	-- flip match job off if only one teacher - stupid software crashed otherwise
	if(@inputCount <=1)
	begin
		update MatchJob set status= 999 where ID = @matchJobID

	end

	-- Turn off matching if there aren't any internet orders Merge Purge throws and exception
	Select @inputCount=count(*) from batch  where orderqualifierid=39009 and CampaignID = @campaignID
	if(@inputCount =0 )
	begin
		update MatchJob set status= 999 where ID = @matchJobID

	end
GO
