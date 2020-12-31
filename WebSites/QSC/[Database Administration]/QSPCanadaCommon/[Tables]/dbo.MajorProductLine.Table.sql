USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[MajorProductLine]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MajorProductLine](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NULL,
	[QSPProductLineID] [int] NULL,
	[PriorityLevel] [int] NULL,
 CONSTRAINT [PK_MajorProductLine] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
