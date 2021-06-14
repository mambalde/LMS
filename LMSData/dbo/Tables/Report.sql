CREATE TABLE [dbo].[Report]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserId] INT NOT NULL, 
    [StaffId] NVARCHAR(128) NOT NULL, 
    [BookId] INT NOT NULL, 
    [ReturnedState] NVARCHAR(50) NOT NULL, 
    [BookedDate] DATETIME2 NOT NULL, 
    [ReturnDate] DATETIME2 NOT NULL, 
    [BookedState] NVARCHAR(50) NULL,
    [ReportedDate] DATETIME2 NOT NULL, 
    CONSTRAINT [FK_Report_ToUser] FOREIGN KEY ([UserId]) REFERENCES [Users]([Id]),
    CONSTRAINT [FK_Report_ToStaff] FOREIGN KEY ([StaffId]) REFERENCES [Staff]([StaffId]),
    CONSTRAINT [FK_Report_ToBooks] FOREIGN KEY ([BookId]) REFERENCES [Books]([Id])

)
