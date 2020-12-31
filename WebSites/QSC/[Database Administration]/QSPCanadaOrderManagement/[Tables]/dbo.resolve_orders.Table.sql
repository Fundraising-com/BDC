USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[resolve_orders]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[resolve_orders](
	[OrderID] [int] NOT NULL,
	[asof] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_resolve_orders] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[resolve_orders] ADD  CONSTRAINT [DF_resolve_orders_asof]  DEFAULT (getdate()) FOR [asof]
GO
