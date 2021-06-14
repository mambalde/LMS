CREATE TABLE [dbo].[Books]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Price] MONEY NOT NULL, 
    [Category] NVARCHAR(50) NOT NULL, 
    [ISBN] NVARCHAR(256) NOT NULL, 
    [PublisherId] INT NOT NULL, 
    [State] NVARCHAR(50) NOT NULL, 
    [Availabilty] BIT NOT NULL DEFAULT 1, 
    [BookImage] IMAGE NULL, 
    CONSTRAINT [FK_Book_ToPublisher] FOREIGN KEY ([PublisherId]) REFERENCES [Publisher]([Id]) 
)
