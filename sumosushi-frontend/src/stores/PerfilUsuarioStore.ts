import { writable, get } from 'svelte/store';
import { PerfilCompletoUsuarioModel } from '../models/usuario/PerfilCompletoModel';
import { obtenerPerfilService } from '../services/UsuarioService';


export const perfil = writable<PerfilCompletoUsuarioModel>(new PerfilCompletoUsuarioModel());

export function estaPerfilVacio(): boolean {
    return get(perfil).Nombre === '';
}

export async function cargarPerfil(token: string, refrescarDirecciones: boolean = false): Promise<void> {
    if (estaPerfilVacio() || refrescarDirecciones) {
        const perfilCargado = await obtenerPerfilService(token);
        perfil.set(perfilCargado);        
    }
}

export function actualizarPerfil(model: PerfilCompletoUsuarioModel) {
    perfil.set(model);
}

