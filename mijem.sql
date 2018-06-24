USE [mijem]
GO

/****** Object:  StoredProcedure [dbo].[SearchContact]    Script Date: 22-Jun-18 4:47:52 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--SearchContact '', ''
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
