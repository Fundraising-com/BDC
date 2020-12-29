USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Brand_Coupon_Sheet]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brand_Coupon_Sheet](
	[Brand_ID] [int] NOT NULL,
	[Coupon_Sheet_ID] [int] NOT NULL,
	[Coupon_Per_Sheet] [smallint] NULL,
 CONSTRAINT [PK_Brand_Coupon_Sheet] PRIMARY KEY NONCLUSTERED 
(
	[Brand_ID] ASC,
	[Coupon_Sheet_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Brand_Coupon_Sheet]  WITH CHECK ADD  CONSTRAINT [fk_Brand_ID] FOREIGN KEY([Brand_ID])
REFERENCES [dbo].[Brand] ([Brand_ID])
GO
ALTER TABLE [dbo].[Brand_Coupon_Sheet] CHECK CONSTRAINT [fk_Brand_ID]
GO
ALTER TABLE [dbo].[Brand_Coupon_Sheet]  WITH CHECK ADD  CONSTRAINT [fk_Coupon_Sheet_ID] FOREIGN KEY([Coupon_Sheet_ID])
REFERENCES [dbo].[Coupon_Sheet] ([Coupon_Sheet_ID])
GO
ALTER TABLE [dbo].[Brand_Coupon_Sheet] CHECK CONSTRAINT [fk_Coupon_Sheet_ID]
GO
ALTER TABLE [dbo].[Brand_Coupon_Sheet] ADD  CONSTRAINT [DF_Brand_Coupon_Sheet_Coupon_Sheet_ID]  DEFAULT (0) FOR [Coupon_Sheet_ID]
GO
ALTER TABLE [dbo].[Brand_Coupon_Sheet] ADD  CONSTRAINT [DF_Brand_Coupon_Sheet_Coupon_Per_Sheet]  DEFAULT (0) FOR [Coupon_Per_Sheet]
GO
