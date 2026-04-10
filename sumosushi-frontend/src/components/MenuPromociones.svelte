<script lang="ts">
    import { menu } from "../stores/MenuStore";
    import { onMount, onDestroy } from 'svelte';
  
    let currentIndex = 0;
    let actualItemsVisibles = 1;
    const intervaloMilisegundos = 4000;
  
    let intervalo: ReturnType<typeof setInterval>;
  
    // Promociones reactivas desde el store
    $: promociones = $menu?.Promociones ?? [];
    $: itemsVisibles = promociones.length;
  
    // Recalcular cantidad de ítems visibles dependiendo del tamaño de pantalla
    function updateVisibleItems(): void {
      if (window.innerWidth < 768) {
        actualItemsVisibles = 1;
      } else if (window.innerWidth < 992) {
        actualItemsVisibles = Math.min(2, itemsVisibles);
      } else {
        actualItemsVisibles = Math.min(3, itemsVisibles);
      }
  
      if (currentIndex > itemsVisibles - actualItemsVisibles) {
        currentIndex = Math.max(0, itemsVisibles - actualItemsVisibles);
      }
    }
  
    onMount(() => {
      updateVisibleItems();
      window.addEventListener('resize', updateVisibleItems);
  
      intervalo = setInterval(() => {
        if (itemsVisibles <= actualItemsVisibles) return;
        if (currentIndex < itemsVisibles - actualItemsVisibles) {
          currentIndex++;
        } else {
          currentIndex = 0;
        }
      }, intervaloMilisegundos);
  
      return () => {
        window.removeEventListener('resize', updateVisibleItems);
        clearInterval(intervalo);
      };
    });
  
    onDestroy(() => {
      clearInterval(intervalo);
    });
  
    function nextSlide() {
      if (currentIndex < itemsVisibles - actualItemsVisibles) {
        currentIndex++;
      }
    }
  
    function prevSlide() {
      if (currentIndex > 0) {
        currentIndex--;
      }
    }
  
    $: offsetX = `translateX(-${(currentIndex * 100) / actualItemsVisibles}%)`;
    $: itemWidth = `${100 / actualItemsVisibles}%`;
  </script>
  
  {#if itemsVisibles > 0}
    <div class="carousel">
      <div class="carousel__container">
        <div class="carousel__track" style="transform: {offsetX}">
          {#each promociones as promocion}
            <!-- {#if promocion.FechaFinal > new Date().toISOString() && promocion.FechaInicio < new Date().toISOString()} -->
            <div class="carousel__item" style="width: {itemWidth}">
              <a href="/producto/{promocion.IdPromocion}">
                <img class="carousel__image" src={promocion.ImagenUrl} alt="Promocion {promocion.Descripcion}">
              </a>
            </div>
            <!-- {/if} -->
          {/each}
        </div>
      </div>
  
      <!-- Indicadores -->
      <div class="indicadores">
        {#each Array(Math.max(1, itemsVisibles - actualItemsVisibles + 1)).fill(0) as _, index}
          <div class="indicador {index === currentIndex ? 'activo' : ''}"></div>
        {/each}
      </div>
  
      <!-- Botones -->
      {#if itemsVisibles > actualItemsVisibles}
        <div class="carousel__controls">
          <button class="carousel__button carousel__button--prev" on:click={prevSlide} disabled={currentIndex === 0}>
            &lt;
          </button>
          <button class="carousel__button carousel__button--next" on:click={nextSlide}
            disabled={currentIndex >= itemsVisibles - actualItemsVisibles}>
            &gt;
          </button>
        </div>
      {/if}
    </div>
  {/if}
  
  <style>
    .carousel {
      position: relative;
      width: 100%;
      margin: 0 auto;
    }
  
    .carousel__container {
      overflow: hidden;
    }
  
    .carousel__track {
      display: flex;
      transition: transform 0.3s ease-out;
    }
  
    .carousel__item {
      flex-shrink: 0;
      padding: 0 0px;
    }
  
    .carousel__image {
      width: 100%;
      height: auto;
      border-radius: 4px;
      display: block;
    }
  
    .carousel__controls {
      display: flex;
      justify-content: space-between;
      position: absolute;
      top: 50%;
      left: 0;
      right: 0;
      transform: translateY(-50%);
      pointer-events: none;
    }
  
    .carousel__button {
      background: rgba(0, 0, 0, 0.5);
      color: white;
      border: none;
      border-radius: 50%;
      width: 40px;
      height: 40px;
      font-size: 18px;
      cursor: pointer;
      pointer-events: auto;
    }
  
    .carousel__button:disabled {
      opacity: 0.3;
      cursor: not-allowed;
    }
  
    .carousel__button--prev {
      margin-left: 10px;
    }
  
    .carousel__button--next {
      margin-right: 10px;
    }
  
    .indicadores {
      display: flex;
      justify-content: center;
      margin-top: 10px;
      gap: 15px;
    }
  
    .indicador {
      width: 10px;
      height: 10px;
      border-radius: 50%;
      background-color: var(--color-principal);
      transition: background-color 0.3s;
    }
  
    .activo {
      background-color: var(--color-secundario);
    }
  </style>
  