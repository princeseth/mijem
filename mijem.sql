CREATE database mijem;

USE [mijem]
GO


-- Includes reservation details
CREATE TABLE [reservation] ( 
    [reservation_id] INT IDENTITY (1, 1) NOT NULL, 
    [description] [varchar](max) NULL,
	[reservation_date] [datetime] NULL,
	[contact_id] INT FOREIGN KEY REFERENCES contact(contact_id) NOT NULL,
    PRIMARY KEY ([reservation_id] ASC) 
); 

--Includes contact details
CREATE TABLE [contact] ( 
    [contact_id] INT IDENTITY (1, 1) NOT NULL, 
    [name] NVARCHAR(30) NOT NULL,
	[phone] VARCHAR(16),
	[birth_date] DATE NOT NULL,	
	[contact_type_id] INT FOREIGN KEY REFERENCES contact_type(contact_type_id) NOT NULL,
    PRIMARY KEY ([contact_id] ASC) 
); 

--Type of contacts
CREATE TABLE [contact_type] ( 
    [contact_type_id] INT IDENTITY (1, 1) NOT NULL, 
    [type_of_contact] VARCHAR(30) NOT NULL, 
    PRIMARY KEY ([contact_type_id] ASC) 
); 


--Populate Contact Type table

INSERT INTO [dbo].[contact_type] ([type_of_contact])  VALUES ('Contact Type 1');
INSERT INTO [dbo].[contact_type] ([type_of_contact])  VALUES ('Contact Type 2');
INSERT INTO [dbo].[contact_type] ([type_of_contact])  VALUES ('Contact Type 3');


--Stored Procedure for Contact Search
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--SearchContact 
CREATE PROCEDURE [dbo].[SearchContact]
(
    @name        varchar(30)
)
AS
BEGIN
    SELECT c.name,c.phone,c.birth_date,c.contact_id,ct.type_of_contact, ct.contact_type_id AS contact_type_id, c.contact_type_id AS contact_type_id1 FROM contact c JOIN contact_type ct ON c.contact_type_id = ct.contact_type_id 
     WHERE c.name LIKE '%'+ @name + '%'

END

GO
