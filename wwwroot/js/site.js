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

function truncate(str, maxLength = 20) {
    const ellipsis = '…';
    return str.length > maxLength
        ? str.slice(0, maxLength - ellipsis.length) + ellipsis
        : str;
}

function showToast() {
    const toast = document.getElementById('clipboard_toast');

    toast.style.visibility = "visible";

    const timer = window.setTimeout(() => {
        toast.style.visibility = "hidden";
        window.clearTimeout(timer);
    }, 4500);
}

class HistoryDict {
    constructor(storageKey = 'searchHistory', maxSize = 10) {
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

    _evict(dict) {
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
                reading: truncate(reading)
            };
        } else {
            this._evict(dict);

            dict.entries[key] = {
                ideogram: ideogram,
                unicode: unicode,
                reading: truncate(reading)
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

    ideograms() {
        const ideograms = [];
        const dict = this._load();
        const keys = dict.order;

        keys.forEach((key) => {
            ideograms.push(dict.entries[key].ideogram);
        });

        return ideograms.join("");
    }

    keys() {
        return this._load().order;
    }

    values() {
        const dict = this._load();

        return dict.order.map(key => [key, dict.entries[key]]);
    }

    clear() {
        localStorage.removeItem(this.storageKey);
    }
}
