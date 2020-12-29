USE [eFundweb]
GO
/****** Object:  Table [dbo].[Form_Type_Desc]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Form_Type_Desc](
	[Form_Type_ID] [int] NOT NULL,
	[Language_ID] [int] NOT NULL,
	[Description] [varchar](256) NULL,
 CONSTRAINT [PK_Form_Type_Desc] PRIMARY KEY CLUSTERED 
(
	[Form_Type_ID] ASC,
	[Language_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
