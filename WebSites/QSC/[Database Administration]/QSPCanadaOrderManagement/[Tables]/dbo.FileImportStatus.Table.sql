USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[FileImportStatus]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FileImportStatus](
	[Name] [varchar](200) NOT NULL,
	[DateProcessed] [datetime] NOT NULL,
	[Type] [int] NOT NULL,
	[StatusInstance] [int] NOT NULL,
	[Description] [varchar](200) NULL,
	[DateFixed] [datetime] NOT NULL,
 CONSTRAINT [aaaaaFileImportStatus_PK] PRIMARY KEY CLUSTERED 
(
	[Name] ASC,
	[DateProcessed] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[FileImportStatus] ADD  CONSTRAINT [DF__FileImport__Name__72FB1674]  DEFAULT (' ') FOR [Name]
GO
ALTER TABLE [dbo].[FileImportStatus] ADD  CONSTRAINT [DF__FileImpor__DateP__73EF3AAD]  DEFAULT ('1/1/1995') FOR [DateProcessed]
GO
ALTER TABLE [dbo].[FileImportStatus] ADD  CONSTRAINT [DF__FileImport__Type__74E35EE6]  DEFAULT (0) FOR [Type]
GO
ALTER TABLE [dbo].[FileImportStatus] ADD  CONSTRAINT [DF__FileImpor__Statu__75D7831F]  DEFAULT (0) FOR [StatusInstance]
GO
ALTER TABLE [dbo].[FileImportStatus] ADD  CONSTRAINT [DF__FileImpor__Descr__76CBA758]  DEFAULT (null) FOR [Description]
GO
ALTER TABLE [dbo].[FileImportStatus] ADD  CONSTRAINT [DF__FileImpor__DateF__77BFCB91]  DEFAULT ('1/1/1995') FOR [DateFixed]
GO
