--Si existe la base de datos
IF EXISTS(SELECT * FROM master.dbo.SYSDATABASES WHERE NAME = 'pruebaImagen')
BEGIN
	--Avisamos que existe
    PRINT('Ya existe esta base de datos.')
--fin si no 
END ELSE BEGIN
	--la creamos
    CREATE DATABASE pruebaImagen;
END;

CREATE TABLE empleado
(
idEmpleado int,
nombreEmpleado varchar(10),
fotoEmpleado image
);