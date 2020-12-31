USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ToteDirectMailExtra]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ToteDirectMailExtra](
	[ToteInstance] [int] NOT NULL,
	[Booklets] [int] NOT NULL,
	[FamilyEnvelopes] [int] NOT NULL,
	[DateReported] [datetime] NULL,
	[MarketingCodesInstance] [int] NULL,
	[UserID] [char](4) NULL,
	[DateChanged] [datetime] NOT NULL,
	[CouponEstimate] [decimal](10, 2) NULL,
 CONSTRAINT [aaaaaToteDirectMailExtra_PK] PRIMARY KEY CLUSTERED 
(
	[ToteInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[ToteDirectMailExtra] ADD  CONSTRAINT [DF__ToteDirec__ToteI__38EE7070]  DEFAULT (0) FOR [ToteInstance]
GO
ALTER TABLE [dbo].[ToteDirectMailExtra] ADD  CONSTRAINT [DF__ToteDirec__Bookl__39E294A9]  DEFAULT (0) FOR [Booklets]
GO
ALTER TABLE [dbo].[ToteDirectMailExtra] ADD  CONSTRAINT [DF__ToteDirec__Famil__3AD6B8E2]  DEFAULT (0) FOR [FamilyEnvelopes]
GO
ALTER TABLE [dbo].[ToteDirectMailExtra] ADD  CONSTRAINT [DF_ToteDirect_DateReport1__211]  DEFAULT ('1/1/95') FOR [DateReported]
GO
ALTER TABLE [dbo].[ToteDirectMailExtra] ADD  CONSTRAINT [DF__ToteDirec__Marke__30AE302A]  DEFAULT (0) FOR [MarketingCodesInstance]
GO
ALTER TABLE [dbo].[ToteDirectMailExtra] ADD  CONSTRAINT [DF__ToteDirec__DateC__3EFC4F81]  DEFAULT (getdate()) FOR [DateChanged]
GO
ALTER TABLE [dbo].[ToteDirectMailExtra] ADD  CONSTRAINT [DF__ToteDirec__Coupo__41D8BC2C]  DEFAULT (0) FOR [CouponEstimate]
GO
