USE [eFundweb]
GO
/****** Object:  Table [dbo].[Advertiser]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Advertiser](
	[Advertiser_ID] [int] NOT NULL,
	[Advertisment_Type_ID] [int] NULL,
	[Contact_ID] [int] NULL,
	[Advertiser_Name] [varchar](200) NULL,
 CONSTRAINT [PK_Advertiser] PRIMARY KEY CLUSTERED 
(
	[Advertiser_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Advertiser]  WITH CHECK ADD  CONSTRAINT [FK_Advertiser_Advertisment_Type] FOREIGN KEY([Advertisment_Type_ID])
REFERENCES [dbo].[Advertisment_Type] ([Advertisment_Type_ID])
GO
ALTER TABLE [dbo].[Advertiser] CHECK CONSTRAINT [FK_Advertiser_Advertisment_Type]
GO
ALTER TABLE [dbo].[Advertiser]  WITH CHECK ADD  CONSTRAINT [FK_Advertiser_Contacts] FOREIGN KEY([Contact_ID])
REFERENCES [dbo].[Contacts] ([Contact_ID])
GO
ALTER TABLE [dbo].[Advertiser] CHECK CONSTRAINT [FK_Advertiser_Contacts]
GO
