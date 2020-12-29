USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[advertisement]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[advertisement](
	[advertisement_id] [int] NOT NULL,
	[division_id] [tinyint] NOT NULL,
	[description] [varchar](50) NOT NULL,
	[size] [float] NULL,
	[nb_colors] [int] NULL,
	[comments] [varchar](255) NULL,
 CONSTRAINT [PK_advertisement] PRIMARY KEY CLUSTERED 
(
	[advertisement_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[advertisement]  WITH CHECK ADD  CONSTRAINT [FK_advertisement_division] FOREIGN KEY([division_id])
REFERENCES [dbo].[division] ([division_id])
GO
ALTER TABLE [dbo].[advertisement] CHECK CONSTRAINT [FK_advertisement_division]
GO
