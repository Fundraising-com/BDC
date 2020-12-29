USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[WFC_Logs]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WFC_Logs](
	[id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[customer_number] [nchar](10) NULL,
	[order_number] [nchar](10) NULL,
	[lead_id] [int] NULL,
	[sale_id] [nchar](10) NULL,
	[date] [datetime] NOT NULL,
	[product_code] [nchar](255) NULL,
	[freight] [nchar](10) NULL,
	[result_message] [nchar](500) NULL,
	[address] [nchar](255) NULL,
 CONSTRAINT [PK_WFC_Logs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[WFC_Logs] ADD  CONSTRAINT [DF_WFC_Logs_date]  DEFAULT (getdate()) FOR [date]
GO
