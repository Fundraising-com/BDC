USE [EFRCommon]
GO
/****** Object:  Table [dbo].[profit_group]    Script Date: 02/14/2014 16:26:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[profit_group](
	[profit_group_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[description] [varchar](50) NULL,
	[disclaimer] [varchar](500) NULL,
	[alt_disclaimer] [varchar](500) NULL,
 CONSTRAINT [PK_profit_group] PRIMARY KEY CLUSTERED 
(
	[profit_group_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
