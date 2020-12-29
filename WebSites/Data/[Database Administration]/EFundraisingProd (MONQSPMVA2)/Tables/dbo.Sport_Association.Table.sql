USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Sport_Association]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sport_Association](
	[Sport_Association_Id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Sport_Ass_Desc] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Sport_Association] PRIMARY KEY NONCLUSTERED 
(
	[Sport_Association_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
