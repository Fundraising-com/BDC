USE [eFundweb]
GO
/****** Object:  Table [dbo].[Sweepstakes_Registration]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sweepstakes_Registration](
	[Sweepstakes_Registration_ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[Address] [varchar](200) NULL,
	[City] [varchar](100) NULL,
	[Zip] [varchar](10) NULL,
	[State_Code] [varchar](10) NULL,
	[Phone] [varchar](15) NULL,
	[Phone_Ext] [varchar](5) NULL,
	[Is_Transfered] [bit] NULL,
	[Entry_Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Sweepstakes_Registration] PRIMARY KEY CLUSTERED 
(
	[Sweepstakes_Registration_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Sweepstakes_Registration] ADD  CONSTRAINT [DF__Sweepstak__Is_Tr__5FB337D6]  DEFAULT (0) FOR [Is_Transfered]
GO
ALTER TABLE [dbo].[Sweepstakes_Registration] ADD  CONSTRAINT [DF__Sweepstak__Entry__60A75C0F]  DEFAULT (getdate()) FOR [Entry_Date]
GO
