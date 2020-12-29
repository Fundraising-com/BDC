USE [eFundweb]
GO
/****** Object:  Table [dbo].[Client_Address]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Client_Address](
	[Address_ID] [int] NOT NULL,
	[Client_Sequence_Code] [varchar](4) NOT NULL,
	[Client_ID] [int] NOT NULL,
	[Address_Type] [varchar](2) NOT NULL,
	[Street_Address] [varchar](100) NULL,
	[State_Code] [varchar](10) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[Zip_Code] [varchar](10) NULL,
	[Country_Code] [varchar](10) NOT NULL,
	[Attention_of] [varchar](100) NULL,
 CONSTRAINT [PK_Client_Address] PRIMARY KEY CLUSTERED 
(
	[Address_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Client_Address] ADD  DEFAULT (0) FOR [Address_ID]
GO
