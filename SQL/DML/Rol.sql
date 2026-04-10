
-- ============================================================================
--             OBTENER ROLES
-- ============================================================================
DROP PROCEDURE IF EXISTS ObtenerRoles;
DELIMITER $$

CREATE PROCEDURE ObtenerRoles (
)
BEGIN
    SELECT IdRol, Descripcion as Rol
    FROM Roles
    ORDER BY IdRol ASC;
END$$

DELIMITER ;