USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[IDGen_Table]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[IDGen_Table](
	[Context] [varchar](50) NOT NULL,
	[ID_Name] [varchar](30) NOT NULL,
	[Last_Value] [int] NULL,
	[Comment] [varchar](50) NULL,
 CONSTRAINT [PK_IDGen_Table] PRIMARY KEY NONCLUSTERED 
(
	[Context] ASC,
	[ID_Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[IDGen_Table] ADD  CONSTRAINT [DF_IDGen_Table_Last_Value]  DEFAULT (1) FOR [Last_Value]
GO
