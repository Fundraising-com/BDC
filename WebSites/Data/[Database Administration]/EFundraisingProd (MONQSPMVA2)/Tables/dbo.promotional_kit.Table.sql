USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[promotional_kit]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[promotional_kit](
	[promotional_kit_id] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[lead_id] [int] NOT NULL,
	[lead_visit_id] [int] NOT NULL,
	[kit_type_id] [int] NOT NULL,
	[carrier_id] [tinyint] NULL,
	[carrier_tracking_id] [int] NULL,
	[postal_address_id] [int] NULL,
	[validated] [smallint] NULL,
	[create_date] [datetime] NOT NULL,
	[sent_date] [datetime] NULL,
	[sample_id] [int] NULL,
 CONSTRAINT [PK_promotional_kit] PRIMARY KEY CLUSTERED 
(
	[promotional_kit_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[promotional_kit]  WITH NOCHECK ADD  CONSTRAINT [FK_promotional_kit_carrier] FOREIGN KEY([carrier_id])
REFERENCES [dbo].[carrier] ([carrier_id])
GO
ALTER TABLE [dbo].[promotional_kit] CHECK CONSTRAINT [FK_promotional_kit_carrier]
GO
ALTER TABLE [dbo].[promotional_kit]  WITH NOCHECK ADD  CONSTRAINT [FK_promotional_kit_Kit_Type] FOREIGN KEY([kit_type_id])
REFERENCES [dbo].[Kit_Type] ([Kit_Type_ID])
GO
ALTER TABLE [dbo].[promotional_kit] CHECK CONSTRAINT [FK_promotional_kit_Kit_Type]
GO
ALTER TABLE [dbo].[promotional_kit]  WITH NOCHECK ADD  CONSTRAINT [FK_promotional_kit_lead] FOREIGN KEY([lead_id])
REFERENCES [dbo].[lead] ([lead_id])
NOT FOR REPLICATION
GO
ALTER TABLE [dbo].[promotional_kit] CHECK CONSTRAINT [FK_promotional_kit_lead]
GO
ALTER TABLE [dbo].[promotional_kit]  WITH NOCHECK ADD  CONSTRAINT [FK_promotional_kit_Lead_Visit] FOREIGN KEY([lead_visit_id])
REFERENCES [dbo].[Lead_Visit] ([Lead_Visit_ID])
GO
ALTER TABLE [dbo].[promotional_kit] CHECK CONSTRAINT [FK_promotional_kit_Lead_Visit]
GO
ALTER TABLE [dbo].[promotional_kit]  WITH NOCHECK ADD  CONSTRAINT [FK_promotional_kit_sample] FOREIGN KEY([sample_id])
REFERENCES [dbo].[Sample] ([SampleID])
GO
ALTER TABLE [dbo].[promotional_kit] CHECK CONSTRAINT [FK_promotional_kit_sample]
GO
ALTER TABLE [dbo].[promotional_kit] ADD  CONSTRAINT [DF_promotional_kit_create_date]  DEFAULT (getdate()) FOR [create_date]
GO
