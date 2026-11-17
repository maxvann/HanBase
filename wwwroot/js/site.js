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
