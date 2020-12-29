USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[Java_Errors]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Java_Errors](
	[Error_ID] [int] IDENTITY(1377,1) NOT FOR REPLICATION NOT NULL,
	[Class_Name] [varchar](255) NULL,
	[Error_Message] [text] NULL,
	[Error_Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Java_Errors] PRIMARY KEY NONCLUSTERED 
(
	[Error_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Java_Errors] ADD  CONSTRAINT [DF_Java_Errors_Error_Date]  DEFAULT (getdate()) FOR [Error_Date]
GO
