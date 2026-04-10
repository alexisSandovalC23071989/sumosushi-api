INSERT INTO TipoEntidades (IdTipoEntidad, Descripcion) VALUES
(1, 'Estado'),
(2, 'Canton'),
(3, 'Distrito'),
(4, 'Día'),
(5, 'Rol'),
(6, 'TipoPedido'),
(7, 'MetodoPago'),
(8, 'GestionPuntos'),
(9, 'Restaurante'),
(10, 'HorarioRestaurante'),
(11, 'Usuario'),
(12, 'Direccion'),
(13, 'TelefonoUsuario'),
(14, 'RefreshTokens'),
(15, "TokenRecuperacionContrasenna"),
(16, 'CategoriaProductos')
(17, 'Promocion')
(18, 'Producto')
(19, 'CalificacionProducto'),
(20, 'Etiquetas'),
(21, 'EtiquetasProductos'),
(22, 'Presentaciones'),
(23, 'PresentacionesProducto'),
(24, 'Carrito'),
(25, 'Pedido');

INSERT INTO Dias (IdDia, Nombre, IdTipoEntidad) VALUES
(1, 'Lunes', 4),
(2, 'Martes', 4),
(3, 'Miércoles', 4),
(4, 'Jueves', 4),
(5, 'Viernes', 4),
(6, 'Sábado', 4),
(7, 'Domingo', 4);


INSERT INTO Estados (IdEstado, Descripcion, IdTipoEntidad) VALUES
(1, 'Activo', 1),
(2, 'Inactivo', 1);
(3, 'Agotado', 1);

INSERT INTO Roles (IdRol, Descripcion, IdTipoEntidad) VALUES
(1, 'Usuario', 5),
(2, 'Admin', 5),
(3, 'Colaborador', 5);

INSERT INTO Restaurante (
  Nombre,
  Direccion,
  Telefono,
  Correo,
  LogoUrl,
  IdTipoEntidad
)
VALUES (
  'Sumo Sushi',
  'Heredia, San Rafael, 50m al oeste del cajero del BCR',
  '2222-3333',
  'contacto@sumosushi.com',
  'https://scontent.fsjo14-1.fna.fbcdn.net/v/t39.30808-6/306543868_626445078912412_5459378620350339078_n.png?_nc_cat=100&ccb=1-7&_nc_sid=6ee11a&_nc_ohc=jOhGD1k_MDIQ7kNvwHvgLRW&_nc_oc=AdkvvZT-5ynLggar_mdGMfDi0IHu4hiagyxH5QIz4TNfAAAzCutfitLpYh4lwIccgWQ&_nc_zt=23&_nc_ht=scontent.fsjo14-1.fna&_nc_gid=Gqj8w2p37RjPxVNbK1vptA&oh=00_AfOQ5Znal_EW1HbfueIrpn2h2Qu3M0ikvUsiLcEfDPWJ8w&oe=684E3ABC',
  9
);

INSERT INTO Presentaciones(
	IdPresentacion,
    Descripcion,
    IdTipoEntidad
)VALUES
(1, "Mediano", 22),
(2, "Grande", 22);




-- DATOS QUE NO SE INICIALIZAN CON LA BD
INSERT INTO Etiquetas (IdEtiqueta, Descripcion, IdTipoEntidad) VALUES
(1,'Picante', 20),
(2, 'Vegetariano', 20);

INSERT INTO CategoriaProductos (Descripcion, IdTipoEntidad, IdEstado) VALUES
('Rolls y Gyozas',16,1),
('Entradas', 16,1),
('Platos Fuertes', 16,1);

INSERT INTO Productos (Nombre, Descripcion, ImagenUrl, CanjeablePuntos, IdEstado, IdCategoria, IdTipoEntidad) VALUES
('California Roll', 'Roll de cangrejo, aguacate y pepino', 'https://scontent.fsjo14-1.fna.fbcdn.net/v/t39.30808-6/496937053_1337885184435061_8697885579035650842_n.jpg?_nc_cat=100&ccb=1-7&_nc_sid=833d8c&_nc_ohc=0-ikv1YWKMEQ7kNvwErHn7T&_nc_oc=AdlAp7_lTx_v1oFR0CcArimAJ7JeBl8qSpFztFkrgaK7vmjK-RGp4uJehQxQrKrk0Gk&_nc_zt=23&_nc_ht=scontent.fsjo14-1.fna&_nc_gid=YMpEJXHtVV_gnsWkZN96_Q&oh=00_AfPR08x_aSdYeEhOZhXfcoufBGJAXgqRJp7dkYCORBOZ0g&oe=68526075', b'1', 1, 1, 18),
('Gyozas de Pollo', 'Empanaditas japonesas rellenas de pollo', 'https://scontent.fsjo14-1.fna.fbcdn.net/v/t39.30808-6/490862591_1316445783245668_7817595176654552736_n.jpg?stp=cp6_dst-jpg_tt6&_nc_cat=109&ccb=1-7&_nc_sid=833d8c&_nc_ohc=wILcuhD-XqEQ7kNvwF-4YhW&_nc_oc=AdlbKRLFKxsEA6XnFJ0lXOoJa6sFdmC-d9_LR7VdA2faUqX8kO9ARS_tKJK4UClUNg4&_nc_zt=23&_nc_ht=scontent.fsjo14-1.fna&_nc_gid=kisYM76Nyo-2ITgMVRVlHA&oh=00_AfN_xXlToNFTEWSbXD_b8VJ21ZDZBBMZjJzs9FjhmcoIxw&oe=68528773', b'0', 1, 1, 18),
('Edamame', 'Vainas de soya cocidas con sal marina', 'https://scontent.fsjo14-1.fna.fbcdn.net/v/t39.30808-6/491130077_1316445609912352_4660176982070185527_n.jpg?stp=cp6_dst-jpg_tt6&_nc_cat=111&ccb=1-7&_nc_sid=833d8c&_nc_ohc=ySBSkaKrR5QQ7kNvwHFNbMk&_nc_oc=AdmFNvQs6I5xMp6-2ivyM2PjlDhGZDXbZ7KY5AhD3_vKnDYQEw3IXsQWcuZyH6_csqk&_nc_zt=23&_nc_ht=scontent.fsjo14-1.fna&_nc_gid=EXJWkvca1yF5j1mQBUXvTg&oh=00_AfN6F7Wi5qnmxbCOygCw2q4AzRD5r2hs8SGE2J9-ELFngA&oe=685263F2', b'0', 1, 2, 18),
('Ramen Picante', 'Sopa japonesa con cerdo, huevo y picante', 'https://scontent.fsjo14-1.fna.fbcdn.net/v/t39.30808-6/491959201_1316445369912376_1220383222522078959_n.jpg?stp=cp6_dst-jpg_tt6&_nc_cat=102&ccb=1-7&_nc_sid=833d8c&_nc_ohc=-Y0QJlL8pmIQ7kNvwGdS0Mw&_nc_oc=AdkFEfJt8Ukcp7gGiFJyD2Y1GGqC-YJ_VSg0LQn7n-8yDENzwDurtj6RNvsJ68608oU&_nc_zt=23&_nc_ht=scontent.fsjo14-1.fna&_nc_gid=Ax8ho9qOWJaRQbtSvcdyCQ&oh=00_AfOjdC-zj4KN5NwldV-n4C5f0As9sDNuGXwbgwBZq121Zg&oe=6852758A', b'1', 1, 3, 18),
('Curry Vegetariano', 'Plato de curry japonés con vegetales', 'https://scontent.fsjo14-1.fna.fbcdn.net/v/t39.30808-6/492405788_1316445586579021_5626993404511501136_n.jpg?stp=cp6_dst-jpg_tt6&_nc_cat=107&ccb=1-7&_nc_sid=833d8c&_nc_ohc=74jf7lX_WRUQ7kNvwH0D9JY&_nc_oc=Adl8HHdkQRE6KVBowcn1zlFPTlbGdY3oZ4V29_oDMlVaD3mvu-ZcnldMZT8kvwQVNo4&_nc_zt=23&_nc_ht=scontent.fsjo14-1.fna&_nc_gid=4hNPY9Fy3Hq2H6XvebVbpA&oh=00_AfPqiBqAmr7ieM-kDBpf3HfPXT6EVYezq6ztu3C9xxmhaA&oe=685260C2', b'1', 1, 3, 18),
('Yakitori', 'Brochetas de pollo a la parrilla con salsa teriyaki', 'https://scontent.fsjo14-1.fna.fbcdn.net/v/t39.30808-6/490982604_1316445843245662_1084274833728739232_n.jpg?stp=cp6_dst-jpg_tt6&_nc_cat=102&ccb=1-7&_nc_sid=833d8c&_nc_ohc=DoOiX2sG6bUQ7kNvwFValv8&_nc_oc=AdlYkNFCyfDrnebF8Oxy6cGK7K4pugQwgh_BSh87kTsLADPqYOMBHmDxT6ACCQV3ih4&_nc_zt=23&_nc_ht=scontent.fsjo14-1.fna&_nc_gid=3NKQNsPJUXBwHrgOiHaw3g&oh=00_AfNFbAuHlCQFghpsqU-2ifObNDCQISENAL490iakBMhq2g&oe=6852667F', b'0', 1, 2, 18);

INSERT INTO EtiquetasProductos (IdProducto, IdEtiqueta, IdTipoEntidad) VALUES
(11, 2, 21),  -- California Roll → Vegetariano
(13, 2, 21),  -- Edamame → Vegetariano
(14, 1, 21),  -- Ramen Picante → Picante
(15, 2, 21),  -- Curry Vegetariano → Vegetariano
(15, 1, 21);  -- Curry Vegetariano → Picante también (ejemplo combinado)'

INSERT INTO TiposPedidos (IdTipoPedido, Descripcion, IdTipoEntidad) VALUES
(1, 'Domicilio', 6),
(2, 'Recogida en local', 6);