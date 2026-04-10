import Home from './Home.svelte';
import Login from './Login.svelte';
import RegistroUsuario from './RegistroUsuario.svelte';
import SolicitarRecuperarContrasenna from './SolicitarRecuperarContrasenna.svelte';
import RecuperacionContrasenna from './RecuperacionContrasenna.svelte';
import PerfilUsuario from './PerfilUsuario.svelte';
import GestionHorario from './GestionHorario.svelte';
import GestionInfoRest from './GestionInfoRest.svelte';
import GestionCuentas from './GestionCuentas.svelte';
import GestionMenu from './GestionMenu.svelte';
import { push } from 'svelte-spa-router';
import Carrito from './Carrito.svelte';
import CheckOut from './CheckOut.svelte';
import MisPuntos from './MisPuntos.svelte';
import GestionPedidos from './GestionPedidos.svelte';
import SeguimientoPedido from './SeguimientoPedido.svelte';

// Rutas centralizadas
export const ROUTE_PATHS = {
  HOME: '/',
  LOGIN: '/login',
  REGISTRO_USUARIO: '/registroUsuario',
  SOLICITAR_RECUPERAR_CONTRASENNA: '/solicitarRecuperarContrasenna',
  RECUPERACION_CONTRASENNA: '/recuperarContrasenna',
  PERFIL_USUARIO: '/perfilUsuario',
  GESTION_HORARIO: '/gestionHorario',
  GESTION_INFO_REST: '/gestionInfoRest',
  GESTION_CUENTAS: '/gestionCuentas',
  GESTION_MENU: '/gestionMenu',
  CARRITO: '/carrito',
  CHECKOUT: '/checkout',
  MIS_PUNTOS: '/misPuntos',
  GESTION_PEDIDOS: '/gestionPedidos',
  SEGUIMIENTO_PEDIDO: '/seguimientoPedido',
  NOT_FOUND: '*'
} as const;

// Rutas para el router
export const routes = {
  [ROUTE_PATHS.HOME]: Home,
  [ROUTE_PATHS.LOGIN]: Login,
  [ROUTE_PATHS.REGISTRO_USUARIO]: RegistroUsuario,
  [ROUTE_PATHS.SOLICITAR_RECUPERAR_CONTRASENNA]: SolicitarRecuperarContrasenna,
  [ROUTE_PATHS.RECUPERACION_CONTRASENNA]: RecuperacionContrasenna,
  [ROUTE_PATHS.PERFIL_USUARIO]: PerfilUsuario,
  [ROUTE_PATHS.GESTION_HORARIO]: GestionHorario,
  [ROUTE_PATHS.GESTION_INFO_REST]: GestionInfoRest,
  [ROUTE_PATHS.GESTION_CUENTAS]: GestionCuentas,
  [ROUTE_PATHS.GESTION_MENU]: GestionMenu,
  [ROUTE_PATHS.CARRITO]: Carrito,
  [ROUTE_PATHS.CHECKOUT]: CheckOut,
  [ROUTE_PATHS.MIS_PUNTOS]: MisPuntos,
  [ROUTE_PATHS.GESTION_PEDIDOS]: GestionPedidos,
  [ROUTE_PATHS.SEGUIMIENTO_PEDIDO]: SeguimientoPedido,
  [ROUTE_PATHS.NOT_FOUND]: Home
};

// Funciones para navegación programática
export const goToHome = () => push(ROUTE_PATHS.HOME);
export const goToLogin = () => push(ROUTE_PATHS.LOGIN);

export const goToRecuperacionContrasenna = () => push(ROUTE_PATHS.RECUPERACION_CONTRASENNA);
export const goToGestionInfoRest = () => push(ROUTE_PATHS.GESTION_INFO_REST);

export const goToCheckOut = () => push(ROUTE_PATHS.CHECKOUT);

export const goToSeguimientoPedido = () => push(ROUTE_PATHS.SEGUIMIENTO_PEDIDO);

export const goToCarrito = () => push(ROUTE_PATHS.CARRITO);



