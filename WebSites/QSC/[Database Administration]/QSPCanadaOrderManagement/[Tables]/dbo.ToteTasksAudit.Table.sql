USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ToteTasksAudit]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToteTasksAudit](
	[ToteInstance] [int] NOT NULL,
	[ToteTaskInstance] [int] NOT NULL,
	[DateAdded] [datetime] NOT NULL,
 CONSTRAINT [PK_ToteTasksAudit] PRIMARY KEY CLUSTERED 
(
	[ToteInstance] ASC,
	[ToteTaskInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
