// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function playHangul(fileCode) {
    try {
        if (fileCode === undefined || fileCode === null || fileCode.length === 0) {
            return;
        }

        playAudio(`/korean_sounds/${fileCode}.mp3`);
    } catch {
        console.log('Error playing file.');
    }
}

function playPinyin(fileCode) {
    try {
        if (fileCode === undefined || fileCode === null || fileCode.length === 0) {
            return;
        }

        playAudio(`/mandarin_sounds/${fileCode}.mp3`);
    } catch {
        console.log('Error playing file.');
    }
}

function playAudio(filename) {
    const audio = new Audio(filename);

    if (audio !== undefined && audio !== null) {
        audio.currentTime = 0;
        audio.play();
    }
}

class HistoryDict {
    constructor(storageKey = 'searchHistory', maxSize = 5) { // MAXV - set back maxSize to 20
        this.storageKey = storageKey;
        this.maxSize = maxSize;
    }

    _load() {
        try {
            return JSON.parse(localStorage.getItem(this.storageKey)) || {};
        } catch {
            return {};
        }
    }

    _save(dict) {
        localStorage.setItem(this.storageKey, JSON.stringify(dict));
    }

    _evictLast(dict) {
        const keys = Object.keys(dict);

        if (keys.length < this.maxSize) return;

        delete dict[keys[keys.length - 1]];
    }

    set(key, { ideogram, unicode, reading }) {
        const dict = this._load();

        if (!(key in dict)) {
            this._evictLast(dict);
        }

        dict[key] = {
            ideogram: ideogram,
            unicode: unicode,
            reading: reading
        };

        this._save(dict);
    }

    get(key) {
        return this._load()[key] ?? null;
    }

    remove(key) {
        const dict = this._load();

        delete dict[key];

        this._save(dict);
    }

    has(key) {
        return key in this._load();
    }

    size() {
        return Object.keys(this._load()).length;
    }

    all() {
        return this._load();
    }

    keys() {
        return Object.keys(this._load());
    }

    values() {
        return Object.values(this._load());
    }

    clear() {
        localStorage.removeItem(this.storageKey);
    }
}

/*
// Iterate all entries
for (const [key, { name, value, timestamp }] of Object.entries(dict.all())) {
  console.log(`${name} (${key}): ${value} — set at ${timestamp}`);
}
*/
