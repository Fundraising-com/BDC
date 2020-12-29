USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Lead_Visit]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Lead_Visit](
	[Lead_Visit_ID] [int] NOT NULL,
	[Promotion_ID] [int] NULL,
	[Lead_ID] [int] NULL,
	[Temp_Lead_ID] [int] NULL,
	[Visit_Date] [datetime] NULL,
	[Channel_Code] [varchar](4) NULL,
 CONSTRAINT [PK_Lead_Visit] PRIMARY KEY NONCLUSTERED 
(
	[Lead_Visit_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Lead_Visit]  WITH NOCHECK ADD  CONSTRAINT [FK_lead_visit_lead] FOREIGN KEY([Lead_ID])
REFERENCES [dbo].[lead] ([lead_id])
GO
ALTER TABLE [dbo].[Lead_Visit] CHECK CONSTRAINT [FK_lead_visit_lead]
GO
ALTER TABLE [dbo].[Lead_Visit]  WITH CHECK ADD  CONSTRAINT [fk_lv_channel_code] FOREIGN KEY([Channel_Code])
REFERENCES [dbo].[Lead_Channel] ([Channel_Code])
GO
ALTER TABLE [dbo].[Lead_Visit] CHECK CONSTRAINT [fk_lv_channel_code]
GO
ALTER TABLE [dbo].[Lead_Visit] ADD  CONSTRAINT [DF_Lead_Visit_Visit_Date]  DEFAULT (getdate()) FOR [Visit_Date]
GO
