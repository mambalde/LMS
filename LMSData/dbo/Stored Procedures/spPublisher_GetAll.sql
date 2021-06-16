CREATE PROCEDURE [dbo].[spPublisher_GetAll]
As
begin
set nocount on;
	 select [Id], [Name]
	 from dbo.Publisher
end

