USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Bank_Account]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Bank_Account](
	[Bank_ID] [int] NOT NULL,
	[Bank_Account_No] [varchar](50) NOT NULL,
	[Currency_Code] [varchar](10) NOT NULL,
	[GL_Account_No] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Bank_Account] PRIMARY KEY CLUSTERED 
(
	[Bank_ID] ASC,
	[Bank_Account_No] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Bank_Account]  WITH NOCHECK ADD  CONSTRAINT [fk_Bank_ID] FOREIGN KEY([Bank_ID])
REFERENCES [dbo].[Bank] ([Bank_ID])
GO
ALTER TABLE [dbo].[Bank_Account] CHECK CONSTRAINT [fk_Bank_ID]
GO
