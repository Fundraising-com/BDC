USE [eFundweb]
GO
/****** Object:  Table [dbo].[Destinations]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Destinations](
	[Destination_ID] [int] NOT NULL,
	[Web_Site_ID] [int] NULL,
	[URL] [varchar](200) NOT NULL,
 CONSTRAINT [PK_destinations_1] PRIMARY KEY CLUSTERED 
(
	[Destination_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Destinations]  WITH CHECK ADD  CONSTRAINT [FK_Destinations_Web_Site] FOREIGN KEY([Web_Site_ID])
REFERENCES [dbo].[Web_Site] ([Web_Site_ID])
GO
ALTER TABLE [dbo].[Destinations] CHECK CONSTRAINT [FK_Destinations_Web_Site]
GO
