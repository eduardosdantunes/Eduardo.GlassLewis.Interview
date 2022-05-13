CREATE DATABASE [InterviewDb]
GO

USE [InterviewDb]
GO

/****** Object:  Table [dbo].[Companies]    Script Date: 13/05/2022 18:07:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Companies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Exchange] [nvarchar](max) NOT NULL,
	[Ticker] [nvarchar](max) NOT NULL,
	[Isin_Value] [nvarchar](450) NOT NULL,
	[WebSite] [nvarchar](max) NULL,
 CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

INSERT INTO [dbo].[Companies]
           ([Isin_Value]
           ,[Name]
		   ,[Exchange]
           ,[Ticker]           
           ,[WebSite])
     VALUES
           ('US0378331005', 'Apple Inc.', 'NASDAQ', 'AAPL', 'http://www.apple.com'),
		   ('US1104193065', 'British Airways Plc', 'Pink Sheets', 'BAIRY', null),
		   ('NL0000009165', 'Heineken NV', 'Euronext Amsterdam', 'HEIA', null),
		   ('JP3866800000', 'Panasonic Corp', 'Tokyo Stock Exchange', '6752', 'http://www.panasonic.co.jp'),
		   ('DE000PAH0038', 'Porsche Automobil', 'Deutsche Börse', 'PAH3', 'https://www.porsche.com/');
GO