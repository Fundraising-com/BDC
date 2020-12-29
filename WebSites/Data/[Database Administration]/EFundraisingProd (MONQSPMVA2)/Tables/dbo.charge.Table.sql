USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[charge]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[charge](
	[charge_id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NOT NULL,
	[default_amount] [decimal](18, 2) NULL,
	[is_credit] [bit] NOT NULL,
	[is_disabled] [bit] NOT NULL,
	[create_date] [datetime] NOT NULL,
	[create_user_id] [int] NOT NULL,
	[update_date] [datetime] NULL,
	[update_user_id] [int] NULL,
	[fulf_charge_id] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[charge_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
