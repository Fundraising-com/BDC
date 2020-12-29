USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[sales_constraints]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sales_constraints](
	[sales_constraint_id] [tinyint] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[product_class_id] [tinyint] NOT NULL,
	[description] [varchar](250) NOT NULL,
	[high_level] [bit] NOT NULL,
 CONSTRAINT [PK_sales_constraints] PRIMARY KEY CLUSTERED 
(
	[sales_constraint_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[sales_constraints] ADD  CONSTRAINT [DF_sales_constraints_high_level]  DEFAULT (0) FOR [high_level]
GO
