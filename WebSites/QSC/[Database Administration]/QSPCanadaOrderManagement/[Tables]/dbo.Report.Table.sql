USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[Report]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Report](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](500) NULL,
	[CreateDate] [datetime] NULL,
	[ModifyDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[DeletedTF] [bit] NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
