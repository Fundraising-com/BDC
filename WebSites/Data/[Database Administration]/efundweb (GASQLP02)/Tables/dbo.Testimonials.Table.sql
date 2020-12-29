USE [eFundweb]
GO
/****** Object:  Table [dbo].[Testimonials]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Testimonials](
	[Testimonials_ID] [int] NOT NULL,
	[Language_ID] [int] NOT NULL,
	[Text] [varchar](2000) NOT NULL,
	[Organism] [varchar](200) NOT NULL,
	[Responsible] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Testimonials] PRIMARY KEY CLUSTERED 
(
	[Testimonials_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
