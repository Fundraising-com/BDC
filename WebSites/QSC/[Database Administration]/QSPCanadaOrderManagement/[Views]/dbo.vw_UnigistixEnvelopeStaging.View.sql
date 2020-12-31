USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_UnigistixEnvelopeStaging]    Script Date: 06/07/2017 09:18:59 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[vw_UnigistixEnvelopeStaging] AS

	SELECT	
	       	DISTINCT Teacher.Instance AS Instance,
		cast(Teacher.Instance as varchar) + cast(cast(rand() * DATEPART(MS,getDate()) * 10000 as int) as varchar) as NewInstance,
		isnull(Teacher.MiddleInitial,'N/A') AS TeacherMiddle,
		isnull(Teacher.LastName,'N/A') AS TeacherLastName,
		isnull(Classroom,'N/A') AS Classroom,
		b.OrderID

	FROM
		QSPCanadaOrderManagement..Batch b,
		QSPCanadaOrderManagement..CustomerOrderHeader coh,
		QSPCanadaOrderManagement..Student student,
		QSPCanadaOrderManagement..Teacher Teacher
	WHERE
		b.date = coh.orderbatchdate
		and b.id = coh.orderbatchid
		and coh.studentinstance = student.instance 
		and student.teacherinstance = teacher.instance

	/*FROM	QSPCanadaOrderManagement..Batch As Batch
			left join QSPCanadaOrderManagement..Envelope As Envelope on
				envelope.orderbatchid=batch.id  
				and envelope.orderbatchdate=date  
			left join QSPCanadaOrderManagement..Teacher As Teacher on
					envelope.teacherinstance=teacher.instance */
GO
