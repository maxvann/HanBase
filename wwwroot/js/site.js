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
    constructor(storageKey = 'searchHistory', maxSize = 20) {
        this.storageKey = storageKey;
        this.maxSize = maxSize;
    }

    _load() {
        try {
            const raw = JSON.parse(localStorage.getItem(this.storageKey));
            return raw || { entries: {}, order: [] };
        } catch {
            return { entries: {}, order: [] };
        }
    }

    _save(dict) {
        localStorage.setItem(this.storageKey, JSON.stringify(dict));
    }

    _evictFirst(dict) {
        if (dict.order.length < this.maxSize) return;

        const key = dict.order[0];

        delete dict.entries[key];

        dict.order.shift();
    }

    set(key, { ideogram, unicode, reading }) {
        const dict = this._load();

        if (key in dict.entries) {
            dict.entries[key] = {
                ideogram: ideogram,
                unicode: unicode,
                reading: reading
            };
        } else {
            this._evictFirst(dict);

            dict.entries[key] = {
                ideogram: ideogram,
                unicode: unicode,
                reading: reading
            };

            dict.order.push(key);
        }

        this._save(dict);
    }

    get(key) {
        return this._load().entries[key] ?? null;
    }

    remove(key) {
        const dict = this._load();

        delete dict.entries[key];

        dict.order = dict.order.filter(k => k !== key);

        this._save(dict);
    }

    has(key) {
        return key in this._load().entries;
    }

    size() {
        return this._load().order.length;
    }

    all() {
        return this._load().entries;
    }

    keys() {
        return this._load().order;
    }

    values() {
        const data = this._load();

        return data.order.map(key => [key, data.entries[key]]);
    }

    clear() {
        localStorage.removeItem(this.storageKey);
    }
}
