USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[supplier]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[supplier](
	[supplier_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[supplier_name] [varchar](50) NOT NULL,
	[street_adress] [varchar](255) NULL,
	[city] [varchar](30) NULL,
	[zip] [varchar](20) NULL,
	[contact_name] [varchar](50) NULL,
	[phone] [varchar](20) NULL,
	[fax] [varchar](20) NULL,
	[account_no] [varchar](20) NULL,
	[credit_margin] [decimal](15, 4) NULL,
	[state_code] [varchar](10) NULL,
	[country_code] [varchar](10) NULL,
	[comments] [text] NULL,
 CONSTRAINT [PK_supplier] PRIMARY KEY CLUSTERED 
(
	[supplier_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
