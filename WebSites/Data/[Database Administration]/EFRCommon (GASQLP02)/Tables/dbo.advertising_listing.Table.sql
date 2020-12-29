USE [EFRCommon]
GO
/****** Object:  Table [dbo].[advertising_listing]    Script Date: 02/14/2014 16:26:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[advertising_listing](
	[advertising_listing_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[listing_id] [int] NOT NULL,
	[advertising_id] [int] NOT NULL,
	[start_date] [datetime] NULL,
	[end_date] [datetime] NULL,
	[create_date] [datetime] NOT NULL,
 CONSTRAINT [PK_advertising_listing] PRIMARY KEY CLUSTERED 
(
	[advertising_listing_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[advertising_listing]  WITH CHECK ADD  CONSTRAINT [FK_advertising_listing_advertising] FOREIGN KEY([advertising_id])
REFERENCES [dbo].[advertising] ([advertising_id])
GO
ALTER TABLE [dbo].[advertising_listing] CHECK CONSTRAINT [FK_advertising_listing_advertising]
GO
ALTER TABLE [dbo].[advertising_listing]  WITH CHECK ADD  CONSTRAINT [FK_advertising_listing_listing] FOREIGN KEY([listing_id])
REFERENCES [dbo].[listing] ([listing_id])
GO
ALTER TABLE [dbo].[advertising_listing] CHECK CONSTRAINT [FK_advertising_listing_listing]
GO
ALTER TABLE [dbo].[advertising_listing] ADD  CONSTRAINT [DF_advertising_listing_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
