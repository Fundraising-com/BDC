USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Billing_Company]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Billing_Company](
	[Billing_Company_ID] [int] NOT NULL,
	[Billing_Company_Code] [varchar](20) NOT NULL,
	[Billing_Company_Name] [varchar](50) NOT NULL,
	[Street_Address] [varchar](100) NOT NULL,
	[City_Name] [varchar](50) NOT NULL,
	[State_Code] [varchar](10) NOT NULL,
	[Zip_Code] [varchar](10) NOT NULL,
	[Country_Code] [varchar](10) NOT NULL,
	[Telephone_Number] [varchar](20) NOT NULL,
	[email] [varchar](50) NULL,
	[Web] [varchar](50) NULL,
	[Logo] [text] NULL,
	[Invoice_Title] [varchar](20) NULL,
	[Invoice_Footer] [text] NULL,
	[fax_number] [varchar](20) NULL,
	[logo_path] [varchar](100) NULL,
	[culture_id] [tinyint] NOT NULL,
 CONSTRAINT [PK_Billing_Company] PRIMARY KEY CLUSTERED 
(
	[Billing_Company_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY],
 CONSTRAINT [UQ_Billing_Company] UNIQUE NONCLUSTERED 
(
	[Billing_Company_Code] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Billing_Company]  WITH CHECK ADD  CONSTRAINT [fk_Country_Code] FOREIGN KEY([Country_Code])
REFERENCES [dbo].[Country] ([Country_Code])
GO
ALTER TABLE [dbo].[Billing_Company] CHECK CONSTRAINT [fk_Country_Code]
GO
ALTER TABLE [dbo].[Billing_Company]  WITH NOCHECK ADD  CONSTRAINT [fk_State_Code] FOREIGN KEY([State_Code])
REFERENCES [dbo].[State] ([State_Code])
GO
ALTER TABLE [dbo].[Billing_Company] CHECK CONSTRAINT [fk_State_Code]
GO
ALTER TABLE [dbo].[Billing_Company] ADD  CONSTRAINT [DF_Billing_Company_culture_id]  DEFAULT (43) FOR [culture_id]
GO
