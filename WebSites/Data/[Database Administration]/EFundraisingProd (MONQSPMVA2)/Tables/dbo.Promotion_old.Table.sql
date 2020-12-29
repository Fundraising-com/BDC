USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Promotion_old]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Promotion_old](
	[Promotion_ID] [int] NOT NULL,
	[Promotion_Type_Code] [varchar](4) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Visibility] [varchar](50) NULL,
	[Contact_Name] [varchar](100) NULL,
	[Tracking_Serial] [varchar](35) NULL,
	[Nb_Impression_Bought] [int] NULL,
	[Is_Active] [bit] NOT NULL,
	[Targeted_Market_ID] [int] NULL,
	[Advertising_Support_ID] [int] NULL,
	[Advertisement_Id] [int] NULL,
	[Partner_ID] [int] NOT NULL,
	[Cookie_Content] [varchar](255) NULL,
	[Grabber_Id] [int] NULL,
	[Is_Predictive] [bit] NULL,
	[Advertiser_ID] [int] NULL,
	[Keyword] [varchar](255) NULL,
	[Script_Name] [varchar](100) NULL,
	[Advertisment_Type_ID] [int] NULL,
	[Destination_ID] [int] NULL,
	[Advertiser_Partner_ID] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Promotion_old] ADD  CONSTRAINT [DF_Promotion_Is_Active]  DEFAULT (0) FOR [Is_Active]
GO
ALTER TABLE [dbo].[Promotion_old] ADD  CONSTRAINT [DF_Promotion_Partner_ID]  DEFAULT (0) FOR [Partner_ID]
GO
ALTER TABLE [dbo].[Promotion_old] ADD  CONSTRAINT [DF_Promotion_Is_Predictive]  DEFAULT (0) FOR [Is_Predictive]
GO
