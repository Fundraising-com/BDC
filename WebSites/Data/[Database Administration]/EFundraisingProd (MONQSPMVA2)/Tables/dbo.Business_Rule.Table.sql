USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Business_Rule]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Business_Rule](
	[Business_Rule_ID] [int] NOT NULL,
	[Partner_ID] [int] NOT NULL,
	[Rule_Description] [varchar](100) NULL,
	[Module_Name] [varchar](25) NULL,
	[Form_Name] [varchar](50) NULL,
	[Access_Sub_Name] [varchar](50) NULL,
 CONSTRAINT [PK_Business_Rule] PRIMARY KEY NONCLUSTERED 
(
	[Business_Rule_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
