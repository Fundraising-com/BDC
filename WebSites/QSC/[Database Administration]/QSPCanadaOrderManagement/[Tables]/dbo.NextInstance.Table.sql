USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[NextInstance]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NextInstance](
	[TableID] [int] NOT NULL,
	[NextInstance] [int] NOT NULL,
	[Description] [varchar](50) NULL,
 CONSTRAINT [aaaaaNextInstance_PK] PRIMARY KEY CLUSTERED 
(
	[TableID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[NextInstance] ADD  CONSTRAINT [DF__NextInsta__Table__07F6335A]  DEFAULT (0) FOR [TableID]
GO
ALTER TABLE [dbo].[NextInstance] ADD  CONSTRAINT [DF__NextInsta__NextI__08EA5793]  DEFAULT (0) FOR [NextInstance]
GO
ALTER TABLE [dbo].[NextInstance] ADD  CONSTRAINT [DF__NextInsta__Descr__09DE7BCC]  DEFAULT (null) FOR [Description]
GO
