let markers = [];
let newMarkers = [];
let imgData = "";

function initializeCanvas(canvas) {
    const ctx = canvas.getContext("2d");
    const img = new Image();
    img.src = imgData === null ? "images/Default.png" : imgData;

    img.onload = function () {
        img.alt = "Image"
        canvas.width = img.width;
        canvas.height = img.height;
        ctx.drawImage(img, 0, 0, img.width, img.height);
        drawMarkers(ctx, canvas);
    };

    if (imgData !== null) {
        canvas.addEventListener("mousedown", function (e) {
            const x = e.clientX - canvas.getBoundingClientRect().left;
            const y = e.clientY - canvas.getBoundingClientRect().top;
            const creationDate = new Date();
            newMarkers.push({ x, y, creationDate });

            ctx.beginPath();
            ctx.arc(x, y, 10, 0, 2 * Math.PI);
            ctx.lineWidth = 0;
            ctx.strokeStyle = "red";
            ctx.stroke();
            ctx.closePath();
        });
    }
}

window.getMarkersFromClient = function () {
    return newMarkers;
};

function drawMarkers(ctx) {
    if (markers.length > 0) {
        markers.forEach((point) => {
            ctx.beginPath();
            ctx.arc(point.x, point.y, 10, 0, 2 * Math.PI);
            ctx.lineWidth = 0;
            ctx.strokeStyle = "red";
            ctx.stroke();
            ctx.closePath();
        });
    }
}

window.getImgFromBackend = function (path) {
    imgData = path;
}

window.getMarkersFromBackend = function (receivedPoints) {
    markers = receivedPoints;
};

//prasowanie
let pressMarkers = [];
let newPressMarkers = [];
let markerCount = 0;
const markerLabels = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

function initPressCanvas(canvas) {
    const ctx = canvas.getContext("2d");
    const img = new Image();
    img.src = imgData === null ? "images/Default.png" : imgData;

    img.onload = function () {
        img.alt = "Image";
        canvas.width = img.width;
        canvas.height = img.height;
        ctx.drawImage(img, 0, 0, img.width, img.height);
        drawPressingMarkers(ctx, canvas);
    };

    if (imgData !== null) {
        canvas.addEventListener("mousedown", function (e) {
            const x = e.clientX - canvas.getBoundingClientRect().left;
            const y = e.clientY - canvas.getBoundingClientRect().top;
            const label = generateMarkerLabel();
            const creationDate = new Date();

            newPressMarkers.push({ x, y, label, creationDate });

            drawMarker(ctx, x, y, label, creationDate);
        });
    }
}

function generateMarkerLabel() {
    const base = markerLabels.length;
    let label;
    let suffix;
    let prefix;

    const existingLabels = new Set([
        ...pressMarkers.map(marker => marker.label),
        ...newPressMarkers.map(marker => marker.label)
    ]);

    do {
        prefix = markerLabels[markerCount % base];
        suffix = Math.floor(markerCount / base) + 1;
        label = `${prefix}${suffix}`;
        markerCount++;
    } while (existingLabels.has(label));

    return label;
}

function drawMarker(ctx, x, y, label) {
    ctx.beginPath();
    ctx.arc(x, y, 10, 0, 2 * Math.PI);
    ctx.lineWidth = 1;
    ctx.strokeStyle = "red";
    ctx.stroke();
    ctx.closePath();

    ctx.font = "14px Arial";
    ctx.fillStyle = "blue";
    ctx.textAlign = "center";
    ctx.fillText(label, x, y - 15);
}

window.getPressingMarkersFromClient = function () {
    const markersToReturn = newPressMarkers;
    newPressMarkers = []; 
    return markersToReturn; 
};

function drawPressingMarkers(ctx) {
    if (pressMarkers.length > 0) {
        pressMarkers.forEach((point) => {
            drawMarker(ctx, point.x, point.y, point.label);
        });
    }
}

window.getPressingMarkersFromBackend = function (receivedPoints) {
    pressMarkers = receivedPoints;
};

