USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[EFO_Campaign]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EFO_Campaign](
	[Campaign_ID] [int] IDENTITY(33,1) NOT FOR REPLICATION NOT NULL,
	[Group_Type_ID] [int] NOT NULL,
	[QSP_Program_ID] [int] NOT NULL,
	[Campaign_Image_ID] [int] NOT NULL,
	[Organizer_ID] [int] NOT NULL,
	[Group_Name] [varchar](50) NULL,
	[Creation_Date] [smalldatetime] NOT NULL,
	[Financial_Goal] [decimal](10, 2) NOT NULL,
	[Fund_Raising_Reason] [varchar](200) NOT NULL,
	[Background_Info] [varchar](200) NULL,
	[Comments] [varchar](150) NULL,
	[Is_Launched] [bit] NOT NULL,
	[Is_Over] [bit] NOT NULL,
	[Account_Number] [varchar](15) NULL,
 CONSTRAINT [PK_EFO_Campaign] PRIMARY KEY NONCLUSTERED 
(
	[Campaign_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
