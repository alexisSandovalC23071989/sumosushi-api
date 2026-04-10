import { writable, get } from 'svelte/store';
import { MenuCompletoModel } from '../models/menu/MenuCompletoModel';
import { obtenerMenuCompletoService, obtenerVersionService } from '../services/MenuService';

export let menu = writable<MenuCompletoModel>(new MenuCompletoModel(0, [], []));

export async function cargarMenu(): Promise<void> {
    const menuLocalStr = localStorage.getItem('menu');
    const menuLocal: MenuCompletoModel | null = menuLocalStr ? JSON.parse(menuLocalStr) : null;

    if (menuLocal) {
        menu.set(menuLocal);
        console.log(get(menu));
        try {
            const versionRemota = await obtenerVersionService();

            if (menuLocal.VersionMenu !== versionRemota) {
                // Si la versión cambió, se actualiza
                const menuActualizado = await obtenerMenuCompletoService();
                menu.set(menuActualizado);
                localStorage.setItem('menu', JSON.stringify(menuActualizado));
            }
        } catch (error) {
            console.warn('No se pudo verificar la versión del menú:', error);
        }
    } else {
        try {
            const menuDesdeAPI = await obtenerMenuCompletoService();
            menu.set(menuDesdeAPI);
            localStorage.setItem('menu', JSON.stringify(menuDesdeAPI));
        } catch (error) {
            console.error('No se pudo cargar el menú desde la API:', error);
        }
    }
}

export function estaMenuVacio(): boolean {
    return get(menu).VersionMenu === 0;
}
