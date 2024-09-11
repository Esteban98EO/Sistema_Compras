create database SistemaCompras

go

use SistemaCompras

go

create table rol (
idrol integer primary key identity,
nombre varchar(30) not null,
descripcion varchar(255) null,
estado bit default(1)
);

go

create table categoria (
idcategoria integer primary key identity,
nombre varchar(50) not null unique,
descripcion varchar(255) null,
estado bit default(1)
);
go
create table articulo (
idarticulo integer primary key identity,
id_categoria integer,
codigo varchar(50) null,
nombre varchar(100) not null unique,
precio_venta decimal(11,2) not null,
stock integer not null,
descripcion varchar(255) null,
imagen varchar(20) null,
estado bit default(1),
FOREIGN KEY (id_categoria) REFERENCES categoria(idcategoria)
);
go

create table PersonaProveedor (
idpersona integer primary key identity,
tipo_persona varchar(20) not null,
nombre varchar(100) not null,
tipo_documento varchar(20) null,
num_documento varchar(20) null,
direccion varchar(70) null,
telefono varchar(20) null,
email varchar(50) null
);

go

create table Usuario (
idusuario integer primary key identity,
idrol integer not null,
nombre varchar(100) not null,
tipo_documento varchar(20) null,
num_documento varchar(20) null,
direccion varchar(70) null,
telefono varchar(20) null,
email varchar(50) not null,
clave varbinary(MAX) not null,
estado bit default(1),
FOREIGN KEY (idrol) REFERENCES rol(idrol)
);
go

create table ingreso (
idingreso integer primary key identity,
idproveedor integer not null,
idusuario integer not null,
tipo_comprobante varchar(20) not null,
serie_comprobante varchar(7) null,
num_comprobante varchar(10) not null,
fecha datetime not null,
impuesto decimal (4,2) not null,
total decimal (11,2) not null,
estado varchar(20) not null,
FOREIGN KEY (idproveedor) REFERENCES PersonaProveedor(idpersona),
FOREIGN KEY (idusuario) REFERENCES usuario(idusuario)
);
go

create table detalle_ingreso (
iddetalle_compra integer primary key identity,
idingreso integer not null,
idarticulo integer not null,
cantidad integer not null,
precio decimal (11,2) not null,
FOREIGN KEY (idingreso) REFERENCES Ingreso (idingreso) ON DELETE CASCADE,
FOREIGN KEY (idarticulo) REFERENCES articulo (idarticulo)
);
go
insert into usuario(idrol, nombre, tipo_documento, num_documento, direccion, telefono, email, clave, estado)
values (1,'Esteban Escorcia', 1, 1, 1, 1, 1,1 ,1);
go


--Proc Almacenado Rol
create proc rol_listar
as 
select idrol, nombre from rol
where estado=1
go

----------------------------------------------------------------------------------
insert into rol (nombre) values ('Administrador');
insert into rol (nombre) values ('Gerente');
insert into rol (nombre) values ('Gestor Tecnico');
-------------------------------------------------------------------------------------
--Proc Almacenados Usuario
create proc usuario_listar
as
select u.idusuario as ID, u.idrol, r.nombre as Rol, u.nombre as Nombre,
u.tipo_documento as Tipo_Documento, u.num_documento as Num_Documento,
u.direccion as Direccion, u.telefono as Telefono, u.email as Email,
u.estado as Estado 
from Usuario u inner join rol r on u.idrol=r.idrol
order by u.idusuario desc
go

--PROCEDIMIENTO BUSCAR
create proc usuario_buscar
@valor varchar(50)
as
select u.idusuario as ID, u.idrol, r.nombre as Rol, u.nombre as Nombre,
u.tipo_documento as Tipo_Documento, u.num_documento as Num_Documento,
u.estado as Estado 
from Usuario u inner join rol r on u.idrol=r.idrol
where u.nombre like '%' + @valor + '%' Or u.email like '%' + @valor + '%'
order by u.nombre asc
go
--PROCEDIMIENTO INSERTAR
create proc usuario_insertar
@idrol integer,
@nombre varchar(100),
@tipo_documento varchar(20),
@num_documento varchar(20),
@direccion varchar(70),
@telefono varchar(20),
@email varchar(50),
@clave varchar(50)
as
insert into Usuario(idrol,nombre,tipo_documento,num_documento,direccion,telefono,email,clave)
values (@idrol,@nombre,@tipo_documento,@num_documento,@direccion,@telefono,@email,HASHBYTES('SHA2_256', @clave))
go

--PROCEDIMIENTO ACTUALIZAR
create proc usuario_actualizar
@idusuario integer,
@idrol integer,
@nombre varchar(100),
@tipo_documento varchar(20),
@num_documento varchar(20),
@direccion varchar(70),
@telefono varchar(20),
@email varchar(50),
@clave varchar(50)
as
if @clave<>''
update Usuario set idrol=@idrol, nombre=@nombre, tipo_documento=@tipo_documento,
num_documento=@num_documento, direccion=@direccion, telefono=@telefono,
email=@email, clave=HASHBYTES ('SHA2_256', @clave)
where idusuario= @idusuario;
else
update Usuario set idrol=@idrol, nombre=@nombre, tipo_documento=@tipo_documento,
num_documento=@num_documento, direccion=@direccion, telefono=@telefono,
email=@email
where idusuario= @idusuario;
go
--PROCEDIMIENTO ELIMINAR
create proc usuario_eliminar
@idusuario integer
as
delete from Usuario
where idusuario=@idusuario
go
--PROCEDIMIENTO DESACTIVAR
create proc usuario_desactivar
@idusuario integer
as
update Usuario set estado=0
where idusuario=@idusuario
go
--PROCEDIMIENTO ACTIVAR
create proc usuario_activar
@idusuario integer
as
update Usuario set estado=1
where idusuario=@idusuario
go
--PROCEDIMIENTO EXISTE
create proc usuario_existe
@valor varchar(100),
@existe bit output
as
   if exists (select email from Usuario where email = ltrim(rtrim(@valor)))
       begin
        set @existe=1
       end
   else
       begin
         set @existe=0
       end
    go

--	usuario login
create proc usuario_login
@email varchar(50),
@clave varchar(50)
as 
select u.idusuario, u.idrol, r.nombre as rol, u.nombre, u.estado
from Usuario u inner join rol r on u.idrol=r.idrol
where u.email=@email and u.clave=HASHBYTES('SHA2_256',@clave)
go


------------------------------------------------------------------------------------------
--Proc Almacenados Categoria  
create proc categoria_listar
as
select idcategoria as ID, nombre as Nombre, descripcion as Descripcion, estado as Estado
from categoria
order by idcategoria desc
go

--procedimiento buscar
create proc categoria_buscar
@valor varchar (50)
as
select idcategoria as ID, nombre as Nombre, descripcion as Descripcion, estado as Estado
from categoria
where nombre like '%' + @valor + '%' or descripcion like '%' + @valor + '%'
order by nombre asc
go

--procedimiento insertar
create proc categoria_insertar
@nombre varchar(50),
@descripcion varchar(255)
as
insert into categoria (nombre, descripcion) values (@nombre, @descripcion)
go
 
--procedimiento actualizar
create proc categoria_actualizar
@idcategoria int,
@nombre varchar(50),
@descripcion varchar(255)
as 
update categoria set nombre= @nombre, descripcion= @descripcion
where idcategoria= @idcategoria
go

--procedimiento eliminar
create proc categoria_eliminar
@idcategoria int
as
delete from categoria
where idcategoria= @idcategoria
go

--procedimiento desactivar
create proc categoria_desactivar
@idcategoria int
as
update categoria set estado=0
where idcategoria= @idcategoria
go

--procedimiento activar
create proc categoria_activar
@idcategoria int
as
update categoria set estado=1
where idcategoria= @idcategoria
go

--procedimiento existe--
 create proc categoria_existe
 @valor varchar (100),
 @existe bit output
 as
 if exists (select nombre from categoria where nombre= ltrim(rtrim(@valor)))
 begin
 set @existe=1
 end
 else
 begin
 set @existe=0
 end



 -----------------------------------------------------
 --Ejecutamos 
 
--Proc Almacenasdos Articulo Listar
create proc articulo_listar
as
select a.idarticulo as ID,a.id_categoria, c.nombre as Categoria,
a.codigo as Codigo, a.nombre as Nombre, a.precio_venta as Precio_Venta,
a.stock as Stock, a.descripcion as Descripcion, a.imagen as Imagen,
a.estado as Estado
from articulo a inner join categoria c on a.id_categoria=c.idcategoria
order by a.idarticulo desc
go

--Procedimiento Buscar
create proc articulo_buscar
@valor varchar(50)
as
select a.idarticulo as ID,a.id_categoria, c.nombre as Categoria,
a.codigo as Codigo, a.nombre as Nombre, a.precio_venta as Precio_Venta,
a.stock as Stock, a.descripcion as Descripcion, a.imagen as Imagen,
a.estado as Estado
from articulo a inner join categoria c on a.id_categoria=c.idcategoria
where a.nombre like '%' + @valor + '%' Or a.descripcion like '%' + @valor + '%'
order by a.nombre asc
go

--Procedimiento Insertar
create proc articulo_insertar
@idcategoria integer,
@codigo varchar(50),
@nombre varchar(100),
@precio_venta decimal(11,2),
@stock integer,
@descripcion varchar(255),
@imagen varchar(20)
as
insert into articulo(id_categoria, codigo, nombre, precio_venta, stock, descripcion, imagen)
values (@idcategoria, @codigo, @nombre, @precio_venta, @stock, @descripcion, @imagen)
go

--Procedimiento Actualizar
create proc articulo_actualizar
@idarticulo integer,
@idcategoria integer,
@codigo varchar(50),
@nombre varchar(100),
@precio_venta decimal(11,2),
@stock integer,
@descripcion varchar(255),
@imagen varchar(20)
as
update articulo set id_categoria = @idcategoria, codigo = @codigo, 
nombre = @nombre, precio_venta = @precio_venta, stock = @stock,
descripcion = @descripcion, imagen = @imagen
where idarticulo=@idarticulo
go

--Procedimiento Eliminar 
create proc articulo_eliminar
@idarticulo integer
as
delete from articulo
where idarticulo = @idarticulo
go

--Procedimiento Desactivar
create proc articulo_desactivar
@idarticulo integer
as
update articulo set estado=0
where idarticulo=@idarticulo
go

--Procedimiento Activar
create proc articulo_activar
@idarticulo integer
as
update articulo set estado=1
where idarticulo=@idarticulo
go 

--Procedimiento existe
create proc articulo_existe
@valor varchar(100),
@existe bit output
as
if exists (select nombre from articulo where nombre = ltrim(rtrim(@valor)))
 begin
 set @existe=1
 end
 else
 begin
 set @existe=0
 end
 go

 --Procedimiento seleccionar
 create proc categoria_seleccionar
 as
 select idcategoria, nombre from categoria
 where estado=1
 go

 --------------------------------
 --procedimiento articulo buscar codigo
create proc articulo_buscar_codigo
@valor varchar(50)
as
select idarticulo, codigo, nombre, precio_venta, stock
from articulo
where codigo=@valor
go
--------------------------------------
--Procedimiento Listar
create proc persona_listar
as
select idpersona as ID, tipo_persona as Tipo_Persona, nombre as Nombre,
tipo_documento as Tipo_Documento, num_documento as Num_Documento,
direccion as Direccion, telefono as Telefono, email as Email
from PersonaProveedor
order by idpersona desc
go

--Procedimiento Listar Proveedores
create proc persona_listar_proveedores
as
select idpersona as ID, tipo_persona as Tipo_Persona, nombre as Nombre,
tipo_documento as Tipo_Documento, num_documento as Num_Documento,
direccion as Direccion, telefono as Telefono, email as Email
from PersonaProveedor
where tipo_persona = 'Proveedor'
order by idpersona desc
go

--Procedimineto Listar Clientes
create proc persona_listar_clientes
as
select idpersona as ID, tipo_persona as Tipo_Persona, nombre as Nombre,
tipo_documento as Tipo_Documento, num_documento as Num_Documento,
direccion as Direccion, telefono as Telefono, email as Email
from PersonaProveedor
where tipo_persona = 'Cliente'
order by idpersona desc
go

--Procedimiento Buscar
create proc persona_buscar
@valor varchar(50)
as
select idpersona as ID, tipo_persona as Tipo_Persona, nombre as Nombre,
tipo_documento as Tipo_Documento, num_documento as Num_Documento,
direccion as Direccion, telefono as Telefono, email as Email
from PersonaProveedor
where nombre like '%' + @valor + '%' Or email like '%' + @valor + '%'
order by nombre asc
go

--Procedimiento Buscar Proveedores
create proc persona_buscar_proveedores
@valor varchar(50)
as
select idpersona as ID, tipo_persona as Tipo_Persona, nombre as Nombre,
tipo_documento as Tipo_Documento, num_documento as Num_Documento,
direccion as Direccion, telefono as Telefono, email as Email
from PersonaProveedor
where (nombre like '%' + @valor + '%' Or email like '%' + @valor + '%')
and tipo_persona = 'Proveedor'
order by nombre asc
go

--Procedimiento Buscar Clientes
create proc persona_buscar_clientes
@valor varchar(50)
as
select idpersona as ID, tipo_persona as Tipo_Persona, nombre as Nombre,
tipo_documento as Tipo_Documento, num_documento as Num_Documento,
direccion as Direccion, telefono as Telefono, email as Email
from PersonaProveedor
where (nombre like '%' + @valor + '%' Or email like '%' + @valor + '%')
and tipo_persona = 'Clientes'
order by nombre asc
go

--Procedimiento Insertar
create proc persona_insertar
@tipo_persona varchar(20),
@nombre varchar(100),
@tipo_documento varchar(20),
@num_documento varchar(20),
@direccion varchar(70),
@telefono varchar(20),
@email varchar(50)
as
insert into PersonaProveedor (tipo_persona, nombre, tipo_documento, num_documento, direccion, telefono, email)
values (@tipo_persona, @nombre, @tipo_documento, @num_documento, @direccion, @telefono, @email)
go

--Procedimiento Actualizar
create proc persona_actualizar
@idpersona integer,
@tipo_persona varchar(20),
@nombre varchar(100),
@tipo_documento varchar(20),
@num_documento varchar(20),
@direccion varchar(70),
@telefono varchar(20),
@email varchar(50)
as
update PersonaProveedor set tipo_persona=@tipo_persona, nombre=@nombre,
tipo_documento=@tipo_documento, num_documento=@num_documento, direccion=@direccion,
telefono=@telefono, email=@email
where idpersona=@idpersona
go

--Procedimiento Eliminar
create proc persona_eliminar
@idpersona integer
as
delete from PersonaProveedor
where idpersona=@idpersona
go

--Procedimiento Existe
create proc persona_existe
@valor varchar(100),
@existe bit output
as
if exists (select nombre from PersonaProveedor where nombre = ltrim(rtrim(@valor)))
    begin
	    set @existe = 1
    end
else
    begin
	    set @existe = 0
	end
go

-----------------------------------------------------------

--Procedimiento listar
create proc ingreso_lista
as
select i.idingreso as ID, i.idusuario, u.nombre as Usuario, p.nombre as Proveedor,
i.tipo_comprobante as Tipo_Comprobante, i.serie_comprobante as Serie,
i.num_comprobante as Numero, i.fecha as Fecha, i.impuesto Impuesto,
i.total as Total, i.estado as Estado
from ingreso i inner join usuario u on i.idusuario=u.idusuario
inner join PersonaProveedor p on i.idproveedor=p.idpersona
order by i.idingreso desc
go

--Procedimiento buscar
create proc ingreso_buscar
@valor varchar(50)
as
select i.idingreso as ID, i.idusuario, u.nombre as Usuario, p.nombre as Proveedor,
i.tipo_comprobante as Tipo_Comprobante, i.serie_comprobante as Serie,
i.num_comprobante as Numero, i.fecha as Fecha, i.impuesto Impuesto,
i.total as Total, i.estado as Estado
from ingreso i inner join usuario u on i.idusuario=u.idusuario
inner join PersonaProveedor p on i.idproveedor=p.idpersona
where i.num_comprobante like '%' + @valor + '%' Or p.nombre like '%' + @valor + '%'
order by i.fecha asc
go

--Procedimiento anular
create proc compras_anular
@idcompras int
as
update compras set estado = 'Anulado'
where idcompras=@idcompras
go

--Tipo de dato detalle ingreso
create type type_detalle_ingreso as table
(
    idarticulo int,
	codigo varchar(50),
	articulo varchar(100),
	cantidad int,
	precio decimal(11,2),
	importe decimal(11,2)
);
go
--PROCEDIMIENTO INGRESO LISTAR DETALLE
create proc ingreso_listar_detalle
@idingreso int 
as 
select d.idarticulo as ID, a.codigo as CODIGO, a.nombre as ARTICULO,
d.cantidad as CANTIDAD, d.precio as PRECIO, (d.precio*d.cantidad) as IMPORTE
from detalle_ingreso d inner join articulo a on d.idarticulo=a.idarticulo
where d.idingreso=@idingreso
go
---------------------------------------------------------
ALTER proc [dbo].[ingreso_anular]
@idingreso int
as
update ingreso set estado = 'Anulado'
where idingreso=@idingreso;
update articulo
set stock=stock-d.cantidad
from articulo a
inner join
(select idarticulo, cantidad from detalle_ingreso where idingreso=@idingreso) as d
on a.idarticulo=d.idarticulo;
--Procedimiento insertar
create proc ingreso_insertar

@idusuario int,
@idproveedor int,
@tipo_comprobante varchar(20),
@serie_comprobante varchar(7),
@num_comprobante varchar(10),
@impuesto decimal(4,2),
@total decimal(11,2),
@detalle type_detalle_ingreso READONLY
as
begin
    --Insertamos la cabecera
    insert into ingreso (idproveedor, idusuario, tipo_comprobante, serie_comprobante,
	num_comprobante, fecha, impuesto, total, estado)
	values (@idproveedor, @idusuario, @tipo_comprobante, @serie_comprobante, 
	@num_comprobante, getdate(), @impuesto, @total, 'Aceptado')
	
	--Insertar los detalles
	insert detalle_ingreso (idingreso, idarticulo, cantidad, precio)
	select @@IDENTITY,  d.idarticulo, d.cantidad, d.precio
	from @detalle d;
end
go
