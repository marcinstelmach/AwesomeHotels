CREATE TABLE [dbo].[Users]
(
	[Id] [bigint] NOT NULL,
	[EmailAddress] [nvarchar](256) NOT NULL,
	[FirstName] [nvarchar](70) NOT NULL,
	[LastName] [nvarchar](70) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[PasswordHash] [nvarchar](1024) NOT NULL,
	[SecurityStamp] [nvarchar](1024) NOT NULL,
	[CreationDateTime] [datetimeoffset](7) NOT NULL,
	[IsDeleted] [bit] NOT NULL DEFAULT 0

	CONSTRAINT [PK_Users_Id] PRIMARY KEY CLUSTERED ([Id]),
	CONSTRAINT [UK_Users_Email] UNIQUE NONCLUSTERED ([EmailAddress])
);