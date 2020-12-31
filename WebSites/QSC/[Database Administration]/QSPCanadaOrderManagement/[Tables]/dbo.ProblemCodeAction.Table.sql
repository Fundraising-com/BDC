USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ProblemCodeAction]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProblemCodeAction](
	[ProblemCodeInstance] [int] NOT NULL,
	[ActionInstance] [int] NOT NULL,
	[IsMandatory] [bit] NOT NULL,
 CONSTRAINT [PK_ProblemCodeActions] PRIMARY KEY CLUSTERED 
(
	[ProblemCodeInstance] ASC,
	[ActionInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
