USE [eFundweb]
GO
/****** Object:  Table [dbo].[Questions]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Questions](
	[Questions_ID] [int] NOT NULL,
	[Questions_Name] [varchar](100) NULL,
	[Questions_Description] [varchar](600) NULL,
	[Questions_Type] [varchar](100) NULL,
	[Validation_Type] [varchar](100) NULL,
	[Min_Lenght] [int] NULL,
	[Max_Lenght] [int] NULL,
	[Nbr_Values] [int] NOT NULL,
 CONSTRAINT [PK_Questions] PRIMARY KEY CLUSTERED 
(
	[Questions_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Questions] ADD  CONSTRAINT [DF__Questions__Nbr_V__25869641]  DEFAULT (1) FOR [Nbr_Values]
GO
