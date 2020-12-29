USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Alias_Country_Code]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Alias_Country_Code](
	[Input_Country_Code] [varchar](255) NOT NULL,
	[Country_Code] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Alias_Country_Code] PRIMARY KEY NONCLUSTERED 
(
	[Input_Country_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Alias_Country_Code]  WITH CHECK ADD  CONSTRAINT [fk_acc_country_code] FOREIGN KEY([Country_Code])
REFERENCES [dbo].[Country] ([Country_Code])
GO
ALTER TABLE [dbo].[Alias_Country_Code] CHECK CONSTRAINT [fk_acc_country_code]
GO
