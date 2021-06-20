SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Client](
	[ClientId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](600) NOT NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[Email] [nvarchar](100) NULL,
	[ModifiedBy] [nvarchar](100) NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED
([ClientId] ASC)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF,
ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF)
ON [PRIMARY]
) ON [PRIMARY]
GO

--These commands insert a sample
SET IDENTITY_INSERT [dbo].[Client] ON
GO

INSERT [dbo].[Client] ([ClientId], [Name], [Address], [PhoneNumber], [Email],
[ModifiedBy], [ModifiedOn])
VALUES (1, N'John Smith', N'1st Street', NULL, N'john@provider.com', N'Lucas Moraes', CURRENT_TIMESTAMP)
GO

SET IDENTITY_INSERT [dbo].[Client] OFF
GO