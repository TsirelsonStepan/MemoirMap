import { MAP_CONFIG, TILE_SOURCES } from './src/config.js';
import { createMap } from './src/map.js';

// Expose a factory compatible with Alpine.js x-data="mapApp()".
// Exporting and attaching to window keeps the public API available
// to the HTML while keeping internal modules separated for testing
// and extension.
function mapApp() {
    return {
        map: null,

        extensions: [
            { id: 'search', title: 'Search', icon: '⌕', kind: 'sidebar' },
            { id: 'info', title: 'Information', icon: 'ⓘ', kind: 'popup' },
            { id: 'custom', title: 'Custom extension', icon: '＋', kind: 'popup' }
        ],

        windows: [],

        init() {
            // Lazily create the map so the DOM is ready
            if (!this.map) {
                this.map = createMap('map', MAP_CONFIG, TILE_SOURCES);

                // Allow other UI to finish layout before invalidating size
                setTimeout(() => {
                    if (this.map && typeof this.map.invalidateSize === 'function') {
                        this.map.invalidateSize();
                    }
                }, 0);
            }
        },

        openExtension(extension) {
            const exists = this.windows.find(w => w.id === extension.id);
            if (exists) return;

            const defaultWidth = extension.kind === 'sidebar' ? 360 : 420;

            this.windows.push({
                id: extension.id,
                title: extension.title,
                kind: extension.kind,
                width: extension.width || defaultWidth
            });
        },

        closeWindow(id) {
            this.windows = this.windows.filter(w => w.id !== id);
        },

        windowStyle(window) {
            return {
                width: `${window.width}px`
            };
        }
    };
}

// Attach to the global scope so Alpine's x-data can call mapApp()
window.mapApp = mapApp;

export { mapApp };
