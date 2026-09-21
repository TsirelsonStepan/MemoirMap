// Map creation and tile layer wiring
export function createMap(containerId, MAP_CONFIG, TILE_SOURCES) {
    if (MAP_CONFIG.engine !== "leaflet") {
        console.error("Unsupported map engine:", MAP_CONFIG.engine);
        return null;
    }

    // Create map with a conservative set of interactions that can be extended
    const map = L.map(containerId, {
        center: MAP_CONFIG.center,
        zoom: MAP_CONFIG.zoom,

        dragging: true,
        scrollWheelZoom: true,
        doubleClickZoom: true,
        boxZoom: true,
        keyboard: true,
        touchZoom: true,
        zoomControl: true,
        attributionControl: true
    });

    const source = TILE_SOURCES[MAP_CONFIG.source];

    if (!source) {
        console.error("Unknown tile source:", MAP_CONFIG.source);
        return map;
    }

    const url = typeof source.getUrl === "function" ? source.getUrl() : source.url;

    L.tileLayer(url, source.options).addTo(map);

    // expose a small helper API to allow future extensions to interact with
    // the map instance without reaching into internals
    map.__helpers = {
        setCenter: (c, z) => map.setView(c, z || map.getZoom()),
        addMarker: (latlng, options = {}) => L.marker(latlng, options).addTo(map)
    };

    return map;
}
