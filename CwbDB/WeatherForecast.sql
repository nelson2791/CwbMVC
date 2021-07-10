CREATE TABLE [dbo].[WeatherForecast]
(
	[seqNo] [bigint] IDENTITY(1,1) NOT NULL,
	[datasetDescription] [nvarchar](50) NOT NULL,
	[locationName] [nvarchar](10) NOT NULL,
	[elementName] [varchar](20) NOT NULL,
	[startTime] [datetime] NOT NULL,
	[endTime] [datetime] NOT NULL,
	[parameterName] [nvarchar](30) NOT NULL,
	[parameterValue] [nvarchar](10) NULL,
	[parameterUnit] [nvarchar](30) NULL,
	[createTime] [datetime] NOT NULL,
 CONSTRAINT [PK_WeatherForecast] PRIMARY KEY CLUSTERED 
(
	[seqNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
