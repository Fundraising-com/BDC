USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[TotePackages]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TotePackages](
	[ToteInstance] [int] NOT NULL,
	[PackageID] [varchar](40) NOT NULL,
 CONSTRAINT [aaaaaTotePackages_PK] PRIMARY KEY CLUSTERED 
(
	[ToteInstance] ASC,
	[PackageID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[TotePackages] ADD  CONSTRAINT [DF__TotePacka__ToteI__3EA749C6]  DEFAULT (0) FOR [ToteInstance]
GO
ALTER TABLE [dbo].[TotePackages] ADD  CONSTRAINT [DF__TotePacka__Packa__3F9B6DFF]  DEFAULT (' ') FOR [PackageID]
GO
