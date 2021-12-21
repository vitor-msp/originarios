create database Originarios;
go
use Originarios;
go
create table Usuario (
  id_usu int identity(1,1),
  nome nvarchar(50) not null,
  dt_nasc date not null,
  email varchar(30) not null,
  constraint pk_usu primary key (id_usu)
);
go
create table Postagem (
  id_post int identity(1,1),
  usuario int not null,
  titulo varchar(30) not null,
  descricao varchar(100) not null,
  corpo varchar(5000),
  nm_img1 varchar(20),
  vb_img1 varbinary(max),
  nm_img2 varchar(20),
  vb_img2 varbinary(max),
  nm_img3 varchar(20),
  vb_img3 varbinary(max),
  nm_img4 varchar(20),
  vb_img4 varbinary(max),
  constraint pk_post primary key (id_post),
  constraint fk_post_usu foreign key (usuario) references Usuario (id_usu)
);
go