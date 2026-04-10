-- ============================================================================
--             OBTENER MENÚ COMPLETO
-- ============================================================================

DROP PROCEDURE IF EXISTS ObtenerMenuCompleto;
DELIMITER $$

CREATE PROCEDURE ObtenerMenuCompleto(
    -- OUT p_CodigoError INT,
    -- OUT p_Mensaje TEXT
)
BEGIN
    -- DECLARE v_error_mensaje TEXT;

    -- -- Manejador para errores SQL
    -- DECLARE EXIT HANDLER FOR SQLEXCEPTION
    -- BEGIN
    --     GET DIAGNOSTICS CONDITION 1 v_error_mensaje = MESSAGE_TEXT;
    --     ROLLBACK;
    --     SET p_CodigoError = 2;
    --     SET p_Mensaje = CONCAT('Error en el servidor: ', v_error_mensaje);
    -- END;

    -- Inicializar salida
    -- SET p_CodigoError = 0;
    -- SET p_Mensaje = '';

    -- START TRANSACTION;

    -- Consulta del menú completo
    SELECT 
        p.IdProducto             AS p_IdProducto,
        p.Nombre                 AS p_Nombre,
        p.Descripcion            AS p_Descripcion,
        p.ImagenUrl              AS p_ImagenUrl,
        cp.Descripcion           AS p_Categoria,
        GROUP_CONCAT(e.Descripcion SEPARATOR ', ') AS p_Etiquetas,
        CASE 
            WHEN SUM(e.Descripcion = 'Promoción') > 0 THEN TRUE
            ELSE FALSE
        END AS p_EsPromocion
    FROM Productos p
    INNER JOIN CategoriaProductos cp ON cp.IdCategoria = p.IdCategoria
    LEFT JOIN EtiquetasProductos ep ON ep.IdProducto = p.IdProducto
    LEFT JOIN Etiquetas e ON e.IdEtiqueta = ep.IdEtiqueta
    GROUP BY p.IdProducto
    ORDER BY p_EsPromocion DESC, cp.Descripcion ASC, p.Nombre ASC;

    -- COMMIT;
    -- SET p_CodigoError = 0;
    -- SET p_Mensaje = 'Menú obtenido correctamente.';
END$$

DELIMITER ;

-- ============================================================================
--             OBTENER DETALLES DEL PRODUCTO
-- ============================================================================

DROP PROCEDURE IF EXISTS ObtenerDetalleProducto;
DELIMITER $$

CREATE PROCEDURE ObtenerDetalleProducto(
    IN p_IdProducto INT,
    OUT p_CodigoError INT,
    OUT p_Mensaje TEXT
)
BEGIN
    DECLARE v_error_mensaje TEXT;

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

    SELECT 
        p.IdProducto         AS p_IdProducto,
        p.Nombre             AS p_Nombre,
        CAST(0 AS DECIMAL(10,2))    AS p_Precio, -- !!!! Se debe definir un campo de precio
        p.Descripcion        AS p_Descripcion,
        p.ImagenUrl          AS p_ImagenUrl,
        cp.Descripcion       AS p_Categoria,
        GROUP_CONCAT(e.Descripcion SEPARATOR ', ') AS p_Etiquetas,
        CASE 
            WHEN SUM(e.Descripcion = 'Promoción') > 0 THEN TRUE
            ELSE FALSE
        END AS p_EsPromocion
    FROM Productos p
    INNER JOIN CategoriaProductos cp ON cp.IdCategoria = p.IdCategoria
    LEFT JOIN EtiquetasProductos ep ON ep.IdProducto = p.IdProducto
    LEFT JOIN Etiquetas e ON e.IdEtiqueta = ep.IdEtiqueta
    WHERE p.IdProducto = p_IdProducto
    GROUP BY p.IdProducto;

    COMMIT;
    SET p_Mensaje = 'Detalle obtenido correctamente.';
END$$

DELIMITER ;

-- ============================================================================
--             CALIFICAR PRODUCTO
-- ============================================================================

DROP PROCEDURE IF EXISTS CalificarProducto;
DELIMITER $$

CREATE PROCEDURE CalificarProducto(
    IN p_IdUsuario BIGINT UNSIGNED,
    IN p_IdProducto INT,
    IN p_Calificacion INT,
    OUT p_CodigoError INT,
    OUT p_Mensaje TEXT
)
BEGIN
    DECLARE v_error TEXT;
    DECLARE v_Existe INT;

    DECLARE v_IdTipoEntidad SMALLINT UNSIGNED;

    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 v_error = MESSAGE_TEXT;
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje = CONCAT('Error en el servidor: ', v_error);
    END;

    DECLARE EXIT HANDLER FOR NOT FOUND
    BEGIN
        ROLLBACK;
        SET p_CodigoError = 2;
        SET p_Mensaje = 'Error: No se encontraron los datos requeridos en la configuración.';
    END;

    SET p_CodigoError = 0;
    SET p_Mensaje = '';

    START TRANSACTION;

    -- Verificar si ya existe calificación
    SELECT COUNT(*) INTO v_Existe
    FROM CalificacionesProductos
    WHERE IdUsuario = p_IdUsuario AND IdProducto = p_IdProducto;

    SELECT IdTipoEntidad INTO v_IdTipoEntidad
    FROM TipoEntidades
    WHERE Descripcion = 'CalificacionProducto' LIMIT 1;

    IF v_Existe > 0 THEN
        -- Actualizar
        UPDATE CalificacionesProductos
        SET Calificacion = p_Calificacion
        WHERE IdUsuario = p_IdUsuario AND IdProducto = p_IdProducto;
        SET p_Mensaje = 'Calificación actualizada.';
    ELSE
        -- Insertar nueva
        INSERT INTO CalificacionesProductos (IdUsuario, IdProducto, Calificacion, IdTipoEntidad)
        VALUES (p_IdUsuario, p_IdProducto, p_Calificacion, v_IdTipoEntidad);
        SET p_Mensaje = 'Calificación registrada.';
    END IF;

    COMMIT;
END$$

DELIMITER ;



-- ============================================================================
--             OBTENER PROMOCIONES
-- ============================================================================

DROP PROCEDURE IF EXISTS ObtenerPromociones;
DELIMITER $$

CREATE PROCEDURE ObtenerPromociones()
BEGIN
    DECLARE v_IdEstadoActivo SMALLINT;
    DECLARE v_IdEstadoAgotado SMALLINT;

    DECLARE EXIT HANDLER FOR NOT FOUND
    BEGIN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'No existe alguno de los estados requeridos en la BD';
	END;


    SELECT IdEstado INTO v_IdEstadoActivo FROM Estados WHERE Descripcion = 'Activo';
    SELECT IdEstado INTO v_IdEstadoAgotado FROM Estados WHERE Descripcion = 'Agotado';

    SELECT 
        p.IdPromocion,
        p.Nombre,
        p.Descripcion,
        p.FechaInicio,
        p.FechaFinal,
        p.Precio,
        p.ImagenUrl,
        p.CanjeablePuntos,
        e.Descripcion AS Estado
    FROM Promociones p
    INNER JOIN Estados e ON e.IdEstado = p.IdEstado
    WHERE p.FechaInicio <= NOW() AND p.FechaFinal >= NOW()
    AND p.IdEstado = v_IdEstadoActivo OR p.IdEstado = v_IdEstadoAgotado;
    
END$$

DELIMITER ;


-- ============================================================================
--             OBTENER PRODUCTOS ACTIVOS O AGOTADOS
-- ============================================================================

DROP PROCEDURE IF EXISTS ObtenerProductosActivos;
DELIMITER $$

CREATE PROCEDURE ObtenerProductosActivos()
BEGIN
    DECLARE v_IdEstadoActivo SMALLINT;
    DECLARE v_IdEstadoAgotado SMALLINT;

    DECLARE EXIT HANDLER FOR NOT FOUND
    BEGIN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'No existe alguno de los estados requeridos en la BD';
    END;

    -- Obtener IDs de los estados 'Activo' y 'Agotado'
    SELECT IdEstado INTO v_IdEstadoActivo FROM Estados WHERE Descripcion = 'Activo';
    SELECT IdEstado INTO v_IdEstadoAgotado FROM Estados WHERE Descripcion = 'Agotado';

    -- Obtener productos con etiquetas concatenadas
    SELECT 
        p.IdProducto,
        p.Nombre,
        p.Descripcion,  
        p.ImagenUrl,
        p.CanjeablePuntos,
        p.IdCategoria,
        c.Descripcion AS Categoria,
        e.Descripcion AS Estado,
        IFNULL(GROUP_CONCAT(et.Descripcion SEPARATOR ', '), '') AS Etiquetas
    FROM Productos p
    INNER JOIN Estados e ON e.IdEstado = p.IdEstado
    INNER JOIN CategoriaProductos c ON c.IdCategoria = p.IdCategoria
    LEFT JOIN EtiquetasProductos ep ON ep.IdProducto = p.IdProducto
    LEFT JOIN Etiquetas et ON et.IdEtiqueta = ep.IdEtiqueta
    WHERE p.IdEstado IN (v_IdEstadoActivo, v_IdEstadoAgotado)
    GROUP BY 
        p.IdProducto, p.Nombre, p.Descripcion, p.ImagenUrl, p.CanjeablePuntos,
        p.IdCategoria, c.Descripcion, e.Descripcion;

END$$

DELIMITER ;

-- ============================================================================
--             OBTENER PRESENTAIONES ACTIVAS
-- ============================================================================
DROP PROCEDURE IF EXISTS ObtenerPresentacionesActivas;
DELIMITER $$

CREATE PROCEDURE ObtenerPresentacionesActivas()
BEGIN
    DECLARE v_IdEstadoActivo SMALLINT;
    DECLARE v_IdEstadoAgotado SMALLINT;

    DECLARE EXIT HANDLER FOR NOT FOUND
    BEGIN
        SIGNAL SQLSTATE '45000'
        SET MESSAGE_TEXT = 'No existe alguno de los estados requeridos en la BD';
    END;

    -- Obtener los IdEstado para 'Activo' y 'Agotado'
    SELECT IdEstado INTO v_IdEstadoActivo FROM Estados WHERE Descripcion = 'Activo';
    SELECT IdEstado INTO v_IdEstadoAgotado FROM Estados WHERE Descripcion = 'Agotado';

    -- Obtener presentaciones activas de productos activos o agotados
    SELECT 
        pp.IdPresentacionProducto,
        pp.IdProducto,
        pr.Descripcion AS DetallePresentacion,
        pp.Precio
    FROM PresentacionesProducto pp
    INNER JOIN Productos p ON p.IdProducto = pp.IdProducto
    INNER JOIN Presentaciones pr ON pr.IdPresentacion = pp.IdPresentacion
    WHERE 
        pp.IdEstado = v_IdEstadoActivo
        AND p.IdEstado IN (v_IdEstadoActivo, v_IdEstadoAgotado);
END$$

DELIMITER ;

-- ============================================================================
--             OBTENER TODOS LOS PRODUCTOS ACTIVOS, AGOTADOS O DESHABILITADOS
-- ============================================================================

DROP PROCEDURE IF EXISTS ObtenerProductosCompleto;
DELIMITER $$

CREATE PROCEDURE ObtenerProductosCompleto()
BEGIN

    -- Obtener productos con etiquetas concatenadas
    SELECT 
        p.IdProducto,
        p.Nombre,
        p.Descripcion,  
        p.ImagenUrl,
        p.CanjeablePuntos,
        p.IdCategoria,
        c.Descripcion AS Categoria,
        e.Descripcion AS Estado        
    FROM Productos p
    INNER JOIN Estados e ON e.IdEstado = p.IdEstado
    INNER JOIN CategoriaProductos c ON c.IdCategoria = p.IdCategoria
    GROUP BY 
        p.IdProducto, p.Nombre, p.Descripcion, p.ImagenUrl, p.CanjeablePuntos,
        p.IdCategoria, c.Descripcion, e.Descripcion;

END$$

DELIMITER ;

-- ============================================================================
--             OBTENER PRESENTACIONES TOTALES
-- ============================================================================
DROP PROCEDURE IF EXISTS ObtenerPresentacionesTotales;
DELIMITER $$

CREATE PROCEDURE ObtenerPresentacionesTotales()
BEGIN

    -- Obtener presentaciones activas de productos activos o agotados
    SELECT 
        pp.IdPresentacionProducto,
        pp.IdProducto,
        pr.Descripcion AS DetallePresentacion,
        pp.Precio
    FROM PresentacionesProducto pp
    INNER JOIN Productos p ON p.IdProducto = pp.IdProducto
    INNER JOIN Presentaciones pr ON pr.IdPresentacion = pp.IdPresentacion;
END$$

DELIMITER ;

-- ============================================================================
--             OBTENER ETIQUETAS-PRODUCTOS TOTALES
-- ============================================================================
DROP PROCEDURE IF EXISTS ObtenerEtiquetasProductosTotales;
DELIMITER $$

CREATE PROCEDURE ObtenerEtiquetasProductosTotales()
BEGIN
    SELECT 
    IdProducto, 
    ep.IdEtiqueta, 
    e.Descripcion AS DetalleEtiqueta
    FROM EtiquetasProductos ep
    INNER JOIN Etiquetas e ON e.IdEtiqueta = ep.IdEtiqueta;
END$$

DELIMITER ;


-- ============================================================================
--             OBTENER ETIQUETAS
-- ============================================================================
DROP PROCEDURE IF EXISTS ObtenerEtiquetas;
DELIMITER $$
CREATE PROCEDURE ObtenerEtiquetas()
BEGIN
    SELECT 
        IdEtiqueta, 
        Descripcion AS Etiqueta
    FROM Etiquetas;
END$$


-- ============================================================================
--             REGISTRAR ETIQUETAS DE PRODUCTO
-- ============================================================================
DROP PROCEDURE IF EXISTS RegistrarEtiquetasProducto;

DELIMITER $$

CREATE PROCEDURE RegistrarEtiquetasProducto(
    IN p_IdProducto BIGINT,
    IN p_IdEtiqueta SMALLINT,
    
    OUT p_CodigoError INT,
    OUT p_Mensaje TEXT
)
procedimiento: BEGIN
    -- Declaraciones
    DECLARE v_IdTipoEntidad SMALLINT UNSIGNED;
    DECLARE v_IdEstado SMALLINT UNSIGNED;
    DECLARE v_error_mensaje TEXT; 
    DECLARE v_etiqueta_producto_existe INT DEFAULT 0;
    
    -- Handler para errores SQL - SIN ROLLBACK
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
    BEGIN
        GET DIAGNOSTICS CONDITION 1 v_error_mensaje = MESSAGE_TEXT;
        SET p_CodigoError = 2;
        SET p_Mensaje = CONCAT('Error en el servidor: ', v_error_mensaje);
    END;
    
    -- Handler para errores NOT FOUND - SIN ROLLBACK
    DECLARE EXIT HANDLER FOR NOT FOUND
    BEGIN
        SET p_CodigoError = 2;
        SET p_Mensaje = 'Error: No se encontraron los datos requeridos en la configuración.';
    END;
    
    -- Inicializar valores de salida
    SET p_CodigoError = 0;
    SET p_Mensaje = '';
    
    -- SIN START TRANSACTION - se maneja desde el servicio
    
    -- Verificar si ya existe la etiqueta para este producto
    SELECT COUNT(*) INTO v_etiqueta_producto_existe
    FROM EtiquetasProducto  -- Ajusta el nombre de la tabla según tu esquema
    WHERE IdProducto = p_IdProducto AND IdEtiqueta = p_IdEtiqueta 
    LIMIT 1;
    
    IF v_etiqueta_producto_existe > 0 THEN
        SET p_CodigoError = 1;
        SET p_Mensaje = 'La etiqueta ya está asignada a este producto.';                
        LEAVE Procedimiento;
    END IF;
    
    -- Obtener IdTipoEntidad y IdEstado
    SELECT IdTipoEntidad INTO v_IdTipoEntidad 
    FROM TipoEntidades
    WHERE Descripcion = 'EtiquetasProductos'
    LIMIT 1;
    
    SELECT IdEstado INTO v_IdEstado 
    FROM Estados    
    WHERE Descripcion = 'Activo' 
    LIMIT 1;
    
    -- Insertar la etiqueta
    INSERT INTO EtiquetasProducto (  -- Ajusta el nombre y campos según tu esquema
        IdProducto,
        IdEtiqueta,
        IdEstado,
        IdTipoEntidad
    ) VALUES (
        p_IdProducto,
        p_IdEtiqueta,
        v_IdEstado,
        v_IdTipoEntidad
    );
    
    SET p_CodigoError = 0;
    SET p_Mensaje = 'Etiqueta asignada correctamente.';
    
    -- SIN COMMIT - se maneja desde el servicio
END$$
