USE [Employee]
GO

/****** Object:  Table [dbo].[EmpFileDetails]    Script Date: 8/19/2021 2:52:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[EmpFileDetails](
	[EmpId] [int] NOT NULL,
	[FilepathEmirates] [varchar](max) NOT NULL,
	[FilepathPassport] [varchar](max) NOT NULL,
	[FilepathCertificate] [varchar](max) NOT NULL,
	[FilepathResume] [varchar](max) NOT NULL,
	[emirates] [varchar](max) NOT NULL,
	[passport] [varchar](max) NOT NULL,
	[certificate] [varchar](max) NOT NULL,
	[resume] [varchar](max) NOT NULL,
 CONSTRAINT [PK_EmpFileDetails] PRIMARY KEY CLUSTERED 
(
	[EmpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

