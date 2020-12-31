USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[UnigistixStudentStaging]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UnigistixStudentStaging](
	[StudentInstance] [int] NOT NULL,
	[StudentLastName] [varchar](50) NULL,
	[StudentFirstName] [varchar](50) NULL,
	[ReadersPremiums] [int] NULL,
	[OtherPremiums] [int] NULL,
 CONSTRAINT [PK_UnigistixStudentStagingTable] PRIMARY KEY CLUSTERED 
(
	[StudentInstance] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
