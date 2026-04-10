
-- REGISTRAR PRODUCTO

DROP PROCEDURE IF EXISTS RegistrarProducto;
DELIMITER $$

CREATE PROCEDURE RegistrarProducto (
    IN p_Nombre VARCHAR(100),
    IN p_Descripcion VARCHAR(500),
    IN p_ImagenURL VARCHAR(510),
    IN p_CanjeablePuntos BIT,
    IN p_IdCategoria TINYINT,
    
    OUT p_CodigoError INT,
    OUT p_Mensaje TEXT,
    OUT p_IdProducto BIGINT
)

procedimiento: BEGIN
    -- Declaraciones
    DECLARE v_IdTipoEntidad SMALLINT UNSIGNED;
    DECLARE v_IdEstado SMALLINT UNSIGNED;

    DECLARE v_error_mensaje TEXT; 
    DECLARE v_nombre_existente INT DEFAULT 0;

    -- Handler para errores SQL
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 v_error_mensaje = MESSAGE_TEXT;
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje = CONCAT('Error en el servidor: ', v_error_mensaje);
        SET p_IdProducto = 0;
    END;

    -- Handler para errores NOT FOUND
    DECLARE EXIT HANDLER FOR NOT FOUND
    BEGIN
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje = 'Error: No se encontraron los datos requeridos en la configuración.';
        SET p_IdProducto = 0;
    END;

    -- Inicializar valores de salida
    SET p_CodigoError = 0;
    SET p_Mensaje = '';
    SET p_IdProducto = 0;

    START TRANSACTION;

    -- Verificar si ya existe un producto con ese nombre
    SELECT COUNT(*) INTO v_nombre_existente
    FROM Productos
    WHERE Nombre = p_Nombre
    LIMIT 1;

    IF v_nombre_existente > 0 THEN
        SET p_CodigoError = 1;
        SET p_Mensaje = 'El nombre del producto ya existe.';
        ROLLBACK;
        LEAVE procedimiento;
    END IF;

    -- Obtener IdTipoEntidad y IdEstado
    SELECT IdTipoEntidad INTO v_IdTipoEntidad
    FROM TipoEntidades
    WHERE Descripcion = 'Producto'
    LIMIT 1;

    SELECT IdEstado INTO v_IdEstado
    FROM Estados
    WHERE Descripcion = 'Activo'
    LIMIT 1;

    -- Insertar el producto
    INSERT INTO Productos (
        Nombre,
        Descripcion,
        ImagenUrl,
        CanjeablePuntos,
        IdEstado,
        IdCategoria,
        IdTipoEntidad
    ) VALUES (
        p_Nombre,
        p_Descripcion,
        p_ImagenURL,
        p_CanjeablePuntos,
        v_IdEstado,
        p_IdCategoria,
        v_IdTipoEntidad
    );

    SET p_IdProducto = LAST_INSERT_ID();

    COMMIT;

    SET p_CodigoError = 0;
    SET p_Mensaje = 'Producto registrado correctamente.';
END$$

DELIMITER ;



----------------------------------------------------------------------
-- REGISTRAR PESENTACIONES DE PRODUCTO 
----------------------------------------------------------------------
-- PROCEDIMIENTO UTIL TANTO PARA REGISTRAR LA PRESENTACION COMO PARA ACTUALIZAR EL PRECIO 

DROP PROCEDURE IF EXISTS RegistrarPresentacionesProducto;
DELIMITER $$
CREATE PROCEDURE RegistrarPresentacionesProducto (
    IN p_IdProducto BIGINT,
    IN p_IdPresentacion SMALLINT,
    IN p_Precio DECIMAL(10,2),
    
    OUT p_CodigoError INT,
    OUT p_Mensaje TEXT
)

procedimiento: BEGIN
    -- Declaraciones
    DECLARE v_IdTipoEntidad SMALLINT UNSIGNED;
    DECLARE v_IdEstado SMALLINT UNSIGNED;

    DECLARE v_error_mensaje TEXT; 

    DECLARE v_presentacion_producto_existe INT DEFAULT 0;

    -- Handler para errores SQL
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 v_error_mensaje = MESSAGE_TEXT;
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje = CONCAT('Error en el servidor: ', v_error_mensaje);
        SET p_IdProducto = NULL;
    END;

    -- Handler para errores NOT FOUND
    DECLARE EXIT HANDLER FOR NOT FOUND
    BEGIN
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje = 'Error: No se encontraron los datos requeridos en la configuración.';
        SET p_IdProducto = NULL;
    END;

    -- Inicializar valores de salida
    SET p_CodigoError = 0;
    SET p_Mensaje = '';

    START TRANSACTION;

    SELECT COUNT(*) INTO v_presentacion_producto_existe
    FROM PresentacionesProducto
    WHERE IdProducto = p_IdProducto AND IdPresentacion = p_IdPresentacion 
    LIMIT 1;
    
    if v_presentacion_producto_existe > 0 THEN
        UPDATE PresentacionesProducto
            SET Precio = p_Precio
            WHERE IdProducto = p_IdProducto AND IdPresentacion = p_IdPresentacion;
        COMMIT;
        SET p_CodigoError = 0;
        SET p_Mensaje = 'Presentación actualizada correctamente.';                
        LEAVE Procedimiento;
    END IF;

    SELECT IdTipoEntidad INTO v_IdTipoEntidad FROM TipoEntidades
    WHERE Descripcion = 'PresentacionesProducto' LIMIT 1;

    SELECT IdEstado INTO v_IdEstado FROM Estados    
    WHERE Descripcion = 'Activo' LIMIT 1;

    INSERT INTO PresentacionesProducto (
        IdProducto,
        IdPresentacion,
        Precio,
        IdEstado,
        IdTipoEntidad
    ) VALUES (
        p_IdProducto,
        p_IdPresentacion,
        p_Precio,
        v_IdEstado,
        v_IdTipoEntidad
    );

    COMMIT;

    SET p_CodigoError = 0;
    SET p_Mensaje = 'Producto registrado correctamente.';
END$$

DELIMITER ;