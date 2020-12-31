USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ToteAdvanceChecks]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToteAdvanceChecks](
	[AccountID] [int] NOT NULL,
	[ReceiveDate] [datetime] NOT NULL,
	[ToteInstance] [int] NOT NULL,
	[Booklets] [int] NOT NULL,
	[FamilyEnvelopes] [int] NOT NULL,
	[DateSent] [datetime] NOT NULL,
 CONSTRAINT [aaaaaToteAdvanceChecks_PK] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC,
	[ReceiveDate] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ToteAdvanceChecks] ADD  CONSTRAINT [DF__ToteAdvan__Accou__278EDA44]  DEFAULT (0) FOR [AccountID]
GO
ALTER TABLE [dbo].[ToteAdvanceChecks] ADD  CONSTRAINT [DF__ToteAdvan__Recei__2882FE7D]  DEFAULT ('1/1/1995') FOR [ReceiveDate]
GO
ALTER TABLE [dbo].[ToteAdvanceChecks] ADD  CONSTRAINT [DF__ToteAdvan__ToteI__297722B6]  DEFAULT (0) FOR [ToteInstance]
GO
ALTER TABLE [dbo].[ToteAdvanceChecks] ADD  CONSTRAINT [DF__ToteAdvan__Bookl__2A6B46EF]  DEFAULT (0) FOR [Booklets]
GO
ALTER TABLE [dbo].[ToteAdvanceChecks] ADD  CONSTRAINT [DF__ToteAdvan__Famil__2B5F6B28]  DEFAULT (0) FOR [FamilyEnvelopes]
GO
ALTER TABLE [dbo].[ToteAdvanceChecks] ADD  CONSTRAINT [DF__ToteAdvan__DateS__2C538F61]  DEFAULT ('1/1/1995') FOR [DateSent]
GO
