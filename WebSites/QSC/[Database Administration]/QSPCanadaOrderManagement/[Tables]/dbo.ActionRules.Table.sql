USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[ActionRules]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ActionRules](
	[Instance] [int] IDENTITY(1,1) NOT NULL,
	[ActionInstance] [int] NOT NULL,
	[Unique] [bit] NOT NULL,
	[ErrorMessage] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_ActionRules] PRIMARY KEY CLUSTERED 
(
	[Instance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ActionRules] ADD  CONSTRAINT [DF_ActionRules_CanBeRepeat]  DEFAULT (1) FOR [Unique]
GO
ALTER TABLE [dbo].[ActionRules] ADD  CONSTRAINT [DF_ActionRules_ErrorMessage]  DEFAULT ('') FOR [ErrorMessage]
GO
