USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[SC_SECTION]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SC_SECTION](
	[Section_Id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Section_Title] [varchar](100) NULL,
	[Section_Image] [varchar](200) NULL,
	[Section_Text] [text] NULL,
	[Section_Template] [varchar](200) NULL,
	[Section_Sub_Title] [varchar](100) NULL,
 CONSTRAINT [PK_SC_SECTION] PRIMARY KEY NONCLUSTERED 
(
	[Section_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
