USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[customer_status]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[customer_status](
	[customer_status_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[customer_status_desc] [varchar](100) NULL,
	[create_date] [datetime] NULL,
 CONSTRAINT [PK_customer_status] PRIMARY KEY CLUSTERED 
(
	[customer_status_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[customer_status] ADD  CONSTRAINT [DF_customer_status_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
