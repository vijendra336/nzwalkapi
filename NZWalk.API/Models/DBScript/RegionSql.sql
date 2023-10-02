USE [NZWalksDb]
GO

INSERT INTO [dbo].[Regions]
           ([Id]
           ,[Code]
           ,[Name]
           ,[RegionImageUrl])
     VALUES
           (newid()
           ,'AKL'
           ,'Auckland'
           ,'auckland.jpg')
GO


select * from  Regions
