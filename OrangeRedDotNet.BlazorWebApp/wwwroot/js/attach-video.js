window.attachVideo = (videoElementId) => {
    // It would be better to dispose of the player when hiding it, but this works for now
    if (videojs.players.hasOwnProperty(videoElementId)) {
        videojs.players[videoElementId].dispose();
    }
    var player = videojs(videoElementId, {
        fluid: true
    });
    player.hlsQualitySelector({
        displayCurrentQuality: true,
    });
};