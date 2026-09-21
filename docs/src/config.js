// Centralized configuration and tile source definitions
export const MAP_CONFIG = {
    engine: "leaflet",
    source: "maptiler",
    // Required only by MapTiler. Keep this configurable.
    maptilerKey: "gh05Ijqx0b2irR199Jfn",
    center: [35.6602777778, 139.8662222222],
    zoom: 20
};

export const TILE_SOURCES = {
    osm: {
        url: "https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png",
        options: {
            maxZoom: 20,
            attribution:
                '&copy; <a href="https://www.openstreetmap.org/copyright">' +
                'OpenStreetMap</a>'
        }
    },
    maptiler: {
        getUrl() {
            return (
                "https://api.maptiler.com/maps/aquarelle-v4/" +
                "{z}/{x}/{y}.png?key=" +
                MAP_CONFIG.maptilerKey
            );
        },
        options: {
            maxZoom: 20,
            attribution:
                '&copy; <a href="https://www.maptiler.com/copyright/">' +
                'MapTiler</a> ' +
                '&copy; <a href="https://www.openstreetmap.org/copyright">' +
                'OpenStreetMap</a>'
        }
    }
};
