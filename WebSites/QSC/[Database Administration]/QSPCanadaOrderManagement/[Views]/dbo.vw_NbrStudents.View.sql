USE [QSPCanadaOrderManagement]
GO
/****** Object:  View [dbo].[vw_NbrStudents]    Script Date: 06/07/2017 09:18:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[vw_NbrStudents] as
select  b.campaignid, count(distinct coh.studentinstance) as nbrstudents
	from CustomerOrderHeader coh, batch b
	where coh.orderbatchid = b.id
	and coh.orderbatchdate = b.date
group by b.campaignid
GO
