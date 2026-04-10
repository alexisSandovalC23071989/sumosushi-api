import { writable} from 'svelte/store';
import { AccessTokenClaimsModel } from '../models/auth/AccesTokenClaimsModel';
import { LOCAL_STORAGE_ACCESS_TOKEN } from '../Config';

export const infoSesion = writable <AccessTokenClaimsModel>(new AccessTokenClaimsModel());

export async function cargarInfoSesion(): Promise<boolean> {
  const token = localStorage.getItem(LOCAL_STORAGE_ACCESS_TOKEN);
  if (!token) {
    infoSesion.update(claims => {
      claims.limpiar();
      return claims;
    });
    return false;
  } 

  const claims = new AccessTokenClaimsModel(token);
  infoSesion.set(claims);
  return claims.estaAutenticado;
}
