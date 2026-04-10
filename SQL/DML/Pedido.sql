
DELIMITER $$

DROP PROCEDURE IF EXISTS RegistrarPedidoCompleto $$

CREATE PROCEDURE RegistrarPedidoCompleto (
    IN p_IdUsuario BIGINT UNSIGNED,
    IN p_NotaCliente VARCHAR(500),
    IN p_PuntosCanjeados BIGINT UNSIGNED,
    IN p_IdTipoPedido TINYINT UNSIGNED,
    IN p_IdDireccion BIGINT UNSIGNED,
    IN p_IdMetodoPago TINYINT UNSIGNED,

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

    -- * Verificar y validar que todos los items del carrito estan activos, tanto presentaciones como promociones
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

        -- * Hay que sumar el valor de las presentaciones y promociones que hay en el carrito del usuario que son Canjeables por puntos

        -- * Ademas hay que verificar que el total del valor de productos y promociones canjeables es igual o menor que los puntos canjeados
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
        DECLARE v_FactorPuntos INT;
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
END$$

DELIMITER ;


-- ============================================================================
--             CAMBIO DE ESTADO PEDIDO
-- ============================================================================
DROP PROCEDURE IF EXISTS CambiarEstadoPedido;
DELIMITER $$

CREATE PROCEDURE CambiarEstadoPedido (
    IN p_IdPedido BIGINT UNSIGNED,
    IN p_IdEstadoPedido SMALLINT UNSIGNED,

    OUT p_CodigoError INT,
    OUT p_Mensaje TEXT
)
BEGIN
    DECLARE v_error_mensaje TEXT;

    -- Handler para errores SQL
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 v_error_mensaje = MESSAGE_TEXT;
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje = CONCAT('Error en el servidor: ', v_error_mensaje);
    END;

    SET p_CodigoError = 0;
    SET p_Mensaje = '';

    START TRANSACTION;

    UPDATE Pedidos
    SET IdEstadoPedido = p_IdEstadoPedido
    WHERE IdPedido = p_IdPedido;

    COMMIT;
    SET p_CodigoError = 0;
    SET p_Mensaje = 'Estado del pedido actualizado correctamente.';
END $$

DELIMITER ;