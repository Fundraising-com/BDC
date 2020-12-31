USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[DailyCouponSummary]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DailyCouponSummary](
	[date] [datetime] NOT NULL,
	[estimatedcoupons] [int] NULL,
	[received] [int] NULL,
	[mailed] [int] NULL,
	[DateCreated] [datetime] NULL,
	[UserIDCreated] [varchar](4) NULL,
	[DateChanged] [datetime] NULL,
	[UserIDChanged] [varchar](4) NULL,
 CONSTRAINT [PK_DailyCouponSummary_1__10] PRIMARY KEY CLUSTERED 
(
	[date] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[DailyCouponSummary] ADD  CONSTRAINT [DF_DailyCoupo_DateCreated2__10]  DEFAULT (1 / 1 / 95) FOR [DateCreated]
GO
ALTER TABLE [dbo].[DailyCouponSummary] ADD  CONSTRAINT [DF_DailyCoupo_DateChanged1__10]  DEFAULT (1 / 1 / 95) FOR [DateChanged]
GO
