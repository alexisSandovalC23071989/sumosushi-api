import { writable } from 'svelte/store';

let audio: HTMLAudioElement | null = null;
let desbloqueado = false;
let estaSonando = false;

const { subscribe, set } = writable(false);

export const audioHabilitado = {
  subscribe,

  inicializar() {
    if (!audio) {
      audio = new Audio('../../public/sounds/notificacionPedido.mp3');
      audio.preload = 'auto';
      audio.loop = true;
    }

    set(true);

    // Solo intenta desbloquear si aún no se ha hecho
    if (!desbloqueado) {
      audio.play().then(() => {
        audio?.pause();
        desbloqueado = true;
        console.log("Audio desbloqueado");
      }).catch(err => {
        console.warn("No se pudo desbloquear el audio:", err);
      });
    } else {
      console.log("Audio ya estaba desbloqueado");
    }
  },

  sonar() {
    if (audio && desbloqueado) {
      if (!estaSonando) {
        audio.currentTime = 0;
        audio.play();
        estaSonando = true;
        console.log("Audio sonando");
      }
    }
  },

  detener() {
    if (audio) {
      audio.pause();
      audio.currentTime = 0;
      estaSonando = false;
      console.log("Audio detenido");
    }
  },

  deshabilitar() {
    audio?.pause();
    audio = null;
    desbloqueado = false;
    estaSonando = false;
    set(false);
    console.log("Audio deshabilitado");
  },
};
