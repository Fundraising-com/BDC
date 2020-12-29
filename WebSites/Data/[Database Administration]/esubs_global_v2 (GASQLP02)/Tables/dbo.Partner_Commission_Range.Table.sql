USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[Partner_Commission_Range]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Partner_Commission_Range](
	[Partner_Commission_Range_ID] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[MinThresholdValue] [int] NOT NULL,
	[MaxThresholdValue] [int] NOT NULL,
 CONSTRAINT [PK_Commission_Range] PRIMARY KEY CLUSTERED 
(
	[Partner_Commission_Range_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Partner_Commission_Range] ADD  CONSTRAINT [DF_Partner_Commission_Range_Start_Range]  DEFAULT ((0)) FOR [MinThresholdValue]
GO
ALTER TABLE [dbo].[Partner_Commission_Range] ADD  CONSTRAINT [DF_Partner_Commission_Range_Stop_Range]  DEFAULT ((0)) FOR [MaxThresholdValue]
GO
