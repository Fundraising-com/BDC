USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Mailing_Code]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Mailing_Code](
	[Mailing_Code_ID] [int] IDENTITY(204940,1) NOT FOR REPLICATION NOT NULL,
	[List_Name] [varchar](25) NOT NULL,
	[List_ID] [int] NOT NULL,
	[Flyer_Code] [varchar](25) NOT NULL,
	[Launch_Date] [datetime] NULL,
	[Mailing_Code_Label] [varchar](25) NOT NULL,
	[Mailing_Name_ID] [int] NULL,
 CONSTRAINT [PK_Mailing_Code] PRIMARY KEY CLUSTERED 
(
	[Mailing_Code_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
