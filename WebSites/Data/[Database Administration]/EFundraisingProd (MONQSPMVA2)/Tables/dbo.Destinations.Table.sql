USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Destinations]    Script Date: 02/14/2014 16:28:18 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
