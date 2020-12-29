USE [eFundweb]
GO
/****** Object:  Table [dbo].[Web_Visit]    Script Date: 02/14/2014 16:26:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Web_Visit](
	[Web_Visit_ID] [int] NOT NULL,
	[Promotion_ID] [int] NULL,
	[Entry_Form_ID] [int] NULL,
	[Temp_Lead_ID] [int] NULL,
	[Entry_Date] [datetime] NULL,
	[Referrer] [varchar](255) NULL,
	[URL] [varchar](100) NULL,
	[Query_String] [varchar](255) NULL,
	[Host] [varchar](255) NULL,
 CONSTRAINT [PK_Web_Visit] PRIMARY KEY CLUSTERED 
(
	[Web_Visit_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Web_Visit]  WITH CHECK ADD  CONSTRAINT [FK_Web_Visit_Entry_Form] FOREIGN KEY([Entry_Form_ID])
REFERENCES [dbo].[Entry_Form] ([Entry_Form_ID])
GO
ALTER TABLE [dbo].[Web_Visit] CHECK CONSTRAINT [FK_Web_Visit_Entry_Form]
GO
ALTER TABLE [dbo].[Web_Visit]  WITH NOCHECK ADD  CONSTRAINT [FK_Web_Visit_Temp_Lead] FOREIGN KEY([Temp_Lead_ID])
REFERENCES [dbo].[Temp_Lead] ([Temp_Lead_ID])
GO
ALTER TABLE [dbo].[Web_Visit] CHECK CONSTRAINT [FK_Web_Visit_Temp_Lead]
GO
