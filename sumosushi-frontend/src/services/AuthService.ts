// MODELS
import type { RespuestaModel } from "../models/RespuestaModel";
import { SolicitudRecuperacionContrasennaModel } from "../models/auth/SolicitudRecuperacionContrasennaModel";
import { RecuperacionContrasennaModel } from "../models/auth/RecuperacionContrasennaModel";
import { LoginModel } from "../models/auth/LoginModel";

// APIs
import { inicioSesion, refreshAccessTokenApi, solicitarRecuperacionContrasennaApi, cerrarSesionApi } from "../api/AuthApi";
import { recuperarContrasennaApi } from "../api/AuthApi";

// Utils
import { responseEstatusOk } from "./utils/ResponseStatus";

// Stores
import { cargarInfoSesion, infoSesion } from "../stores/AuthStores";
import { get } from 'svelte/store';

// Rutas
import { goToLogin, goToRecuperacionContrasenna, goToHome } from "../routes/Routes";

/**
 * Procesamos inicio de sesion con Tokens, 
 * Se ejecuta exclusivamente cuando el usuario no tiene un refresh token 
 * en HttpOnly cookie.
 */
export async function inicioSesionService(usuario: LoginModel): Promise<void> {
    try {
        // Validacion de datos del formulario
        const errores = usuario.validar();
        if (errores.length > 0){
            alert(errores.join("\n"));
            return;
        }

        // Verificaciones
        const response = await inicioSesion(usuario);
        if (!await responseEstatusOk(response)) {
            return;
        }  

        const respuesta: RespuestaModel = await response.json();
        if (!respuesta.exito){
            alert(respuesta.mensaje)
            return;
        }
        if (!respuesta.datos){
            alert("Error al obtener los tokens.")
            return;
        }                
        
        // Gestion de inicio de sesion
        localStorage.setItem('accessToken', respuesta.datos.accesToken);
        cargarInfoSesion();
        alert("Inicio de sesión exitoso");
        goToHome();

    } catch (error: any) {
        alert("Error de conexión. Por favor verifica tu conexión e intenta de nuevo.");
    }
}
/**
 * Funcion para solicitar un nuevo access token, esta se ejecuta
 * sin que el usuario se de cuenta, utilizada cuando en llamadas API,
 * se deteca que el access token ha expirado.
 */
export async function refreshAccesTokenService(): Promise<boolean> {
    try {
        const accessToken = localStorage.getItem('accessToken');
        if (!accessToken) {
            await cargarInfoSesion();
            goToLogin();
            alert("Debes iniciar sesión.");
            return false;
        }

        const response = await refreshAccessTokenApi(accessToken);
        if (!await responseEstatusOk(response)) {
            await cargarInfoSesion();
            return false;
        }

        const respuesta = await response.json();
        if (!respuesta.exito || !respuesta.datos?.accesToken) {
            alert(respuesta.mensaje);
            localStorage.removeItem('accessToken');
            await cargarInfoSesion();
            goToLogin();
            alert("Debes iniciar sesión.");
            return false;
        }

        localStorage.setItem('accessToken', respuesta.datos.accesToken);        
        await cargarInfoSesion();
        return true;

    } catch (error) {
        alert("Error de conexión.");
        await cargarInfoSesion();
        return false;
    }
}
export async function solicitudRecuperacionContrasennaService(correo: SolicitudRecuperacionContrasennaModel){
    try {
        const errores = correo.validar();
        if (errores.length > 0){
            alert(errores.join("\n"));
            return;
        }

        const response : Response = await solicitarRecuperacionContrasennaApi(correo);
        if (!await responseEstatusOk(response)) {
            return;
        }  
        const respuesta: RespuestaModel = await response.json();
        if (!respuesta.exito){
            alert(respuesta.mensaje)
            return;
        }  

        alert(respuesta.mensaje);
        goToRecuperacionContrasenna();

    } catch (error) {
        alert("Error de conexión. Por favor verifica tu conexión e intenta de nuevo.");
    }
}
export async function recuperacionContrasennaService(recuperacionContrasenna: RecuperacionContrasennaModel){
    try {
        const errores = recuperacionContrasenna.validar();
        if (errores.length > 0){
            alert(errores.join("\n"));
            return;
        }

        const response : Response = await recuperarContrasennaApi(recuperacionContrasenna);
        if (!await responseEstatusOk(response)) {
            return;
        }  
        const respuesta: RespuestaModel = await response.json();
        if (!respuesta.exito){
            alert(respuesta.mensaje)
            return;
        }      
        alert(respuesta.mensaje);
        goToLogin();

    } catch (error) {
        alert("Error de conexión. Por favor verifica tu conexión e intenta de nuevo.");
    }
}
export async function cerrarSesionService(){
    try {
        const response : Response = await cerrarSesionApi();
        if (!await responseEstatusOk(response)) {
            return;
        }  
        localStorage.removeItem('accessToken');
        cargarInfoSesion();
        goToLogin();
    } catch (error) {
        alert("Error de conexión. Por favor verifica tu conexión e intenta de nuevo.");
    }
}
export async function obtenerAccesToken(): Promise<string | null> {
    try {
        let sesion = get(infoSesion);
        
        // Si el token es válido, lo retornamos
        if (sesion.esTokenValido) {
            return sesion.Jwt;
        }
        // Si no es válido, intentamos refrescarlo
        if (!await refreshAccesTokenService()){
            return null;
        };  
        // Obtenemos el estado actualizado
        sesion = get(infoSesion);
        if (!sesion.esTokenValido) {
            return null;
        }   
             
        return sesion.Jwt;
    } catch (error) {
        console.error("Error al obtener el token:", error);
        return null;
    }
}