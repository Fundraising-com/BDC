USE [eFundweb]
GO
/****** Object:  Table [dbo].[Sweepstakes_Registration_NSG]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Sweepstakes_Registration_NSG](
	[Sweepstakes_Registration_ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NULL,
	[LastName] [varchar](100) NULL,
	[Address] [varchar](200) NULL,
	[City] [varchar](100) NULL,
	[Zip] [varchar](10) NULL,
	[State_Code] [varchar](10) NULL,
	[Phone] [char](15) NULL,
	[Phone_Ext] [char](5) NULL,
	[Email] [varchar](200) NULL,
	[Is_Transfered] [bit] NOT NULL,
	[Entry_Date] [datetime] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Sweepstakes_Registration_NSG] ADD  CONSTRAINT [DF_Sweepstakes_Registration_NSG_Is_Transfered]  DEFAULT (0) FOR [Is_Transfered]
GO
ALTER TABLE [dbo].[Sweepstakes_Registration_NSG] ADD  CONSTRAINT [DF__Sweepstak__Entry__33BFA6FF]  DEFAULT (getdate()) FOR [Entry_Date]
GO
