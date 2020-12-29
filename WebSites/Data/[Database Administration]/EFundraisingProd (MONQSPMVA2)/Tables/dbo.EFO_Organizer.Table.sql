USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[EFO_Organizer]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EFO_Organizer](
	[Organizer_ID] [int] IDENTITY(26,1) NOT FOR REPLICATION NOT NULL,
	[Name] [varchar](50) NULL,
	[User_Name] [varchar](50) NULL,
	[Password] [varchar](15) NULL,
	[Title] [varchar](15) NULL,
	[Email] [varchar](75) NULL,
	[Best_Time_To_Call] [varchar](20) NULL,
	[Evening_Phone] [varchar](15) NULL,
	[Day_Phone] [varchar](15) NULL,
	[Fax_Number] [varchar](15) NULL,
	[Entry_Date] [smalldatetime] NOT NULL,
	[Comments] [varchar](150) NULL,
	[Organization_ID] [int] NOT NULL,
	[School_ID] [int] NULL,
 CONSTRAINT [PK_EFO_Organizer] PRIMARY KEY NONCLUSTERED 
(
	[Organizer_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
