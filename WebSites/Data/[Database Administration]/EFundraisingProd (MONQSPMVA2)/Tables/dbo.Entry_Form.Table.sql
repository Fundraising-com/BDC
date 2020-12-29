USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Entry_Form]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Entry_Form](
	[Entry_Form_ID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Entry_Form_Desc] [varchar](255) NOT NULL,
	[Master_Template] [varchar](255) NULL,
	[Content_Template] [varchar](255) NULL,
	[Web_Site_ID] [int] NULL,
 CONSTRAINT [PK_Entry_Form] PRIMARY KEY NONCLUSTERED 
(
	[Entry_Form_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Entry_Form]  WITH NOCHECK ADD  CONSTRAINT [fk_ef_web_site_id] FOREIGN KEY([Web_Site_ID])
REFERENCES [dbo].[Web_Site] ([Web_Site_Id])
GO
ALTER TABLE [dbo].[Entry_Form] CHECK CONSTRAINT [fk_ef_web_site_id]
GO
