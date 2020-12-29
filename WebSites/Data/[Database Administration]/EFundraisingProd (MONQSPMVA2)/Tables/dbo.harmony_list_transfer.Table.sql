USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[harmony_list_transfer]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[harmony_list_transfer](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[list_name] [varchar](100) NULL,
	[list_desc] [varchar](100) NULL,
 CONSTRAINT [PK_harmony_list_transfer] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
