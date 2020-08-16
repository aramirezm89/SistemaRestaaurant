CREATE DATABASE SistemaRestaurant
-------------------------------------------------------------------------------------
--tabla que regstra usuarios con acceso al sistema
-------------------------------------------------------------------------------------

CREATE TABLE TBL_RestUsuarios(

id int identity (1,1) primary key not null,
usuario varchar (20) not null,
pass varchar (20) not null,
nombres varchar (100) not null,
apellidos varchar (100) not null,
tipo varchar (15) not null
)

go

-------------------------------------------------------------------------------------
-- tabla que registra los productos ofrecidos por el restaurant
-------------------------------------------------------------------------------------

CREATE TABLE TBL_RestProductos(

id int identity (1,1) primary key not null,
producto varchar (50) not null,
descripcion varchar (150),
precio int 
)
select * from  TBL_RestProductos
---------------------------------------------------------------------------------------
-- Procedimeinto almacenado que inserta productos
----------------------------------------------------------------------------------------
CREATE PROCEDURE Rest_InsertaProducto_SP
@in_producto varchar (50),
@in_descripcion varchar (150),
@in_precio decimal (9,2)
AS
BEGIN
INSERT INTO TBL_RestProductos(producto,descripcion,precio)
values (@in_producto,@in_descripcion,@in_precio)
END

exec Rest_InsertaProducto_SP 'gsg','fafa',23
Select * from TBL_RestProductos
------------------------------------------------------------------------------
--Procedimeinto almacenado que crea usuarios
------------------------------------------------------------------------------
CREATE PROCEDURE Rest_CreaUsuarios_SP
@in_usuario varchar (20) ,
@in_pass varchar (20),
@in_nombres varchar (100),
@in_apellidos varchar (100),
@in_tipo varchar (15) 
AS
BEGIN
INSERT INTO TBL_RestUsuarios (usuario,pass,nombres,apellidos,tipo)
values (@in_usuario,@in_pass,@in_nombres,@in_apellidos,@in_tipo)
END

EXEC Rest_CreaUsuarios_SP 'aramirezm','ramirez1989','Antonio Isaac','Ramirez Monsalve','administrador'
EXEC Rest_CreaUsuarios_SP 'cramirez','cristal01','Cristal Anais','Ramirez Monsalve','usuario'

select * from TBL_RestUsuarios
------------------------------------------------------------------------
--Procedimiento almacenado que busca usuarios
------------------------------------------------------------------------
go
CREATE PROCEDURE Rest_BuscaUsuario_SP
@in_usuario varchar (20) ,
@in_pass varchar (20)
AS
BEGIN
SELECT id,SUBSTRING(nombres,1,CHARINDEX((' '),nombres,1)-1),usuario , pass, tipo from TBL_RestUsuarios where usuario=@in_usuario and pass = @in_pass
END

exec Rest_BuscaUsuario_SP 'aramirezm','ramirez1989'

---------------------------------------------------------------------
--Procedimiento alamcenado que busca productos
---------------------------------------------------------------------

CREATE PROCEDURE Rest_BuscaProducto_SP
@in_id int
AS
BEGIN
  select id,producto,descripcion,precio  from TBL_RestProductos where id = @in_id
END

-----------------------------------------------------------------------------------------
-- Procedimiento almacenado que Modifica productos
-------------------------------------------------------------------------------------------

CREATE PROCEDURE Rest_ModificaProducto_SP
@in_id int,
@in_producto varchar (50),
@in_descripcion varchar (150),
@in_precio int
AS 
BEGIN
UPDATE TBL_RestProductos set producto = @in_producto, descripcion  = @in_descripcion, precio = @in_precio 
where id = @in_id
END

select * from TBL_RestProductos

---------------------------------------------------------------------------------------------------
--Procedimiento almacenado que elimina producto
--------------------------------------------------------------------------------------------------

CREATE PROCEDURE Rest_EliminaProducto_SP
@in_id int
AS
BEGIN
DELETE FROM TBL_RestProductos WHERE id = @in_id
END