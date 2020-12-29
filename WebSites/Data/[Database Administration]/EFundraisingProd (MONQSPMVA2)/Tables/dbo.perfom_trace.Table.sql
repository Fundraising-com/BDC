USE [eFundraisingProd]
GO
/****** Object:  Table [dbo].[perfom_trace]    Script Date: 02/14/2014 16:28:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[perfom_trace](
	[RowNumber] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,
	[EventClass] [int] NULL,
	[TextData] [ntext] NULL,
	[NTUserName] [nvarchar](128) NULL,
	[HostName] [nvarchar](128) NULL,
	[ApplicationName] [nvarchar](128) NULL,
	[LoginName] [nvarchar](128) NULL,
	[SPID] [int] NULL,
	[Duration] [bigint] NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[CPU] [int] NULL,
	[EventSubClass] [int] NULL,
	[IntegerData] [int] NULL,
	[Error] [int] NULL,
	[ObjectName] [nvarchar](128) NULL,
	[DatabaseName] [nvarchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[RowNumber] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'Build', @value=760 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'perfom_trace'
GO
EXEC sys.sp_addextendedproperty @name=N'MajorVer', @value=8 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'perfom_trace'
GO
EXEC sys.sp_addextendedproperty @name=N'MinorVer', @value=0 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'perfom_trace'
GO
