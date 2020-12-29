USE [eFundweb]
GO
/****** Object:  Table [dbo].[Customer_Questions]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Customer_Questions](
	[Customer_Question_ID] [int] IDENTITY(1,1) NOT NULL,
	[First_Name] [varchar](25) NULL,
	[Last_Name] [varchar](25) NULL,
	[Phone] [varchar](15) NULL,
	[Phone_Ext] [varchar](5) NULL,
	[Email] [varchar](75) NULL,
	[Question] [varchar](1000) NULL,
	[Date_Inserted] [datetime] NOT NULL,
 CONSTRAINT [PK_Customer_Questions] PRIMARY KEY CLUSTERED 
(
	[Customer_Question_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Customer_Questions] ADD  DEFAULT (getdate()) FOR [Date_Inserted]
GO
