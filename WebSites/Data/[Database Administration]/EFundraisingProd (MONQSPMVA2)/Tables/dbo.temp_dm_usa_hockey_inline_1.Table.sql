USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[temp_dm_usa_hockey_inline_1]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[temp_dm_usa_hockey_inline_1](
	[id] [int] NULL,
	[Compagnie] [varchar](200) NULL,
	[Contact] [varchar](200) NULL,
	[Address1] [varchar](200) NULL,
	[Address2] [varchar](200) NULL,
	[City] [varchar](200) NULL,
	[State] [varchar](200) NULL,
	[Zip] [varchar](200) NULL,
	[Phone] [varchar](200) NULL,
	[Ext] [varchar](200) NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
