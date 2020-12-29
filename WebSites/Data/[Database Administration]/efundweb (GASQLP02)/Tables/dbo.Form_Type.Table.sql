USE [eFundweb]
GO
/****** Object:  Table [dbo].[Form_Type]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Form_Type](
	[Form_Type_ID] [int] NOT NULL,
	[Form_Type_Name] [varchar](200) NULL,
	[Lead_Status_ID] [int] NOT NULL,
 CONSTRAINT [PK_Form_Type] PRIMARY KEY CLUSTERED 
(
	[Form_Type_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
