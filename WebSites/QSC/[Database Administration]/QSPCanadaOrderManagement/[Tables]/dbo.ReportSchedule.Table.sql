USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ReportSchedule]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ReportSchedule](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ReportType] [int] NOT NULL,
	[DateToRun] [datetime] NOT NULL,
	[DateRan] [datetime] NOT NULL,
	[DateRanEnd] [datetime] NOT NULL,
	[StatusInstance] [int] NOT NULL,
	[FromDateParam] [datetime] NOT NULL,
	[ToDateParam] [datetime] NOT NULL,
	[OtherParams] [varchar](200) NULL,
	[DateCreated] [datetime] NOT NULL,
	[UserIDCreated] [dbo].[UserID_UDDT] NOT NULL,
 CONSTRAINT [aaaaaReportSchedule_PK] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[ReportSchedule] ADD  CONSTRAINT [DF__ReportSch__Repor__00750D23]  DEFAULT (0) FOR [ReportType]
GO
ALTER TABLE [dbo].[ReportSchedule] ADD  CONSTRAINT [DF__ReportSch__DateT__0169315C]  DEFAULT ('1/1/1995') FOR [DateToRun]
GO
ALTER TABLE [dbo].[ReportSchedule] ADD  CONSTRAINT [DF__ReportSch__DateR__025D5595]  DEFAULT ('1/1/1995') FOR [DateRan]
GO
ALTER TABLE [dbo].[ReportSchedule] ADD  CONSTRAINT [DF__ReportSch__DateR__035179CE]  DEFAULT ('1/1/1995') FOR [DateRanEnd]
GO
ALTER TABLE [dbo].[ReportSchedule] ADD  CONSTRAINT [DF__ReportSch__Statu__04459E07]  DEFAULT (0) FOR [StatusInstance]
GO
ALTER TABLE [dbo].[ReportSchedule] ADD  CONSTRAINT [DF__ReportSch__FromD__0539C240]  DEFAULT ('1/1/1995') FOR [FromDateParam]
GO
ALTER TABLE [dbo].[ReportSchedule] ADD  CONSTRAINT [DF__ReportSch__ToDat__062DE679]  DEFAULT ('1/1/1995') FOR [ToDateParam]
GO
ALTER TABLE [dbo].[ReportSchedule] ADD  CONSTRAINT [DF__ReportSch__Other__07220AB2]  DEFAULT (null) FOR [OtherParams]
GO
ALTER TABLE [dbo].[ReportSchedule] ADD  CONSTRAINT [DF__ReportSch__DateC__08162EEB]  DEFAULT ('1/1/1995') FOR [DateCreated]
GO
ALTER TABLE [dbo].[ReportSchedule] ADD  CONSTRAINT [DF__ReportSch__UserI__090A5324]  DEFAULT (' ') FOR [UserIDCreated]
GO
