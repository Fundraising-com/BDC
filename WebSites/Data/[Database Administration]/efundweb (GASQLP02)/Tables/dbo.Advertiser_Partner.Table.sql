USE [eFundweb]
GO
/****** Object:  Table [dbo].[Advertiser_Partner]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Advertiser_Partner](
	[Advertiser_Partner_ID] [int] NOT NULL,
	[Advertiser_ID] [int] NULL,
	[Description] [varchar](200) NULL,
 CONSTRAINT [PK_Advertiser_Partner] PRIMARY KEY CLUSTERED 
(
	[Advertiser_Partner_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Advertiser_Partner]  WITH CHECK ADD  CONSTRAINT [FK_Advertiser_Partner_Advertiser] FOREIGN KEY([Advertiser_ID])
REFERENCES [dbo].[Advertiser] ([Advertiser_ID])
GO
ALTER TABLE [dbo].[Advertiser_Partner] CHECK CONSTRAINT [FK_Advertiser_Partner_Advertiser]
GO
