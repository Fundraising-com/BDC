USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[RemitTest]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RemitTest](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Description] [varchar](500) NULL,
	[Script] [varchar](100) NULL,
	[CorrectionDescription] [varchar](500) NULL,
	[CorrectionScript] [varchar](100) NULL,
	[IsCritical] [bit] NOT NULL,
	[DateChanged] [datetime] NULL,
	[UserChangedInstance] [int] NULL,
	[SequenceID] [int] NULL,
	[DeletedTF] [bit] NULL,
 CONSTRAINT [PK_RemitTest] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[RemitTest] ADD  CONSTRAINT [DF_RemitTest_DeletedTF]  DEFAULT (0) FOR [DeletedTF]
GO
