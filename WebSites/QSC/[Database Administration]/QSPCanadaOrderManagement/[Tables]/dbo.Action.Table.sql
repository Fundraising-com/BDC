USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[Action]    Script Date: 06/07/2017 09:18:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Action](
	[Instance] [int] NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[ReponsibleDeptInstance] [int] NOT NULL,
	[IsNotifyPublisherPrint] [bit] NOT NULL,
	[IsActionUserUpdatable] [bit] NOT NULL,
	[Message] [nvarchar](200) NULL,
	[CommentsIsRequired] [bit] NOT NULL,
	[DisplayOrder] [int] NULL,
 CONSTRAINT [PK_Actions] PRIMARY KEY CLUSTERED 
(
	[Instance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Action', @level2type=N'COLUMN',@level2name=N'Instance'
GO
ALTER TABLE [dbo].[Action] ADD  CONSTRAINT [DF_Action_CommentsRequired]  DEFAULT (0) FOR [CommentsIsRequired]
GO
