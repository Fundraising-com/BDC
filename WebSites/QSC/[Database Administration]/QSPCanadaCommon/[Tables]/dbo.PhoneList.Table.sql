USE [QSPCanadaCommon]
GO
/****** Object:  Table [dbo].[PhoneList]    Script Date: 06/07/2017 09:29:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhoneList](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NULL,
	[DeletedTF] [bit] NOT NULL,
 CONSTRAINT [PK_PhoneList] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PhoneList] ADD  CONSTRAINT [DF_PhoneList_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[PhoneList] ADD  CONSTRAINT [DF_PhoneList_DeletedTF]  DEFAULT (0) FOR [DeletedTF]
GO
