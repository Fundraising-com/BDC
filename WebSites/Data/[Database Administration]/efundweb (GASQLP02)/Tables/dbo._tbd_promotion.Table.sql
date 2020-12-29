USE [eFundweb]
GO
/****** Object:  Table [dbo].[_tbd_promotion]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[_tbd_promotion](
	[Promotion_ID] [int] NOT NULL,
	[Promotion_Type_Code] [varchar](4) NULL,
	[Description] [varchar](100) NOT NULL,
	[Visibility] [varchar](50) NULL,
	[Contact_Name] [varchar](100) NULL,
	[Tracking_Serial] [varchar](35) NULL,
	[Nb_Impression_Bought] [int] NULL,
	[Is_Active] [bit] NOT NULL,
	[Targeted_Market_ID] [int] NULL,
	[Advertising_Support_ID] [int] NULL,
	[Advertisement_ID] [int] NULL,
	[Partner_ID] [int] NOT NULL,
	[Cookie_Content] [varchar](255) NULL,
	[Grabber_ID] [int] NULL,
	[Is_Predictive] [bit] NOT NULL,
	[Advertiser_ID] [int] NULL,
	[Keyword] [varchar](255) NULL,
	[Script_Name] [varchar](100) NULL,
	[Advertisment_Type_ID] [int] NULL,
	[Destination_ID] [int] NULL,
	[Advertiser_Partner_ID] [int] NULL,
	[Is_Displayable] [bit] NOT NULL,
 CONSTRAINT [PK_Promotion] PRIMARY KEY CLUSTERED 
(
	[Promotion_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[_tbd_promotion]  WITH NOCHECK ADD  CONSTRAINT [FK_Promotion_Advertisement] FOREIGN KEY([Advertisement_ID])
REFERENCES [dbo].[Advertisement] ([Advertisement_ID])
GO
ALTER TABLE [dbo].[_tbd_promotion] CHECK CONSTRAINT [FK_Promotion_Advertisement]
GO
ALTER TABLE [dbo].[_tbd_promotion]  WITH NOCHECK ADD  CONSTRAINT [FK_Promotion_Advertiser] FOREIGN KEY([Advertiser_ID])
REFERENCES [dbo].[Advertiser] ([Advertiser_ID])
GO
ALTER TABLE [dbo].[_tbd_promotion] CHECK CONSTRAINT [FK_Promotion_Advertiser]
GO
ALTER TABLE [dbo].[_tbd_promotion]  WITH NOCHECK ADD  CONSTRAINT [FK_Promotion_Advertiser_Partner] FOREIGN KEY([Advertiser_Partner_ID])
REFERENCES [dbo].[Advertiser_Partner] ([Advertiser_Partner_ID])
GO
ALTER TABLE [dbo].[_tbd_promotion] CHECK CONSTRAINT [FK_Promotion_Advertiser_Partner]
GO
ALTER TABLE [dbo].[_tbd_promotion]  WITH NOCHECK ADD  CONSTRAINT [FK_Promotion_Advertising_Support] FOREIGN KEY([Advertising_Support_ID])
REFERENCES [dbo].[Advertising_Support] ([Advertising_Support_ID])
GO
ALTER TABLE [dbo].[_tbd_promotion] CHECK CONSTRAINT [FK_Promotion_Advertising_Support]
GO
ALTER TABLE [dbo].[_tbd_promotion]  WITH NOCHECK ADD  CONSTRAINT [FK_Promotion_Advertisment_Type] FOREIGN KEY([Advertisment_Type_ID])
REFERENCES [dbo].[Advertisment_Type] ([Advertisment_Type_ID])
GO
ALTER TABLE [dbo].[_tbd_promotion] CHECK CONSTRAINT [FK_Promotion_Advertisment_Type]
GO
ALTER TABLE [dbo].[_tbd_promotion]  WITH NOCHECK ADD  CONSTRAINT [FK_Promotion_Destinations] FOREIGN KEY([Destination_ID])
REFERENCES [dbo].[Destinations] ([Destination_ID])
GO
ALTER TABLE [dbo].[_tbd_promotion] CHECK CONSTRAINT [FK_Promotion_Destinations]
GO
ALTER TABLE [dbo].[_tbd_promotion]  WITH NOCHECK ADD  CONSTRAINT [FK_Promotion_Grabber] FOREIGN KEY([Grabber_ID])
REFERENCES [dbo].[Grabber] ([Grabber_ID])
GO
ALTER TABLE [dbo].[_tbd_promotion] CHECK CONSTRAINT [FK_Promotion_Grabber]
GO
ALTER TABLE [dbo].[_tbd_promotion]  WITH NOCHECK ADD  CONSTRAINT [FK_Promotion_Partner] FOREIGN KEY([Partner_ID])
REFERENCES [dbo].[_tbd_partner] ([partner_id])
GO
ALTER TABLE [dbo].[_tbd_promotion] CHECK CONSTRAINT [FK_Promotion_Partner]
GO
ALTER TABLE [dbo].[_tbd_promotion]  WITH NOCHECK ADD  CONSTRAINT [FK_Promotion_Promotion_Type] FOREIGN KEY([Promotion_Type_Code])
REFERENCES [dbo].[_tbd_promotion_type] ([Promotion_Type_Code])
GO
ALTER TABLE [dbo].[_tbd_promotion] CHECK CONSTRAINT [FK_Promotion_Promotion_Type]
GO
ALTER TABLE [dbo].[_tbd_promotion]  WITH NOCHECK ADD  CONSTRAINT [FK_Promotion_Targeted_Market] FOREIGN KEY([Targeted_Market_ID])
REFERENCES [dbo].[Targeted_Market] ([Targeted_Market_ID])
GO
ALTER TABLE [dbo].[_tbd_promotion] CHECK CONSTRAINT [FK_Promotion_Targeted_Market]
GO
ALTER TABLE [dbo].[_tbd_promotion] ADD  CONSTRAINT [DF__Promotion__Is_Ac__00200768]  DEFAULT (1) FOR [Is_Active]
GO
ALTER TABLE [dbo].[_tbd_promotion] ADD  CONSTRAINT [DF__Promotion__Partn__01142BA1]  DEFAULT (0) FOR [Partner_ID]
GO
ALTER TABLE [dbo].[_tbd_promotion] ADD  CONSTRAINT [DF__Promotion__Is_Pr__02084FDA]  DEFAULT (1) FOR [Is_Predictive]
GO
ALTER TABLE [dbo].[_tbd_promotion] ADD  CONSTRAINT [DF_Promotion_Is_Displayable]  DEFAULT (0) FOR [Is_Displayable]
GO
