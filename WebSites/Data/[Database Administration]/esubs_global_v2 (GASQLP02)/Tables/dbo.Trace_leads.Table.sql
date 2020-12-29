USE [esubs_global_v2]
GO
/****** Object:  Table [dbo].[Trace_leads]    Script Date: 02/14/2014 16:26:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trace_leads](
	[RowNumber] [int] IDENTITY(1,1) NOT NULL,
	[EventClass] [int] NULL,
	[TextData] [ntext] NULL,
	[DatabaseID] [int] NULL,
	[NTUserName] [nvarchar](128) NULL,
	[NTDomainName] [nvarchar](128) NULL,
	[ClientProcessID] [int] NULL,
	[ApplicationName] [nvarchar](128) NULL,
	[LoginName] [nvarchar](128) NULL,
	[SPID] [int] NULL,
	[Duration] [bigint] NULL,
	[StartTime] [datetime] NULL,
	[Reads] [bigint] NULL,
	[Writes] [bigint] NULL,
	[CPU] [int] NULL,
	[ObjectID] [int] NULL,
	[Success] [int] NULL,
	[ServerName] [nvarchar](128) NULL,
	[ObjectType] [int] NULL,
	[State] [int] NULL,
	[ObjectName] [nvarchar](128) NULL,
	[DatabaseName] [nvarchar](128) NULL,
	[DBUserName] [nvarchar](128) NULL,
PRIMARY KEY CLUSTERED 
(
	[RowNumber] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 95) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'Build', @value=760 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Trace_leads'
GO
EXEC sys.sp_addextendedproperty @name=N'MajorVer', @value=8 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Trace_leads'
GO
EXEC sys.sp_addextendedproperty @name=N'MinorVer', @value=0 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Trace_leads'
GO
