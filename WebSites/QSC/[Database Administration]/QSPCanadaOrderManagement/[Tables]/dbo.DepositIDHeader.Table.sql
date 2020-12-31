USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[DepositIDHeader]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DepositIDHeader](
	[DepositDate] [datetime] NOT NULL,
	[DepositID] [int] NOT NULL,
	[CalculatedAmount] [float] NOT NULL,
	[NextDetailTransID] [int] NOT NULL,
	[DateCreated] [datetime] NOT NULL,
	[UserIDCreated] [varchar](4) NULL,
	[DateChanged] [datetime] NOT NULL,
	[UserIDChanged] [varchar](4) NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [aaaaaDepositIDHeader_PK] PRIMARY KEY CLUSTERED 
(
	[DepositDate] ASC,
	[DepositID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[DepositIDHeader] ADD  CONSTRAINT [DF__DepositID__Depos__406F96A7]  DEFAULT ('1/1/1995') FOR [DepositDate]
GO
ALTER TABLE [dbo].[DepositIDHeader] ADD  CONSTRAINT [DF__DepositID__Depos__4163BAE0]  DEFAULT (0) FOR [DepositID]
GO
ALTER TABLE [dbo].[DepositIDHeader] ADD  CONSTRAINT [DF__DepositID__Calcu__4257DF19]  DEFAULT (0.0) FOR [CalculatedAmount]
GO
ALTER TABLE [dbo].[DepositIDHeader] ADD  CONSTRAINT [DF__DepositID__NextD__434C0352]  DEFAULT (0) FOR [NextDetailTransID]
GO
ALTER TABLE [dbo].[DepositIDHeader] ADD  CONSTRAINT [DF__DepositID__DateC__4440278B]  DEFAULT ('1/1/1995') FOR [DateCreated]
GO
ALTER TABLE [dbo].[DepositIDHeader] ADD  CONSTRAINT [DF__DepositID__UserI__45344BC4]  DEFAULT (' ') FOR [UserIDCreated]
GO
ALTER TABLE [dbo].[DepositIDHeader] ADD  CONSTRAINT [DF__DepositID__DateC__46286FFD]  DEFAULT ('1/1/1995') FOR [DateChanged]
GO
ALTER TABLE [dbo].[DepositIDHeader] ADD  CONSTRAINT [DF__DepositID__UserI__471C9436]  DEFAULT (' ') FOR [UserIDChanged]
GO
ALTER TABLE [dbo].[DepositIDHeader] ADD  CONSTRAINT [DF__DepositID__Statu__4810B86F]  DEFAULT (0) FOR [Status]
GO
