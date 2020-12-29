USE [eFundstore]
GO
/****** Object:  Table [dbo].[shopping_cart]    Script Date: 02/14/2014 16:26:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[shopping_cart](
	[shopping_cart_id] [int] IDENTITY(1,1) NOT NULL,
	[visitor_log_id] [int] NOT NULL,
	[online_user_id] [int] NULL,
	[shopping_cart_code] [char](1) NULL,
	[date_created] [datetime] NOT NULL,
 CONSTRAINT [PK_shopping_cart] PRIMARY KEY CLUSTERED 
(
	[shopping_cart_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[shopping_cart] ADD  CONSTRAINT [DF_Shopping_Cart_date_created]  DEFAULT (getdate()) FOR [date_created]
GO
