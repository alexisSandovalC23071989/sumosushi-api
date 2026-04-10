import { defineConfig } from 'vite'
import { svelte } from '@sveltejs/vite-plugin-svelte'
import { VitePWA } from 'vite-plugin-pwa'

export default defineConfig({
  plugins: [
    svelte(),
    VitePWA({
      strategies: 'injectManifest',
      srcDir: 'src',
      filename: 'custom-sw.js',
      registerType: 'autoUpdate',
      includeAssets: ['favicon.svg'],
      manifest: {
        name: 'SumoSushiApp',
        short_name: 'SumoSushi',
        start_url: '/',
        display: 'standalone',
        background_color: '#241E20',
        theme_color: '#8B0201',
        icons: [
          { src: '/icon-192x192.png', sizes: '192x192', type: 'image/png' },
          { src: '/icon-512x512.png', sizes: '512x512', type: 'image/png' }
        ]
      },
      devOptions: {
        enabled: true
      }
    })
  ],
  server: {
    port: 5176
  }
})
