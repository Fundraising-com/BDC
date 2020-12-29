USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Advertising_Support_Contact]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Advertising_Support_Contact](
	[Advertising_Support_Contact_ID] [int] NOT NULL,
	[Advertising_Support_ID] [int] NOT NULL,
	[First_Name] [varchar](35) NULL,
	[Last_Name] [varchar](35) NULL,
	[Phone_Number] [varchar](25) NULL,
	[Fax_Number] [varchar](25) NULL,
	[Email] [varchar](50) NULL,
 CONSTRAINT [PK_Advertising_Support_Contact] PRIMARY KEY NONCLUSTERED 
(
	[Advertising_Support_Contact_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Advertising_Support_Contact]  WITH CHECK ADD  CONSTRAINT [fk_ASC_Advertising_Support_ID] FOREIGN KEY([Advertising_Support_ID])
REFERENCES [dbo].[Advertising_Support] ([Advertising_Support_ID])
GO
ALTER TABLE [dbo].[Advertising_Support_Contact] CHECK CONSTRAINT [fk_ASC_Advertising_Support_ID]
GO
