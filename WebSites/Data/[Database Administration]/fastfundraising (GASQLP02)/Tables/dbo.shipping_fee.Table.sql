USE [fastfundraising]
GO
/****** Object:  Table [dbo].[shipping_fee]    Script Date: 02/14/2014 16:35:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[shipping_fee](
	[shipping_fee_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[sale_amt_min] [int] NULL,
	[sale_amt_max] [int] NULL,
	[shipping_fee] [money] NULL,
 CONSTRAINT [PK_shipping_fee] PRIMARY KEY CLUSTERED 
(
	[shipping_fee_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
