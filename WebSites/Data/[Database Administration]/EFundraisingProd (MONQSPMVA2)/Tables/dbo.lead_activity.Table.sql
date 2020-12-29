USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[lead_activity]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[lead_activity](
	[lead_activity_id] [int] NOT NULL,
	[lead_id] [int] NOT NULL,
	[lead_activity_type_id] [int] NOT NULL,
	[lead_activity_date] [datetime] NOT NULL,
	[completed_date] [datetime] NULL,
	[comments] [text] NULL,
 CONSTRAINT [PK_lead_activity] PRIMARY KEY NONCLUSTERED 
(
	[lead_activity_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[lead_activity]  WITH NOCHECK ADD  CONSTRAINT [FK_lead_activity_lead] FOREIGN KEY([lead_id])
REFERENCES [dbo].[lead] ([lead_id])
GO
ALTER TABLE [dbo].[lead_activity] CHECK CONSTRAINT [FK_lead_activity_lead]
GO
ALTER TABLE [dbo].[lead_activity]  WITH CHECK ADD  CONSTRAINT [FK_lead_activity_lead_activity_type] FOREIGN KEY([lead_activity_type_id])
REFERENCES [dbo].[Lead_Activity_Type] ([Lead_Activity_Type_Id])
GO
ALTER TABLE [dbo].[lead_activity] CHECK CONSTRAINT [FK_lead_activity_lead_activity_type]
GO
