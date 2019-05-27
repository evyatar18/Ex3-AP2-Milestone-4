class Point {
    constructor(x, y) {
        this._x = x;
        this._y = y;
    }

    get x() { return this._x; }

    get y() { return this._y; }
}

/**
 * Parses a point from a given line
 * @param {string} line
 */
function parsePoint(line) {
    var arr = line.split(",");

    if (arr.length != 2)
        return null;

    var x = parseFloat(arr[0]);
    var y = parseFloat(arr[1]);

    return new Point(x, y);
}

/**
 * Basic graph module
 * @param {any} canvas a canvas to draw on
 * @returns an object including the methods addPoint, clear and draw
 */
function graphModule(canvas) {
    var ctx = canvas.getContext("2d");
    var points = [];

    // color and line width definitions for line
    var baseStroke = "rgb(232, 65, 24)";
    var baseStrokeWidth = 5;

    // color and radius definitions for the location circle
    var circleStroke = "rgb(25, 42, 86)";
    var circleR1 = baseStrokeWidth * 1.75;
    var circleR2 = baseStrokeWidth * 1;

    /**
     * add a point to the collection of points on graph
     * @param {Point} p
     */
    function addPoint(p) {
        return points.push(p);
    }

    /**
     * clear the graph view from any kind of things drawn
     */
    function clear() {
        ctx.clearRect(0, 0, canvas.width, canvas.height);
    }


    /**
     * Draws a filled circle
     * @param {Point} loc the location to draw at
     * @param {any} radius the radius
     */
    function drawFilledCircle(loc, radius) {
        ctx.beginPath();
        ctx.arc(loc.x, loc.y, radius, 0, Math.PI * 2);
        ctx.fill();
    }

    /**
     * Draws the line between all the points
     */
    function drawLine() {
        ctx.beginPath();

        // add all points on path and connect them by straight lines
        for (var i = 0; i < points.length; i++)
            ctx.lineTo(points[i].x, points[i].y);

        // actually draw
        ctx.stroke();
    }

    /**
     * draw the whole graph
     */
    function draw() {
        var lastPoint = points[points.length - 1];

        if (!lastPoint) return; // no last point

        /* draw the gray current location circle */
        ctx.fillStyle = circleStroke;
        drawFilledCircle(lastPoint, circleR1);

        /* draw the line between all the points */
        ctx.strokeStyle = baseStroke;
        ctx.lineWidth = baseStrokeWidth;
        drawLine();

        /* draw the red current location circle */
        ctx.fillStyle = baseStroke;
        drawFilledCircle(lastPoint, circleR2);
    }

    return {
        addPoint: addPoint,
        clear: clear,
        draw: draw
    };
}


/**
 * Creates a graph which has defined borders within the given canvas,
 *  filling all the canvas
 * @param {any} canvas the canvas to draw on
 * @param {any} min_x the minimal x of every point on graph
 * @param {any} max_x the maximal x of every point on graph
 * @param {any} min_y the minimal y of every point on graph
 * @param {any} max_y the maximal y of every point on graph
 * @return a graph object
 */
function scaledGraphModule(canvas, min_x, max_x, min_y, max_y) {
    var graph = graphModule(canvas);

    function scaler(min_from, width_from,
        width_to) {
        var bias = - min_from;
        var scale = width_to / width_from;
        return function (value) { return (value + bias) * scale; }
    }

    var xscale = scaler(min_x, max_x - min_x, canvas.width);
    var yscale = scaler(min_y, max_y - min_y, canvas.height);

    /**
     * Add a point to the graph (which is between the graph edges)
     * @param {Point} p
     */
    function addPoint(p) {
        graph.addPoint(new Point(xscale(p.x), yscale(p.y)));
    }

    return {
        addPoint: addPoint,
        clear: graph.clear,
        draw: graph.draw
    };
}

/**
 * Creates a graph which has defined borders within the given canvas,
 *  filling all the canvas
 * Also includes an option to redraw on a bigger graph
 * @param {any} canvas the canvas to draw on
 * @param {any} min_x the minimal x of every point on graph
 * @param {any} max_x the maximal x of every point on graph
 * @param {any} min_y the minimal y of every point on graph
 * @param {any} max_y the maximal y of every point on graph
 * @returns a graph object with method recreate which recreates the graph
 */
function scalableScaledGraphModule(canvas, min_x, max_x, min_y, max_y) {
    var graph;
    var saved_points = [];

    function createGraph() {
        graph = scaledGraphModule(canvas, min_x, max_x, min_y, max_y);

        for (var p in saved_points) {
            graph.addPoint(saved_points[p]);
        }
    }

    function addPoint(p) {
        saved_points.push(p);
        graph.addPoint(p);
    }

    function clear() {
        graph.clear();
    }

    function draw() {
        graph.draw();
    }

    // create the first graph
    createGraph();

    return {
        recreate: createGraph,
        addPoint: addPoint,
        clear: clear,
        draw: draw
    };
}