USE [EFRCommon]
GO
/****** Object:  Table [dbo].[advertising]    Script Date: 02/14/2014 16:26:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[advertising](
	[advertising_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[lead_id] [int] NULL,
	[org_promotion_id] [int] NULL,
	[advertising_type_id] [int] NULL,
	[first_name] [varchar](20) NULL,
	[last_name] [varchar](20) NULL,
	[phone] [varchar](20) NULL,
	[email] [varchar](50) NULL,
	[compagnie_name] [varchar](45) NULL,
	[compagnie_url] [varchar](150) NULL,
	[display_url] [varchar](100) NULL,
	[listing_text] [varchar](355) NULL,
	[picture_url] [image] NULL,
	[image_type] [varchar](100) NULL,
	[is_visible] [varchar](20) NULL,
	[start_date] [datetime] NULL,
	[end_date] [datetime] NULL,
	[create_date] [datetime] NULL,
 CONSTRAINT [PK_advertising] PRIMARY KEY CLUSTERED 
(
	[advertising_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[advertising]  WITH CHECK ADD  CONSTRAINT [FK_advertising_advertising_type] FOREIGN KEY([advertising_type_id])
REFERENCES [dbo].[advertising_type] ([advertising_type_id])
GO
ALTER TABLE [dbo].[advertising] CHECK CONSTRAINT [FK_advertising_advertising_type]
GO
ALTER TABLE [dbo].[advertising] ADD  CONSTRAINT [DF_advertising_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
