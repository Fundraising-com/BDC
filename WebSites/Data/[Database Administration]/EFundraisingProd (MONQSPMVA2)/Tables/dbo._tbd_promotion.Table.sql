USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[_tbd_promotion]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[_tbd_promotion](
	[promotion_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[promotion_type_code] [varchar](4) NOT NULL,
	[targeted_market_id] [int] NULL,
	[advertising_support_id] [int] NULL,
	[advertisement_id] [int] NULL,
	[partner_id] [int] NOT NULL,
	[advertiser_id] [int] NULL,
	[transfer_status_id] [tinyint] NOT NULL,
	[advertisment_type_id] [int] NULL,
	[destination_id] [int] NULL,
	[advertiser_partner_id] [int] NULL,
	[grabber_id] [int] NULL,
	[description] [varchar](100) NOT NULL,
	[script_name] [varchar](100) NULL,
	[contact_name] [varchar](100) NULL,
	[visibility] [varchar](50) NULL,
	[tracking_serial] [varchar](35) NULL,
	[nb_impression_bought] [int] NULL,
	[is_active] [bit] NOT NULL,
	[cookie_content] [varchar](255) NULL,
	[is_predictive] [bit] NULL,
	[keyword] [varchar](255) NULL,
	[is_displayable] [bit] NULL,
 CONSTRAINT [PK_promotion] PRIMARY KEY CLUSTERED 
(
	[promotion_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[_tbd_promotion]  WITH NOCHECK ADD  CONSTRAINT [FK_promotion_advertisement] FOREIGN KEY([advertisement_id])
REFERENCES [dbo].[advertisement] ([advertisement_id])
GO
ALTER TABLE [dbo].[_tbd_promotion] CHECK CONSTRAINT [FK_promotion_advertisement]
GO
ALTER TABLE [dbo].[_tbd_promotion]  WITH CHECK ADD  CONSTRAINT [FK_promotion_advertiser] FOREIGN KEY([advertiser_id])
REFERENCES [dbo].[Advertiser] ([Advertiser_ID])
GO
ALTER TABLE [dbo].[_tbd_promotion] CHECK CONSTRAINT [FK_promotion_advertiser]
GO
ALTER TABLE [dbo].[_tbd_promotion]  WITH CHECK ADD  CONSTRAINT [FK_promotion_advertiser_partner] FOREIGN KEY([advertiser_partner_id])
REFERENCES [dbo].[Advertiser_Partner] ([Advertiser_Partner_ID])
GO
ALTER TABLE [dbo].[_tbd_promotion] CHECK CONSTRAINT [FK_promotion_advertiser_partner]
GO
ALTER TABLE [dbo].[_tbd_promotion]  WITH CHECK ADD  CONSTRAINT [FK_promotion_advertising_support] FOREIGN KEY([advertising_support_id])
REFERENCES [dbo].[Advertising_Support] ([Advertising_Support_ID])
GO
ALTER TABLE [dbo].[_tbd_promotion] CHECK CONSTRAINT [FK_promotion_advertising_support]
GO
ALTER TABLE [dbo].[_tbd_promotion]  WITH CHECK ADD  CONSTRAINT [FK_promotion_advertisment_type] FOREIGN KEY([advertisment_type_id])
REFERENCES [dbo].[Advertisment_Type] ([Advertisment_Type_ID])
GO
ALTER TABLE [dbo].[_tbd_promotion] CHECK CONSTRAINT [FK_promotion_advertisment_type]
GO
ALTER TABLE [dbo].[_tbd_promotion]  WITH CHECK ADD  CONSTRAINT [FK_promotion_Destinations] FOREIGN KEY([destination_id])
REFERENCES [dbo].[Destinations] ([Destination_ID])
GO
ALTER TABLE [dbo].[_tbd_promotion] CHECK CONSTRAINT [FK_promotion_Destinations]
GO
ALTER TABLE [dbo].[_tbd_promotion]  WITH CHECK ADD  CONSTRAINT [FK_promotion_grabber] FOREIGN KEY([grabber_id])
REFERENCES [dbo].[Grabber] ([Grabber_Id])
GO
ALTER TABLE [dbo].[_tbd_promotion] CHECK CONSTRAINT [FK_promotion_grabber]
GO
ALTER TABLE [dbo].[_tbd_promotion]  WITH NOCHECK ADD  CONSTRAINT [FK_promotion_partner] FOREIGN KEY([partner_id])
REFERENCES [dbo].[_tbd_partner] ([partner_id])
GO
ALTER TABLE [dbo].[_tbd_promotion] CHECK CONSTRAINT [FK_promotion_partner]
GO
ALTER TABLE [dbo].[_tbd_promotion]  WITH NOCHECK ADD  CONSTRAINT [FK_promotion_promotion_type] FOREIGN KEY([promotion_type_code])
REFERENCES [dbo].[_tbd_promotion_type] ([Promotion_Type_Code])
GO
ALTER TABLE [dbo].[_tbd_promotion] CHECK CONSTRAINT [FK_promotion_promotion_type]
GO
ALTER TABLE [dbo].[_tbd_promotion]  WITH CHECK ADD  CONSTRAINT [FK_promotion_targeted_market] FOREIGN KEY([targeted_market_id])
REFERENCES [dbo].[Targeted_Market] ([Targeted_Market_ID])
GO
ALTER TABLE [dbo].[_tbd_promotion] CHECK CONSTRAINT [FK_promotion_targeted_market]
GO
ALTER TABLE [dbo].[_tbd_promotion]  WITH CHECK ADD  CONSTRAINT [FK_promotion_transfer_status] FOREIGN KEY([transfer_status_id])
REFERENCES [dbo].[transfer_status] ([transfer_status_id])
GO
ALTER TABLE [dbo].[_tbd_promotion] CHECK CONSTRAINT [FK_promotion_transfer_status]
GO
ALTER TABLE [dbo].[_tbd_promotion] ADD  CONSTRAINT [DF_promotion_transfer_status_id]  DEFAULT ((1)) FOR [transfer_status_id]
GO
