USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ToteEMagnetExtra]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToteEMagnetExtra](
	[ToteInstance] [int] NOT NULL,
	[DMToteInstance] [int] NOT NULL,
 CONSTRAINT [PK_ToteEMagnetExtra] PRIMARY KEY CLUSTERED 
(
	[ToteInstance] ASC,
	[DMToteInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
