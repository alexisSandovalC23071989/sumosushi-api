import { API_BASE } from '../Config';

//Modelos
import type { RegistroUsuarioModel } from '../models/auth/RegistroUsuarioModel';
import type { DireccionCompletaModel } from '../models/direccion/DireccionCompletaModel';
import type { NuevaDireccionModel } from '../models/direccion/NuevaDireccionModel';
import type { ActualizarRolModel } from '../models/usuario/ActualizarRolModel';
import type { PerfilCompletoUsuarioModel } from '../models/usuario/PerfilCompletoModel';
import type { UsuarioFormModel } from '../models/usuario/UsuarioFormModel';

export async function registrarUsuarioApi(usuario: RegistroUsuarioModel): Promise<Response> {
    return await fetch(`${API_BASE}/Auth/RegistroUsuario`, {
        method: 'POST',
        credentials: 'include',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(usuario)
    });
};
export async function obtenerPerfilApi(token: string): Promise<Response> {
    return await fetch(`${API_BASE}/Usuario/ObtenerPerfilUsuario`, {
        method: 'GET',
        headers: { 
            'Content-Type': 'application/json', 
            'Authorization': `Bearer ${token}` }
    });
};
export async function obtenerColaboradoresApi(token: string): Promise<Response> {
    return await fetch(`${API_BASE}/Usuario/ObtenerColaboradoresRestaurante`, {
        method: 'GET',
        headers: { 
            'Content-Type': 'application/json', 
            'Authorization': `Bearer ${token}` }
    });
};
export async function actualizarRolUsuarioApi(token: string, model: ActualizarRolModel): Promise<Response> {
    return await fetch(`${API_BASE}/Usuario/ActualizarRolUsuario`, {
        method: 'PUT',
        headers: { 
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}` },
        body: JSON.stringify(model)
    });
};
export async function adminDeshabilitarCuenta(token : string, model: UsuarioFormModel): Promise<Response> {
    return await fetch(`${API_BASE}/Usuario/AdminDesactivarCuenta`, {
        method: 'PUT',
        headers: { 
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}` },
        body: JSON.stringify({ correo: model.Correo })
    });
};
export async function adminRehabilitarCuenta(token : string, model: UsuarioFormModel): Promise<Response> {
    return await fetch(`${API_BASE}/Usuario/AdminReactivarCuenta`, {
        method: 'PUT',
        headers: { 
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}` },
        body: JSON.stringify({ correo: model.Correo })
    });
};
export async function obtenerUsuarioPorCorreoApi(token: string, correo: string): Promise<Response> {
    return await fetch(`${API_BASE}/Usuario/ObtenerUsuarioPorCorreo`, {
        method: 'POST',
        headers: { 
            'Content-Type': 'application/json', 
            'Authorization': `Bearer ${token}` },
        body: JSON.stringify({ correo: correo })
    });
};
export async function obtenerUsuariosDeshabilitadosApi(token: string): Promise<Response> {
    return await fetch(`${API_BASE}/Usuario/ObtenerUsuariosDeshabilitados`, {
        method: 'GET',
        headers: { 
            'Content-Type': 'application/json', 
            'Authorization': `Bearer ${token}` }
    });
};
export async function actualizarPerfilApi(token: string, model: PerfilCompletoUsuarioModel): Promise<Response> {
    return await fetch(`${API_BASE}/Usuario/ActualizarPerfilUsuario`, {
        method: 'PUT',
        headers: { 
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}` },
        body: JSON.stringify({
            nombre: model.Nombre,
            apellidos: model.Apellidos
        })
    });
};
export async function guardarTelefonoApi(token: string, telefono: string): Promise<Response> {
    return await fetch(`${API_BASE}/Telefono/RegistrarTelefono`, {
        method: 'POST',
        headers: { 
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}` },
        body: JSON.stringify({ telefono: telefono })
    });
};
export async function eliminarTelefonoApi(token: string, telefono: string): Promise<Response> {
    return await fetch(`${API_BASE}/Telefono/EliminarTelefono`, {
        method: 'DELETE',
        headers: { 
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}` },
        body: JSON.stringify({ telefono: telefono })
    });
};
export async function guardarDireccionUsuarioApi(token: string, model: NuevaDireccionModel): Promise<Response> {
    return await fetch(`${API_BASE}/Direccion/RegistrarDireccion`, {
        method: 'POST',
        headers: { 
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}` },
        body: JSON.stringify({
            detalle: model.Detalle,
            idCanton: model.IdCanton,
            idDistrito: model.IdDistrito
        })
    });
}
export async function eliminarDireccionUsuarioApi(token: string, idDireccion: number): Promise<Response> {
    return await fetch(`${API_BASE}/Direccion/Deshabilitar`, {
        method: 'DELETE',
        headers: { 
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}` },
        body: JSON.stringify({ idDireccion: idDireccion })
    });
}

