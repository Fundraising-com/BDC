USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[conciliation]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[conciliation](
	[conciliation_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[sales_id] [int] NOT NULL,
	[sales_item_no] [smallint] NOT NULL,
	[supplier_id] [tinyint] NOT NULL,
	[conciliate_date] [datetime] NOT NULL,
	[is_conciliated] [bit] NOT NULL,
	[is_ordered] [bit] NOT NULL,
	[invoice_number] [varchar](25) NULL,
 CONSTRAINT [PK_conciliation] PRIMARY KEY CLUSTERED 
(
	[conciliation_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[conciliation] ADD  CONSTRAINT [DF_conciliation_is_conciliated]  DEFAULT (0) FOR [is_conciliated]
GO
ALTER TABLE [dbo].[conciliation] ADD  CONSTRAINT [DF_conciliation_is_ordered]  DEFAULT (0) FOR [is_ordered]
GO
