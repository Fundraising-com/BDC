USE [QSPCanadaOrderManagement]
GO
/****** Object:  Table [dbo].[GiftCardOutput]    Script Date: 06/07/2017 09:18:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[GiftCardOutput](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [varchar](50) NOT NULL,
	[Date] [datetime] NOT NULL,
	[FileContent] [text] NULL,
 CONSTRAINT [PK_GiftCardOutput] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[GiftCardOutput] ADD  CONSTRAINT [DF_GiftCardOutput_Date]  DEFAULT (getdate()) FOR [Date]
GO
