CREATE TABLE [dbo].[Bookings]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [BookId] INT NOT NULL, 
    [UserId] INT NOT NULL, 
    [BookedDate] DATETIME2 NOT NULL,
    CONSTRAINT [FK_Bookings_ToBooks] FOREIGN KEY ([BookId]) REFERENCES [Books]([Id]),
    CONSTRAINT [FK_Bookings_User] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]) 
)
