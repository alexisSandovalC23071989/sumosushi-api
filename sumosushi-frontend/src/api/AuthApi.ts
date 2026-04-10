// Config
import { API_BASE } from '../Config';

// Modelos
import type { SolicitudRecuperacionContrasennaModel } from '../models/auth/SolicitudRecuperacionContrasennaModel';
import type { LoginModel } from '../models/auth/LoginModel';
import type { RecuperacionContrasennaModel } from '../models/auth/RecuperacionContrasennaModel';

const RUTAS_API = {
  INICIO_SESION: `${API_BASE}/Auth/InicioSesion`,
  REFRESH_TOKEN: `${API_BASE}/Auth/RefreshAccessToken`,
  SOLICITAR_RECUPERACION: `${API_BASE}/Auth/SolicitarRecuperarContrasenna`,
  RECUPERAR_CONTRASENNA: `${API_BASE}/Auth/RecuperarContrasenna`,
  CERRAR_SESION: `${API_BASE}/Auth/CerrarSesion`,
};

export async function inicioSesion(usuario: LoginModel) : Promise<Response> {
  return await fetch(`${RUTAS_API.INICIO_SESION}`, {
    method: 'POST',
    credentials: 'include',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(usuario)
  });
};

export async function refreshAccessTokenApi(token: string) : Promise<Response> {
  return await fetch(`${RUTAS_API.REFRESH_TOKEN}`, {
    method: 'POST',
    credentials: 'include',
    headers: { 
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`},    
  })
};

export async function solicitarRecuperacionContrasennaApi(formulario: SolicitudRecuperacionContrasennaModel) : Promise<Response> {
    return await fetch(`${RUTAS_API.SOLICITAR_RECUPERACION}`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(formulario)
    });
};

export async function recuperarContrasennaApi(recuperacionContrasenna: RecuperacionContrasennaModel) : Promise<Response> {
    return await fetch(`${RUTAS_API.RECUPERAR_CONTRASENNA}`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(recuperacionContrasenna)
    });
};

export async function cerrarSesionApi() : Promise<Response> {
    return await fetch(`${RUTAS_API.CERRAR_SESION}`, {
        method: 'POST',
        credentials: 'include',
    });
};