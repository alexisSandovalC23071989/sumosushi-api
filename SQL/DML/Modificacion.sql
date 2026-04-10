

CREATE TABLE PresentacionesProducto (
    IdPresentacionProducto INT UNSIGNED NOT NULL AUTO_INCREMENT,
    IdProducto SMALLINT UNSIGNED NOT NULL,
    IdPresentacion TINYINT UNSIGNED NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    IdEstado SMALLINT UNSIGNED NOT NULL,
    IdTipoEntidad SMALLINT UNSIGNED NOT NULL,

    CONSTRAINT PK_PresentacionesProducto PRIMARY KEY (IdPresentacionProducto),

    CONSTRAINT UQ_PresentacionesProducto UNIQUE (IdProducto, IdPresentacion),

    CONSTRAINT FK_PresentacionesProducto_Productos FOREIGN KEY (IdProducto) 
        REFERENCES Productos(IdProducto) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT FK_PresentacionesProducto_Presentacion FOREIGN KEY (IdPresentacion) 
        REFERENCES Presentaciones(IdPresentacion) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT FK_PresentacionesProducto_Estados FOREIGN KEY (IdEstado) 
        REFERENCES Estados(IdEstado) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT FK_PresentacionesProducto_TipoEntidad FOREIGN KEY (IdTipoEntidad) 
        REFERENCES TipoEntidades(IdTipoEntidad) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;



CREATE TABLE Carrito(
    IdProductoCarritoUsuario BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
    IdUsuario BIGINT UNSIGNED NOT NULL,
    IdPresentacionProducto INT UNSIGNED DEFAULT NULL,
    IdPromocion BIGINT UNSIGNED DEFAULT NULL,
    Cantidad INT UNSIGNED NOT NULL,
    IdTipoEntidad SMALLINT UNSIGNED NOT NULL,
    CONSTRAINT PK_Carrito PRIMARY KEY (IdProductoCarritoUsuario),

    CONSTRAINT UQ_Carrito UNIQUE (IdUsuario, IdPresentacionProducto),
    CONSTRAINT UQ_Carrito_Promocion UNIQUE (IdUsuario, IdPromocion),

    CONSTRAINT CHK_Carrito_SoloUno CHECK (
        (IdPresentacionProducto IS NOT NULL AND IdPromocion IS NULL) OR 
        (IdPresentacionProducto IS NULL AND IdPromocion IS NOT NULL)
    ),
    CONSTRAINT FK_Carrito_Usuarios FOREIGN KEY (IdUsuario)
        REFERENCES Usuarios(IdUsuario) ON DELETE RESTRICT ON UPDATE RESTRICT, 

    CONSTRAINT FK_Carrito_PresentacionesProducto FOREIGN KEY (IdPresentacionProducto)
        REFERENCES PresentacionesProducto(IdPresentacionProducto) ON DELETE RESTRICT ON UPDATE RESTRICT,

    CONSTRAINT FK_Carrito_Promociones FOREIGN KEY (IdPromocion)
        REFERENCES Promociones(IdPromocion) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT FK_Carrito_TipoEntidad FOREIGN KEY (IdTipoEntidad) 
        REFERENCES TipoEntidades(IdTipoEntidad) ON DELETE RESTRICT ON UPDATE RESTRICT
);


CREATE TABLE DetallesPedidos (
    IdDetallePedido BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
    IdPedido BIGINT UNSIGNED NOT NULL,
    IdPresentacionProducto INT UNSIGNED DEFAULT NULL,
    IdPromocion BIGINT UNSIGNED DEFAULT NULL,
    IdTipoEntidad SMALLINT UNSIGNED NOT NULL,
    Precio DECIMAL(10,2) NOT NULL,
    NotaCliente VARCHAR(500) DEFAULT NULL,
    Cantidad INT UNSIGNED NOT NULL DEFAULT 1,

    CONSTRAINT PK_DetallesPedidos PRIMARY KEY (IdDetallePedido),    

    CONSTRAINT CHK_DetallesPedidos_SoloUno CHECK (
        (IdPresentacionProducto IS NOT NULL AND IdPromocion IS NULL) OR 
        (IdPresentacionProducto IS NULL AND IdPromocion IS NOT NULL)
    ),

    CONSTRAINT FK_DetallesPedidos_Pedidos FOREIGN KEY (IdPedido) 
        REFERENCES Pedidos(IdPedido) ON DELETE RESTRICT ON UPDATE RESTRICT,

    CONSTRAINT FK_DetallesPedidos_PresentacionesProducto FOREIGN KEY (IdPresentacionProducto) 
        REFERENCES PresentacionesProducto(IdPresentacionProducto) ON DELETE RESTRICT ON UPDATE RESTRICT,

    CONSTRAINT FK_DetallesPedidos_Promocion FOREIGN KEY (IdPromocion) 
        REFERENCES Promociones(IdPromocion) ON DELETE RESTRICT ON UPDATE RESTRICT,

    CONSTRAINT FK_DetallesPedidos_TipoEntidad FOREIGN KEY (IdTipoEntidad) 
        REFERENCES TipoEntidades(IdTipoEntidad) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;


CREATE TABLE Pedidos (
    IdPedido BIGINT UNSIGNED NOT NULL AUTO_INCREMENT,
    NotaCliente VARCHAR(500) DEFAULT NULL,
    PuntosCanjeados BIGINT UNSIGNED DEFAULT 0,
    TotalPagado DECIMAL(10,2) NOT NULL,
    IdUsuario BIGINT UNSIGNED NOT NULL,

    IdTipoPedido TINYINT UNSIGNED NOT NULL,
    IdDireccion BIGINT UNSIGNED DEFAULT NULL,
    
    IdMetodoPago TINYINT UNSIGNED NOT NULL,
    IdTipoEntidad SMALLINT UNSIGNED NOT NULL,
    FechaCreacion DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT PK_Pedidos PRIMARY KEY (IdPedido),

    CONSTRAINT FK_Pedidos_Usuarios FOREIGN KEY (IdUsuario) 
        REFERENCES Usuarios(IdUsuario) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT FK_Pedidos_TiposPedidos FOREIGN KEY (IdTipoPedido) 
        REFERENCES TiposPedidos(IdTipoPedido) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT FK_Pedidos_Direcciones FOREIGN KEY (IdDireccion)
        REFERENCES Direcciones(IdDireccion) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT FK_Pedidos_MetodoPago FOREIGN KEY (IdMetodoPago) 
        REFERENCES MetodoPago(IdMetodoPago) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT FK_Pedidos_TipoEntidad FOREIGN KEY (IdTipoEntidad) REFERENCES TipoEntidades(IdTipoEntidad) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;


CREATE TABLE Restaurante (
  IdRestaurante SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT,
  Nombre VARCHAR(100) NOT NULL,
  Direccion VARCHAR(200) NOT NULL,
  Telefono VARCHAR(20) NOT NULL,
  Correo VARCHAR(100) NOT NULL,
  LogoUrl VARCHAR(512) DEFAULT NULL,  
  IdTipoEntidad SMALLINT UNSIGNED NOT NULL,

  VersionMenu BIGINT UNSIGNED NOT NULL DEFAULT 0,
  
  CONSTRAINT PK_Restaurante PRIMARY KEY (IdRestaurante),
  CONSTRAINT FK_Restaurante_TipoEntidad FOREIGN KEY (IdTipoEntidad) 
    REFERENCES TipoEntidades(IdTipoEntidad) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;



--AUN NO ESTA APLICADO EN LA BD
CREATE TABLE Productos (
    IdProducto SMALLINT UNSIGNED NOT NULL AUTO_INCREMENT,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(500) NOT NULL,
    ImagenUrl VARCHAR(510) NOT NULL,
    CanjeablePuntos BIT NOT NULL DEFAULT b'0',
    IdEstado SMALLINT UNSIGNED NOT NULL,
    IdCategoria TINYINT UNSIGNED NOT NULL,
    IdTipoEntidad SMALLINT UNSIGNED NOT NULL,
    CONSTRAINT PK_Productos PRIMARY KEY (IdProducto),
    CONSTRAINT FK_Productos_Estados FOREIGN KEY (IdEstado) 
        REFERENCES Estados(IdEstado) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT FK_Productos_CategoriaProductos FOREIGN KEY (IdCategoria) 
        REFERENCES CategoriaProductos(IdCategoria) ON DELETE RESTRICT ON UPDATE RESTRICT,
    CONSTRAINT FK_Productos_TipoEntidad FOREIGN KEY (IdTipoEntidad) 
        REFERENCES TipoEntidades(IdTipoEntidad) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB;






CREATE DEFINER=`SumoAdmin`@`%` PROCEDURE `ActualizarPromocion`(
    IN  p_IdPromocion      BIGINT UNSIGNED,
    IN  p_Descripcion      VARCHAR(500),
    IN  p_FechaInicio      DATETIME,
    IN  p_FechaFinal       DATETIME,
    IN  p_ImagenUrl        TEXT,
    IN  p_CanjeablePuntos  BIT,
    IN 	p_Estado VARCHAR(50),
    
    OUT p_CodigoError      INT,
    OUT p_Mensaje          TEXT
)
BEGIN
    DECLARE v_error_msg        TEXT;
    DECLARE v_IdEstadoActivo   SMALLINT UNSIGNED;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
      GET DIAGNOSTICS CONDITION 1 v_error_msg = MESSAGE_TEXT;
      ROLLBACK;
      SET p_CodigoError = 2;
      SET p_Mensaje    = CONCAT('Error BD: ', v_error_msg);
    END;

    DECLARE EXIT HANDLER FOR NOT FOUND
    BEGIN
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje = 'Error BD: No se encontraron los datos requeridos en la actualizacion.';
    END;

    START TRANSACTION;

        -- Validación de fechas
        IF p_FechaFinal < p_FechaInicio THEN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'La FechaFinal no puede ser anterior a FechaInicio';
        END IF;

        SELECT  IdEstado INTO v_IdEstadoActivo            
        FROM Estados
        WHERE Descripcion = p_Estado;

      -- Actualizo Promoción
        UPDATE Promociones
            SET Descripcion      = p_Descripcion,
                FechaInicio      = p_FechaInicio,
                FechaFinal       = p_FechaFinal,
                ImagenUrl        = p_ImagenUrl,
                CanjeablePuntos  = p_CanjeablePuntos,
                IdEstado         = v_IdEstadoActivo
        WHERE IdPromocion = p_IdPromocion;

    COMMIT;

    SET p_CodigoError = 0;
    SET p_Mensaje    = 'Promoción actualizada correctamente';
END





CREATE DEFINER=`SumoAdmin`@`%` PROCEDURE `CrearPromocion`(
    IN  p_Nombre           VARCHAR(100),
    IN  p_Descripcion      VARCHAR(500),
    IN  p_FechaInicio      DATETIME,
    IN  p_FechaFinal       DATETIME,
    IN  p_ImagenUrl        TEXT,
    IN  p_CanjeablePuntos  BIT,
    IN  p_Precio DECIMAL(10,2),
    
    OUT p_CodigoError      INT,
    OUT p_Mensaje          TEXT
)
BEGIN
    DECLARE v_error_mensaje      TEXT;
    DECLARE v_IdTipoEntidad    SMALLINT UNSIGNED;
    DECLARE v_IdEstado    SMALLINT UNSIGNED;

    -- Handler para errores SQL
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 v_error_mensaje = MESSAGE_TEXT;
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje = CONCAT('Error en el servidor: ', v_error_mensaje);
    END;

    -- Handler para errores NOT FOUND (por SELECT INTO que no devuelve nada)
    DECLARE EXIT HANDLER FOR NOT FOUND

    proc: BEGIN
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje = 'Error: No se encontraron los datos requeridos en la configuración.';
    END;

    START TRANSACTION;

      -- Validación de fechas
      IF p_FechaFinal < p_FechaInicio THEN
        ROLLBACK;
        SET p_CodigoError = 1;
        SET p_Mensaje = CONCAT("La FechaFinal '", p_FechaFinal, "' no puede ser anterior a FechaInicio '", p_FechaInicio, "'");
      END IF;

      -- Obtengo IdTipoEntidad = 'Promocion'
      SELECT IdTipoEntida INTO v_IdTipoEntidad FROM TipoEntidades
       WHERE Descripcion = 'Promocion' LIMIT 1;

      -- Obtengo IdEstado = 'Activo'
        SELECT IdEstado INTO v_IdEstado FROM Estados WHERE Descripcion = 'Activo' LIMIT 1;

      -- Inserto en Promociones
      INSERT INTO Promociones
        (
            Nombre, 
            Descripcion, 
            FechaInicio, 
            FechaFinal,
            IdTipoEntidad, 
            IdEstado, 
            ImagenUrl, 
            CanjeablePuntos,
            Precio
        )
      VALUES
        (
            p_Nombre, 
            p_Descripcion, 
            p_FechaInicio, 
            p_FechaFinal,
            v_IdTipoEntidad, 
            v_IdEstado, 
            p_ImagenUrl, 
            p_CanjeablePuntos,
            p_Precio
        );

    COMMIT;

    SET p_CodigoError = 0;
    SET p_Mensaje    = 'Promoción creada correctamente';
END;







CREATE DEFINER=`SumoAdmin`@`%` PROCEDURE `Carrito_AgregarOActualizarPresentacion`(
    IN p_IdUsuario BIGINT,
    IN p_IdPresentacionProducto INT,
    IN p_Cantidad INT,

    OUT p_CodigoError INT,
    OUT p_Mensaje VARCHAR(300)
)
proc: BEGIN
    DECLARE v_IdExistente BIGINT;
    DECLARE v_IdTipoEntidad SMALLINT UNSIGNED;

    -- Validación de campos
    IF p_IdPresentacionProducto IS NULL THEN
        SET p_CodigoError = 1;
        SET p_Mensaje = 'Error: El campo IdPresentacionProducto es requerido.';
        LEAVE proc;
    END IF;

    -- Buscar si ya existe un producto en el carrito con esa presentación
    SELECT IdProductoCarritoUsuario INTO v_IdExistente
    FROM Carrito
    WHERE IdUsuario = p_IdUsuario AND IdPresentacionProducto = p_IdPresentacionProducto;

    IF v_IdExistente IS NOT NULL THEN
        -- Actualizar cantidad si ya existe
        UPDATE Carrito
        SET Cantidad = p_Cantidad
        WHERE IdProductoCarritoUsuario = v_IdExistente;

        SET p_CodigoError = 0;
        SET p_Mensaje = 'Cantidad actualizada para el producto en el carrito.';
    ELSE
        -- Obtener IdTipoEntidad para Carrito
        SELECT IdTipoEntidad INTO v_IdTipoEntidad
        FROM TipoEntidades
        WHERE Descripcion = 'Carrito';

        -- Insertar si no existe
        INSERT INTO Carrito (IdUsuario, IdPresentacionProducto, Cantidad, IdTipoEntidad)
        VALUES (p_IdUsuario, p_IdPresentacionProducto, p_Cantidad, v_TipoEntidad);

        SET p_CodigoError = 0;
        SET p_Mensaje = 'Producto agregado al carrito.';
    END IF;
END




CREATE DEFINER=`SumoAdmin`@`%` PROCEDURE `Carrito_AgregarOActualizarPromocion`(
    IN p_IdUsuario BIGINT,
    IN p_IdPromocion BIGINT,
    IN p_Cantidad INT,

    OUT p_CodigoError INT,
    OUT p_Mensaje VARCHAR(300)
)
proc: BEGIN
    DECLARE v_IdExistente BIGINT;
    DECLARE v_IdTipoEntidad SMALLINT UNSIGNED;

    -- Validación de campos
    IF p_IdPromocion IS NULL THEN
        SET p_CodigoError = 1;
        SET p_Mensaje = 'Error: El campo IdPromocion es requerido.';
        LEAVE proc;
    END IF;

    -- Buscar si ya existe un producto en el carrito con la misma promoción
    SELECT IdProductoCarritoUsuario INTO v_IdExistente
    FROM Carrito
    WHERE IdUsuario = p_IdUsuario AND IdPromocion = p_IdPromocion;

    IF v_IdExistente IS NOT NULL THEN
        -- Actualizar cantidad si ya existe
        UPDATE Carrito
        SET Cantidad = p_Cantidad
        WHERE IdProductoCarritoUsuario = v_IdExistente;

        SET p_CodigoError = 0;
        SET p_Mensaje = 'Cantidad actualizada para la promoción en el carrito.';
    ELSE
        -- Obtener IdTipoEntidad para Carrito
        SELECT IdTipoEntidad INTO v_IdTipoEntidad
        FROM TipoEntidades
        WHERE Descripcion = 'Carrito';
        -- Insertar si no existe
        INSERT INTO Carrito (IdUsuario, IdPromocion, Cantidad, IdTipoEntidad)
        VALUES (p_IdUsuario, p_IdPromocion, p_Cantidad, v_TipoEntidad);

        SET p_CodigoError = 0;
        SET p_Mensaje = 'Promoción agregada al carrito.';
    END IF;
END




CREATE DEFINER=`SumoAdmin`@`%` PROCEDURE `Carrito_Eliminar`(
    IN p_IdProductoCarritoUsuario BIGINT, 
    IN p_IdUsuario BIGINT,
       
    OUT p_CodigoError INT,
    OUT p_Mensaje VARCHAR(300)
)
proc: BEGIN 
    DECLARE v_ProductoEncontrado INT;

    DECLARE EXIT HANDLER FOR NOT FOUND
    BEGIN
        ROLLBACK;
        SET p_CodigoError = 1;
        SET p_Mensaje = 'El item no existe o no pertenece al usuario.';
    END;

    -- Verificar si el producto existe en el carrito del usuario
    SELECT IdProductoCarritoUsuario INTO v_ProductoEncontrado
    FROM Carrito
    WHERE IdProductoCarritoUsuario = p_IdProductoCarritoUsuario AND IdUsuario = p_IdUsuario;

    DELETE FROM Carrito
    WHERE IdProductoCarritoUsuario = p_IdProductoCarritoUsuario;

    -- Confirmación
    SET p_CodigoError = 0;
    SET p_Mensaje = 'Producto eliminado correctamente del carrito.';
END








DROP PROCEDURE IF EXISTS ActualizarVersionMenu;

DELIMITER $$

CREATE PROCEDURE ActualizarVersionMenu(    
    OUT p_CodigoError INT,
    OUT p_Mensaje TEXT
)
procedimiento: BEGIN
    -- Declaraciones

    DECLARE v_error_mensaje TEXT;
    
    -- Handler para errores SQL - SIN ROLLBACK
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 v_error_mensaje = MESSAGE_TEXT;
        SET p_CodigoError = 2;
        SET p_Mensaje = CONCAT('Error en BD: ', v_error_mensaje);
    END;
    
    -- Inicializar valores de salida
    SET p_CodigoError = 0;
    SET p_Mensaje = '';
    
    START TRANSACTION;
    
    UPDATE Restaurante
    SET VersionMenu = VersionMenu + 1
    WHERE IdRestaurante = 1;  
    
    SET p_CodigoError = 0;
    SET p_Mensaje = 'Etiqueta asignada correctamente.';
    
    COMMIT;
END$$



-- ===========================================================



CREATE DEFINER=`SumoAdmin`@`%` PROCEDURE `RegistrarPedidoCompleto`(
    IN p_IdUsuario BIGINT UNSIGNED,
    IN p_IdTipoPedido TINYINT UNSIGNED,
    IN p_IdMetodoPago TINYINT UNSIGNED,
    IN p_PuntosCanjeados BIGINT UNSIGNED,
    IN p_NotaCliente VARCHAR(500),        
    IN p_IdDireccion BIGINT UNSIGNED,    

    OUT p_CodigoError INT,
    OUT p_Mensaje TEXT
)
transaccion:BEGIN
    DECLARE v_IdTipoEntidad SMALLINT UNSIGNED;
    DECLARE v_CostoEnvio DECIMAL(10,2) DEFAULT 0;
    DECLARE v_TotalCarrito DECIMAL(10,2) DEFAULT 0;
    DECLARE v_PuntosUsuario BIGINT UNSIGNED;
    DECLARE v_IdPedido BIGINT UNSIGNED;
    DECLARE v_IdEstadoActivo SMALLINT UNSIGNED;

    DECLARE v_MinPuntos INT;
    DECLARE v_MaxPuntos INT;
    DECLARE v_FactorPuntos DECIMAL(5,2);
    DECLARE v_TotalCanjeableEnCarrito DECIMAL(10,2) DEFAULT 0;
    
    DECLARE v_error TEXT;
    DECLARE v_TipoPedidoDescripcion VARCHAR(50);
    DECLARE v_TotalFinal DECIMAL(10,2);

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 v_error = MESSAGE_TEXT;
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje = CONCAT('Error del servidor: ', v_error);
    END;

    DECLARE EXIT HANDLER FOR NOT FOUND
    BEGIN
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje = 'Error: No se encontraron los datos requeridos en la configuración.';
    END;

    START TRANSACTION;

    SET p_CodigoError = 0;
    SET p_Mensaje = '';

    SELECT IdEstado INTO v_IdEstadoActivo
    FROM Estados WHERE Descripcion = 'Activo' LIMIT 1;

    -- Verificar que el usuario tiene items en el carrito
    IF NOT EXISTS (SELECT 1 FROM Carrito WHERE IdUsuario = p_IdUsuario) THEN
        SET p_CodigoError = 1;
        SET p_Mensaje = 'El carrito está vacío.';
        ROLLBACK;
        LEAVE transaccion;
    END IF;

    -- Verificar y validar que todos los items del carrito estan activos, tanto presentaciones como promociones
    IF EXISTS (
        SELECT 1
        FROM Carrito c
        LEFT JOIN PresentacionesProducto pp ON c.IdPresentacionProducto = pp.IdPresentacionProducto
        LEFT JOIN Promociones p ON c.IdPromocion = p.IdPromocion
        WHERE c.IdUsuario = p_IdUsuario
        AND (
            (c.IdPresentacionProducto IS NOT NULL AND pp.IdEstado <> v_IdEstadoActivo)
            OR (c.IdPromocion IS NOT NULL AND p.IdEstado <> v_IdEstadoActivo)
        )
    ) THEN
        SET p_CodigoError = 1;
        SET p_Mensaje = 'Hay productos o promociones en el carrito que no están activos.';
        ROLLBACK;
        LEAVE transaccion;
    END IF;

    -- Verificar que ninguna presentación o promoción tenga precio 0
    IF EXISTS (
        SELECT 1
        FROM Carrito c
        LEFT JOIN PresentacionesProducto pp ON c.IdPresentacionProducto = pp.IdPresentacionProducto
        LEFT JOIN Promociones p ON c.IdPromocion = p.IdPromocion
        WHERE c.IdUsuario = p_IdUsuario
        AND (
            (c.IdPresentacionProducto IS NOT NULL AND (pp.Precio IS NULL OR pp.Precio = 0))
            OR (c.IdPromocion IS NOT NULL AND (p.Precio IS NULL OR p.Precio = 0))
        )
    ) THEN
        SET p_CodigoError = 1;
        SET p_Mensaje = 'Uno o más productos o promociones del carrito tienen un precio inválido.';
        ROLLBACK;
        LEAVE transaccion;
    END IF;

    -- ========================================================================
    -- Se estan usando puntos?
    IF p_PuntosCanjeados > 0 THEN
        SELECT PuntosAcumulados INTO v_PuntosUsuario
        FROM Usuarios WHERE IdUsuario = p_IdUsuario;

        -- Usuario con suficientes puntos? 
        IF v_PuntosUsuario < p_PuntosCanjeados THEN
            SET p_CodigoError = 1;
            SET p_Mensaje = 'No tiene suficientes puntos acumulados.';
            ROLLBACK;
            LEAVE transaccion;
        END IF;

        -- Obtenemos informacion de gestion de puntos
        SELECT PuntosMinimosCompra, PuntosMaximosCompra 
        INTO v_MinPuntos, v_MaxPuntos
        FROM GestionPuntos;

        -- Sumar el valor de las presentaciones y promociones que hay en el carrito del usuario que son Canjeables por puntos
        SELECT 
            IFNULL(SUM(
                CASE 
                    WHEN c.IdPresentacionProducto IS NOT NULL AND pp.CanjeablePuntos = 1 THEN pp.Precio * c.Cantidad
                    WHEN c.IdPromocion IS NOT NULL AND p.CanjeablePuntos = 1 THEN p.Precio * c.Cantidad
                    ELSE 0
                END
            ), 0)
        INTO v_TotalCanjeableEnCarrito
        FROM Carrito c
        LEFT JOIN PresentacionesProducto pp ON c.IdPresentacionProducto = pp.IdPresentacionProducto
        LEFT JOIN Promociones p ON c.IdPromocion = p.IdPromocion
        WHERE c.IdUsuario = p_IdUsuario;

        IF p_PuntosCanjeados > v_TotalCanjeableEnCarrito THEN
            SET p_CodigoError = 1;
            SET p_Mensaje = 'El valor de puntos canjeados supera el total canjeable del carrito.';
            ROLLBACK;
            LEAVE transaccion;
        END IF;
        
        IF p_PuntosCanjeados > 0 AND 
        (p_PuntosCanjeados < v_MinPuntos OR 
        p_PuntosCanjeados > v_MaxPuntos) THEN
            SET p_CodigoError = 1;
            SET p_Mensaje = 'La cantidad de puntos a canjear no está permitida.';
            ROLLBACK;
            LEAVE transaccion;
        END IF;
    END IF;   
    -- ========================================================================

    -- Obtener descripción tipo pedido
    SELECT Descripcion INTO v_TipoPedidoDescripcion
    FROM TiposPedidos WHERE IdTipoPedido = p_IdTipoPedido;

    -- Si es domicilio, verificar dirección
    IF v_TipoPedidoDescripcion = 'Domicilio' THEN
        -- Obtener costo de envío
        SET v_CostoEnvio = 1000;
        -- Verificar que se proporciono una direccion
        IF p_IdDireccion IS NULL THEN
            SET p_CodigoError = 1;
            SET p_Mensaje = 'Debe indicar una dirección válida para entrega a domicilio.';
            ROLLBACK;
            LEAVE transaccion;
        END IF;
        -- Verificar que la dirección pertenece al usuario
        IF NOT EXISTS (
            SELECT 1 FROM Direcciones 
            WHERE IdDireccion = p_IdDireccion AND IdUsuario = p_IdUsuario
        ) THEN
            SET p_CodigoError = 1;
            SET p_Mensaje = 'La dirección especificada no pertenece al usuario.';
            ROLLBACK;
            LEAVE transaccion;
        END IF;
    END IF;

    -- Calcular total del carrito
    SELECT 
        IFNULL(SUM(
            CASE 
                WHEN c.IdPresentacionProducto IS NOT NULL THEN pp.Precio * c.Cantidad
                WHEN c.IdPromocion IS NOT NULL THEN p.Precio * c.Cantidad
                ELSE 0
            END
        ), 0)
    INTO v_TotalCarrito
    FROM Carrito c
    LEFT JOIN PresentacionesProducto pp ON c.IdPresentacionProducto = pp.IdPresentacionProducto
    LEFT JOIN Promociones p ON c.IdPromocion = p.IdPromocion
    WHERE c.IdUsuario = p_IdUsuario;

    SET v_TotalFinal = v_TotalCarrito + v_CostoEnvio - p_PuntosCanjeados;

    -- Obtener el idTipoEntidad para Pedidos
    SELECT IdTipoEntidad INTO v_IdTipoEntidad 
    FROM TipoEntidades WHERE Descripcion = 'Pedidos';

    -- Insertar el pedido
    INSERT INTO Pedidos (
        NotaCliente, 
        PuntosCanjeados, 
        TotalPagado, 
        IdUsuario,
        IdTipoPedido, 
        IdDireccion, 
        IdMetodoPago, 
        IdTipoEntidad, 
        CostoEnvio
    ) VALUES (
        p_NotaCliente, 
        p_PuntosCanjeados, 
        v_TotalFinal, 
        p_IdUsuario,
        p_IdTipoPedido, 
        p_IdDireccion, 
        p_IdMetodoPago, 
        v_IdTipoEntidad, 
        v_CostoEnvio
    );

    SET v_IdPedido = LAST_INSERT_ID();

    -- Obtener el idTipoEntidad para DetallesPedidos
    SELECT IdTipoEntidad INTO v_IdTipoEntidad 
    FROM TipoEntidades WHERE Descripcion = 'DetallesPedidos';

    -- Insertar detalles del pedido desde el carrito
    INSERT INTO DetallesPedidos (
        IdPedido, IdPresentacionProducto, IdPromocion, IdTipoEntidad,
        Precio, NotaCliente, Cantidad
    )
    SELECT
        v_IdPedido,
        c.IdPresentacionProducto,
        c.IdPromocion,
        v_IdTipoEntidad,
        CASE 
            WHEN c.IdPresentacionProducto IS NOT NULL THEN pp.Precio
            WHEN c.IdPromocion IS NOT NULL THEN p.Precio
            ELSE 0
        END AS Precio,
        c.NotaCliente,
        c.Cantidad
    FROM Carrito c
    LEFT JOIN PresentacionesProducto pp ON c.IdPresentacionProducto = pp.IdPresentacionProducto
    LEFT JOIN Promociones p ON c.IdPromocion = p.IdPromocion
    WHERE c.IdUsuario = p_IdUsuario;

    -- Restar puntos si se usaron
    IF p_PuntosCanjeados > 0 THEN
        UPDATE Usuarios
        SET PuntosAcumulados = PuntosAcumulados - p_PuntosCanjeados
        WHERE IdUsuario = p_IdUsuario;
    END IF;

    -- Sumar puntos ganados si aplica
    IF p_PuntosCanjeados = 0 THEN
        -- Obtener factor de puntos 
        SELECT FactorPuntos INTO v_FactorPuntos FROM GestionPuntos;

        UPDATE Usuarios
        SET PuntosAcumulados = PuntosAcumulados + FLOOR(v_TotalFinal * v_FactorPuntos)
        WHERE IdUsuario = p_IdUsuario;
    END IF;

    -- Limpiar carrito del usuario
    DELETE FROM Carrito WHERE IdUsuario = p_IdUsuario;

    COMMIT;
    SET p_CodigoError = 0;
    SET p_Mensaje = 'Pedido registrado correctamente.';
END