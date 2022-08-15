CREATE TABLE [dbo].[Guest]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1) , 
	[GuestName] nvarchar(20) Not Null UNIQUE ,
	[PasswordHash] nvarchar(250) Not Null ,
	[RefreshToken] nvarchar(250)  Null ,
	[RefreshTokenExpirationDate] datetime2 NULL ,
	[CreationDate] datetime2 not null
)
