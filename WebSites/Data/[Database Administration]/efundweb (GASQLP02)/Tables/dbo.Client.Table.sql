USE [eFundweb]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Client](
	[Client_Sequence_Code] [varchar](4) NOT NULL,
	[Client_ID] [int] NOT NULL,
	[Organization_Class_Code] [varchar](4) NOT NULL,
	[Group_Type_ID] [int] NULL,
	[Channel_Code] [varchar](4) NOT NULL,
	[Promotion_ID] [int] NOT NULL,
	[Lead_ID] [int] NULL,
	[Division_ID] [int] NOT NULL,
	[Salutation] [varchar](10) NULL,
	[First_Name] [varchar](50) NOT NULL,
	[Last_name] [varchar](50) NOT NULL,
	[Title] [varchar](50) NULL,
	[Organization] [varchar](100) NULL,
	[Day_Phone] [varchar](20) NULL,
	[Day_Time_Call] [varchar](45) NULL,
	[Evening_Phone] [varchar](20) NULL,
	[Evening_Time_Call] [varchar](20) NULL,
	[Fax] [varchar](20) NULL,
	[Email] [varchar](50) NULL,
	[Extra_Comment] [varchar](2000) NULL,
	[Interested_In_Agent] [bit] NOT NULL,
	[CSR_Consultant_ID] [int] NULL,
	[Interested_In_Online] [bit] NOT NULL,
	[Day_Phone_Ext] [varchar](10) NULL,
	[Evening_Phone_Ext] [varchar](10) NULL,
	[Other_Phone] [varchar](20) NULL,
	[Other_Phone_Ext] [varchar](10) NULL,
	[Title_ID] [int] NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[Client_Sequence_Code] ASC,
	[Client_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Client] ADD  DEFAULT (0) FOR [Interested_In_Agent]
GO
ALTER TABLE [dbo].[Client] ADD  DEFAULT (0) FOR [Interested_In_Online]
GO
