CREATE DATABASE  IF NOT EXISTS `sumosushidb` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `sumosushidb`;
-- MySQL dump 10.13  Distrib 8.0.42, for Win64 (x86_64)
--
-- Host: sumosushiappdev.mysql.database.azure.com    Database: sumosushidb
-- ------------------------------------------------------
-- Server version	8.0.41-azure

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `auditoria`
--

DROP TABLE IF EXISTS `auditoria`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `auditoria` (
  `IdAuditoria` bigint unsigned NOT NULL AUTO_INCREMENT,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  `IdEntidad` bigint unsigned NOT NULL,
  `Accion` varchar(20) COLLATE utf8mb4_unicode_ci NOT NULL COMMENT 'INSERT, UPDATE, DELETE',
  `ValorAnterior` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `ValorNuevo` varchar(255) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Fecha` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `UsuarioResponsable` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`IdAuditoria`),
  KEY `FK_Auditoria_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_Auditoria_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `calificacionesproductos`
--

DROP TABLE IF EXISTS `calificacionesproductos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `calificacionesproductos` (
  `IdCalificacion` int unsigned NOT NULL AUTO_INCREMENT,
  `IdUsuario` bigint unsigned NOT NULL,
  `IdProducto` smallint unsigned NOT NULL,
  `Calificacion` int NOT NULL,
  `FechaCreacion` datetime DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`IdCalificacion`),
  KEY `IdUsuario` (`IdUsuario`),
  KEY `IdProducto` (`IdProducto`),
  CONSTRAINT `calificacionesproductos_ibfk_1` FOREIGN KEY (`IdUsuario`) REFERENCES `usuarios` (`IdUsuario`),
  CONSTRAINT `calificacionesproductos_ibfk_2` FOREIGN KEY (`IdProducto`) REFERENCES `productos` (`IdProducto`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `calificacionproducto`
--

DROP TABLE IF EXISTS `calificacionproducto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `calificacionproducto` (
  `IdUsuario` bigint unsigned NOT NULL,
  `IdProducto` smallint unsigned NOT NULL,
  `Calificacion` tinyint unsigned NOT NULL,
  `Fecha` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  PRIMARY KEY (`IdUsuario`,`IdProducto`),
  KEY `FK_CalificacionProducto_Productos` (`IdProducto`),
  KEY `FK_CalificacionProducto_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_CalificacionProducto_Productos` FOREIGN KEY (`IdProducto`) REFERENCES `productos` (`IdProducto`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_CalificacionProducto_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_CalificacionProducto_Usuarios` FOREIGN KEY (`IdUsuario`) REFERENCES `usuarios` (`IdUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `calificacionproducto_chk_1` CHECK ((`Calificacion` between 1 and 5))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `cantones`
--

DROP TABLE IF EXISTS `cantones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `cantones` (
  `IdCanton` smallint unsigned NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  PRIMARY KEY (`IdCanton`),
  UNIQUE KEY `UK_Cantones_Nombre` (`Nombre`),
  KEY `FK_Cantones_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_Cantones_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `carrito`
--

DROP TABLE IF EXISTS `carrito`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `carrito` (
  `IdProductoCarritoUsuario` bigint unsigned NOT NULL AUTO_INCREMENT,
  `IdUsuario` bigint unsigned NOT NULL,
  `IdPresentacionProducto` int unsigned DEFAULT NULL,
  `IdPromocion` bigint unsigned DEFAULT NULL,
  `Cantidad` int unsigned NOT NULL,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  PRIMARY KEY (`IdProductoCarritoUsuario`),
  UNIQUE KEY `UQ_Carrito` (`IdUsuario`,`IdPresentacionProducto`),
  UNIQUE KEY `UQ_Carrito_Promocion` (`IdUsuario`,`IdPromocion`),
  KEY `FK_Carrito_PresentacionesProducto` (`IdPresentacionProducto`),
  KEY `FK_Carrito_Promociones` (`IdPromocion`),
  KEY `FK_Carrito_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_Carrito_PresentacionesProducto` FOREIGN KEY (`IdPresentacionProducto`) REFERENCES `presentacionesproducto` (`IdPresentacionProducto`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Carrito_Promociones` FOREIGN KEY (`IdPromocion`) REFERENCES `promociones` (`IdPromocion`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Carrito_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Carrito_Usuarios` FOREIGN KEY (`IdUsuario`) REFERENCES `usuarios` (`IdUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `CHK_Carrito_SoloUno` CHECK ((((`IdPresentacionProducto` is not null) and (`IdPromocion` is null)) or ((`IdPresentacionProducto` is null) and (`IdPromocion` is not null))))
) ENGINE=InnoDB AUTO_INCREMENT=31 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `categoriaproductos`
--

DROP TABLE IF EXISTS `categoriaproductos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `categoriaproductos` (
  `IdCategoria` tinyint unsigned NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  `IdEstado` int unsigned NOT NULL,
  PRIMARY KEY (`IdCategoria`),
  KEY `FK_CategoriaProductos_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_CategoriaProductos_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `detallespedidos`
--

DROP TABLE IF EXISTS `detallespedidos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `detallespedidos` (
  `IdDetallePedido` bigint unsigned NOT NULL AUTO_INCREMENT,
  `IdPedido` bigint unsigned NOT NULL,
  `IdPresentacionProducto` int unsigned DEFAULT NULL,
  `IdPromocion` bigint unsigned DEFAULT NULL,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  `Precio` decimal(10,2) NOT NULL,
  `NotaCliente` varchar(500) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `Cantidad` int unsigned NOT NULL DEFAULT '1',
  PRIMARY KEY (`IdDetallePedido`),
  KEY `FK_DetallesPedidos_Pedidos` (`IdPedido`),
  KEY `FK_DetallesPedidos_PresentacionesProducto` (`IdPresentacionProducto`),
  KEY `FK_DetallesPedidos_Promocion` (`IdPromocion`),
  KEY `FK_DetallesPedidos_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_DetallesPedidos_Pedidos` FOREIGN KEY (`IdPedido`) REFERENCES `pedidos` (`IdPedido`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_DetallesPedidos_PresentacionesProducto` FOREIGN KEY (`IdPresentacionProducto`) REFERENCES `presentacionesproducto` (`IdPresentacionProducto`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_DetallesPedidos_Promocion` FOREIGN KEY (`IdPromocion`) REFERENCES `promociones` (`IdPromocion`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_DetallesPedidos_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `CHK_DetallesPedidos_SoloUno` CHECK ((((`IdPresentacionProducto` is not null) and (`IdPromocion` is null)) or ((`IdPresentacionProducto` is null) and (`IdPromocion` is not null))))
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `dias`
--

DROP TABLE IF EXISTS `dias`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `dias` (
  `IdDia` tinyint unsigned NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(20) COLLATE utf8mb4_unicode_ci NOT NULL,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  PRIMARY KEY (`IdDia`),
  UNIQUE KEY `UK_Dias_Nombre` (`Nombre`),
  KEY `FK_Dias_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_Dias_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `direcciones`
--

DROP TABLE IF EXISTS `direcciones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `direcciones` (
  `IdDireccion` bigint unsigned NOT NULL AUTO_INCREMENT,
  `Detalle` varchar(200) COLLATE utf8mb4_unicode_ci NOT NULL,
  `FechaRegistro` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `DireccionPrincipal` bit(1) NOT NULL DEFAULT b'0',
  `IdUsuario` bigint unsigned NOT NULL,
  `IdDistrito` smallint unsigned NOT NULL,
  `IdCanton` smallint unsigned DEFAULT NULL,
  `IdEstado` smallint unsigned NOT NULL,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  PRIMARY KEY (`IdDireccion`),
  KEY `FK_Direcciones_Usuarios` (`IdUsuario`),
  KEY `FK_Direcciones_Distritos` (`IdDistrito`),
  KEY `FK_Direcciones_Estados` (`IdEstado`),
  KEY `FK_Direcciones_TipoEntidad` (`IdTipoEntidad`),
  KEY `FK_Direcciones_Cantones` (`IdCanton`),
  CONSTRAINT `FK_Direcciones_Cantones` FOREIGN KEY (`IdCanton`) REFERENCES `cantones` (`IdCanton`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Direcciones_Distritos` FOREIGN KEY (`IdDistrito`) REFERENCES `distritos` (`IdDistrito`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Direcciones_Estados` FOREIGN KEY (`IdEstado`) REFERENCES `estados` (`IdEstado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Direcciones_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Direcciones_Usuarios` FOREIGN KEY (`IdUsuario`) REFERENCES `usuarios` (`IdUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `distritos`
--

DROP TABLE IF EXISTS `distritos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `distritos` (
  `IdDistrito` smallint unsigned NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `IdCanton` smallint unsigned NOT NULL,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  PRIMARY KEY (`IdDistrito`),
  UNIQUE KEY `UK_Distritos_Nombre` (`Nombre`),
  KEY `FK_Distritos_Cantones` (`IdCanton`),
  KEY `FK_Distritos_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_Distritos_Cantones` FOREIGN KEY (`IdCanton`) REFERENCES `cantones` (`IdCanton`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Distritos_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `estados`
--

DROP TABLE IF EXISTS `estados`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `estados` (
  `IdEstado` smallint unsigned NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  PRIMARY KEY (`IdEstado`),
  UNIQUE KEY `UK_Estados_Descripcion` (`Descripcion`),
  KEY `FK_Estados_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_Estados_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `etiquetas`
--

DROP TABLE IF EXISTS `etiquetas`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `etiquetas` (
  `IdEtiqueta` tinyint unsigned NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  PRIMARY KEY (`IdEtiqueta`),
  KEY `FK_Etiquetas_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_Etiquetas_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `etiquetasproductos`
--

DROP TABLE IF EXISTS `etiquetasproductos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `etiquetasproductos` (
  `IdProducto` smallint unsigned NOT NULL,
  `IdEtiqueta` tinyint unsigned NOT NULL,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  PRIMARY KEY (`IdProducto`,`IdEtiqueta`),
  KEY `FK_EtiquetasProductos_Etiquetas` (`IdEtiqueta`),
  KEY `FK_EtiquetasProductos_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_EtiquetasProductos_Etiquetas` FOREIGN KEY (`IdEtiqueta`) REFERENCES `etiquetas` (`IdEtiqueta`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_EtiquetasProductos_Productos` FOREIGN KEY (`IdProducto`) REFERENCES `productos` (`IdProducto`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_EtiquetasProductos_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `gestionpuntos`
--

DROP TABLE IF EXISTS `gestionpuntos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `gestionpuntos` (
  `IdGestionPuntos` int unsigned NOT NULL AUTO_INCREMENT,
  `PuntosMinimosCompra` int NOT NULL,
  `PuntosMaximosCompra` int NOT NULL,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  `FactorPuntos` decimal(5,2) NOT NULL COMMENT 'Factor de conversión dinero→puntos',
  PRIMARY KEY (`IdGestionPuntos`),
  KEY `FK_GestionPuntos_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_GestionPuntos_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `historialestadospedidos`
--

DROP TABLE IF EXISTS `historialestadospedidos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `historialestadospedidos` (
  `IdRegistroEstado` bigint unsigned NOT NULL AUTO_INCREMENT,
  `IdPedido` bigint unsigned NOT NULL,
  `IdEstado` smallint unsigned NOT NULL,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  `FechaHora` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`IdRegistroEstado`),
  KEY `FK_HistorialEstadosPedidos_Pedidos` (`IdPedido`),
  KEY `FK_HistorialEstadosPedidos_Estados` (`IdEstado`),
  KEY `FK_HistorialEstadosPedidos_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_HistorialEstadosPedidos_Estados` FOREIGN KEY (`IdEstado`) REFERENCES `estados` (`IdEstado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_HistorialEstadosPedidos_Pedidos` FOREIGN KEY (`IdPedido`) REFERENCES `pedidos` (`IdPedido`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_HistorialEstadosPedidos_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `horariorestaurante`
--

DROP TABLE IF EXISTS `horariorestaurante`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `horariorestaurante` (
  `IdHorario` int unsigned NOT NULL AUTO_INCREMENT,
  `HoraApertura` time NOT NULL,
  `HoraCierre` time NOT NULL,
  `IdDia` tinyint unsigned NOT NULL,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  PRIMARY KEY (`IdHorario`),
  KEY `FK_HorarioRestaurante_Dias` (`IdDia`),
  KEY `FK_HorarioRestaurante_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_HorarioRestaurante_Dias` FOREIGN KEY (`IdDia`) REFERENCES `dias` (`IdDia`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_HorarioRestaurante_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `metodopago`
--

DROP TABLE IF EXISTS `metodopago`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `metodopago` (
  `IdMetodoPago` tinyint unsigned NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  PRIMARY KEY (`IdMetodoPago`),
  UNIQUE KEY `UK_MetodoPago_Descripcion` (`Descripcion`),
  KEY `FK_MetodoPago_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_MetodoPago_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `pedidos`
--

DROP TABLE IF EXISTS `pedidos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pedidos` (
  `IdPedido` bigint unsigned NOT NULL AUTO_INCREMENT,
  `NotaCliente` varchar(500) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `PuntosCanjeados` bigint unsigned DEFAULT '0',
  `TotalPagado` decimal(10,2) NOT NULL,
  `IdUsuario` bigint unsigned NOT NULL,
  `IdTipoPedido` tinyint unsigned NOT NULL,
  `IdMetodoPago` tinyint unsigned NOT NULL,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  `FechaCreacion` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `IdDireccion` bigint unsigned DEFAULT NULL,
  `CostoEnvio` decimal(10,2) NOT NULL DEFAULT '0.00',
  `TotalConPuntos` decimal(10,2) GENERATED ALWAYS AS ((`TotalPagado` + `PuntosCanjeados`)) STORED,
  PRIMARY KEY (`IdPedido`),
  KEY `FK_Pedidos_Usuarios` (`IdUsuario`),
  KEY `FK_Pedidos_TiposPedidos` (`IdTipoPedido`),
  KEY `FK_Pedidos_MetodoPago` (`IdMetodoPago`),
  KEY `FK_Pedidos_TipoEntidad` (`IdTipoEntidad`),
  KEY `FK_Direccion_Pedido` (`IdDireccion`),
  CONSTRAINT `FK_Direccion_Pedido` FOREIGN KEY (`IdDireccion`) REFERENCES `direcciones` (`IdDireccion`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Pedidos_MetodoPago` FOREIGN KEY (`IdMetodoPago`) REFERENCES `metodopago` (`IdMetodoPago`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Pedidos_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Pedidos_TiposPedidos` FOREIGN KEY (`IdTipoPedido`) REFERENCES `tipospedidos` (`IdTipoPedido`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Pedidos_Usuarios` FOREIGN KEY (`IdUsuario`) REFERENCES `usuarios` (`IdUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `presentaciones`
--

DROP TABLE IF EXISTS `presentaciones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `presentaciones` (
  `IdPresentacion` tinyint unsigned NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  PRIMARY KEY (`IdPresentacion`),
  KEY `FK_Presentacion_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_Presentacion_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `presentacionesproducto`
--

DROP TABLE IF EXISTS `presentacionesproducto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `presentacionesproducto` (
  `IdPresentacionProducto` int unsigned NOT NULL AUTO_INCREMENT,
  `IdProducto` smallint unsigned NOT NULL,
  `IdPresentacion` tinyint unsigned NOT NULL,
  `Precio` decimal(10,2) NOT NULL,
  `IdEstado` smallint unsigned NOT NULL,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  PRIMARY KEY (`IdPresentacionProducto`),
  UNIQUE KEY `UQ_PresentacionesProducto` (`IdProducto`,`IdPresentacion`),
  KEY `FK_PresentacionesProducto_Presentacion` (`IdPresentacion`),
  KEY `FK_PresentacionesProducto_Estados` (`IdEstado`),
  KEY `FK_PresentacionesProducto_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_PresentacionesProducto_Estados` FOREIGN KEY (`IdEstado`) REFERENCES `estados` (`IdEstado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_PresentacionesProducto_Presentacion` FOREIGN KEY (`IdPresentacion`) REFERENCES `presentaciones` (`IdPresentacion`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_PresentacionesProducto_Productos` FOREIGN KEY (`IdProducto`) REFERENCES `productos` (`IdProducto`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_PresentacionesProducto_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `productos`
--

DROP TABLE IF EXISTS `productos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `productos` (
  `IdProducto` smallint unsigned NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Descripcion` varchar(500) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `ImagenUrl` varchar(510) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `CanjeablePuntos` bit(1) NOT NULL DEFAULT b'0',
  `IdEstado` smallint unsigned NOT NULL,
  `IdCategoria` tinyint unsigned NOT NULL,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  PRIMARY KEY (`IdProducto`),
  KEY `FK_Productos_Estados` (`IdEstado`),
  KEY `FK_Productos_CategoriaProductos` (`IdCategoria`),
  KEY `FK_Productos_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_Productos_CategoriaProductos` FOREIGN KEY (`IdCategoria`) REFERENCES `categoriaproductos` (`IdCategoria`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Productos_Estados` FOREIGN KEY (`IdEstado`) REFERENCES `estados` (`IdEstado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Productos_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=47 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `promociones`
--

DROP TABLE IF EXISTS `promociones`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `promociones` (
  `IdPromocion` bigint unsigned NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Descripcion` varchar(500) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `FechaInicio` date NOT NULL,
  `FechaFinal` date NOT NULL,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  `IdEstado` smallint unsigned NOT NULL,
  `ImagenUrl` text COLLATE utf8mb4_unicode_ci,
  `CanjeablePuntos` bit(1) NOT NULL DEFAULT b'0' COMMENT 'Indica si la promoción genera canje de puntos',
  `Precio` decimal(10,2) NOT NULL,
  PRIMARY KEY (`IdPromocion`),
  KEY `FK_Promocion_TipoEntidad` (`IdTipoEntidad`),
  KEY `FK_Promocion_Estados` (`IdEstado`),
  CONSTRAINT `FK_Promocion_Estados` FOREIGN KEY (`IdEstado`) REFERENCES `estados` (`IdEstado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Promocion_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `refreshtokens`
--

DROP TABLE IF EXISTS `refreshtokens`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `refreshtokens` (
  `IdToken` bigint unsigned NOT NULL AUTO_INCREMENT,
  `IdUsuario` bigint unsigned NOT NULL,
  `RefreshToken` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `FechaInicio` datetime NOT NULL,
  `FechaCaducidad` datetime NOT NULL,
  `Utilizado` int NOT NULL DEFAULT '0',
  `Revocado` bit(1) NOT NULL DEFAULT b'0',
  `IdTipoEntidad` smallint unsigned NOT NULL,
  PRIMARY KEY (`IdToken`),
  KEY `FK_RefreshTokens_IdUsuario` (`IdUsuario`),
  KEY `FK_RefreshToken_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_RefreshToken_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_RefreshTokens_IdUsuario` FOREIGN KEY (`IdUsuario`) REFERENCES `usuarios` (`IdUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=372 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `restaurante`
--

DROP TABLE IF EXISTS `restaurante`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `restaurante` (
  `IdRestaurante` smallint unsigned NOT NULL AUTO_INCREMENT,
  `Nombre` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Direccion` varchar(200) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Telefono` varchar(20) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Correo` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `LogoUrl` varchar(512) COLLATE utf8mb4_unicode_ci DEFAULT NULL,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  `VersionMenu` bigint unsigned NOT NULL DEFAULT '0',
  PRIMARY KEY (`IdRestaurante`),
  KEY `FK_Restaurante_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_Restaurante_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `roles`
--

DROP TABLE IF EXISTS `roles`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `roles` (
  `IdRol` smallint unsigned NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  PRIMARY KEY (`IdRol`),
  UNIQUE KEY `UK_Roles_Descripcion` (`Descripcion`),
  KEY `FK_Roles_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_Roles_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `telefonosusuarios`
--

DROP TABLE IF EXISTS `telefonosusuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `telefonosusuarios` (
  `IdUsuario` bigint unsigned NOT NULL,
  `Telefono` varchar(20) COLLATE utf8mb4_unicode_ci NOT NULL,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  PRIMARY KEY (`IdUsuario`,`Telefono`),
  KEY `FK_TelefonosUsuarios_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_TelefonosUsuarios_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_TelefonosUsuarios_Usuarios` FOREIGN KEY (`IdUsuario`) REFERENCES `usuarios` (`IdUsuario`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tipoentidades`
--

DROP TABLE IF EXISTS `tipoentidades`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tipoentidades` (
  `IdTipoEntidad` smallint unsigned NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  PRIMARY KEY (`IdTipoEntidad`),
  UNIQUE KEY `UK_TipoEntidad_Descripcion` (`Descripcion`)
) ENGINE=InnoDB AUTO_INCREMENT=27 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tipospedidos`
--

DROP TABLE IF EXISTS `tipospedidos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tipospedidos` (
  `IdTipoPedido` tinyint unsigned NOT NULL AUTO_INCREMENT,
  `Descripcion` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  PRIMARY KEY (`IdTipoPedido`),
  UNIQUE KEY `UK_TiposPedidos_Descripcion` (`Descripcion`),
  KEY `FK_TiposPedidos_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_TiposPedidos_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `tokenrecuperacioncontrasenna`
--

DROP TABLE IF EXISTS `tokenrecuperacioncontrasenna`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `tokenrecuperacioncontrasenna` (
  `IdToken` bigint unsigned NOT NULL AUTO_INCREMENT,
  `Correo` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Token` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `FechaExpiracion` datetime NOT NULL,
  `Usado` bit(1) NOT NULL DEFAULT b'0',
  `IdTipoEntidad` smallint unsigned NOT NULL,
  PRIMARY KEY (`IdToken`),
  KEY `FK_TokenRecuperacionContrasenna_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_TokenRecuperacionContrasenna_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuarios` (
  `IdUsuario` bigint unsigned NOT NULL AUTO_INCREMENT,
  `Correo` varchar(100) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Nombre` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `Apellidos` varchar(50) COLLATE utf8mb4_unicode_ci NOT NULL,
  `PuntosAcumulados` bigint unsigned NOT NULL DEFAULT '0',
  `Contrasenna` varchar(255) COLLATE utf8mb4_unicode_ci NOT NULL,
  `IdRol` smallint unsigned NOT NULL,
  `IdEstado` smallint unsigned NOT NULL,
  `IdTipoEntidad` smallint unsigned NOT NULL,
  `FechaCreacion` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`IdUsuario`),
  UNIQUE KEY `UK_Usuarios_Correo` (`Correo`),
  KEY `FK_Usuarios_Roles` (`IdRol`),
  KEY `FK_Usuarios_Estados` (`IdEstado`),
  KEY `FK_Usuarios_TipoEntidad` (`IdTipoEntidad`),
  CONSTRAINT `FK_Usuarios_Estados` FOREIGN KEY (`IdEstado`) REFERENCES `estados` (`IdEstado`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Usuarios_Roles` FOREIGN KEY (`IdRol`) REFERENCES `roles` (`IdRol`) ON DELETE RESTRICT ON UPDATE RESTRICT,
  CONSTRAINT `FK_Usuarios_TipoEntidad` FOREIGN KEY (`IdTipoEntidad`) REFERENCES `tipoentidades` (`IdTipoEntidad`) ON DELETE RESTRICT ON UPDATE RESTRICT
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-07-27 18:00:00
