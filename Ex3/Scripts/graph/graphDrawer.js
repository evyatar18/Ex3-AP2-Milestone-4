class Point {
    constructor(x, y) {
        this._x = x;
        this._y = y;
    }

    get x() { return this._x; }

    get y() { return this._y; }
}

function graphModule(canvas) {
    var ctx = canvas.getContext("2d");
    
    var points = [];

    // color and line width definitions for line
    var baseStroke = "red";
    var baseStrokeWidth = 5;

    // color and radius definitions for the location circle
    var circleStroke = "gray";
    var circleR1 = baseStrokeWidth * 1.5;
    var circleR2 = baseStrokeWidth;

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

        /* draw the gray current location circle */
        ctx.strokeStyle = circleStroke;
        drawFilledCircle(lastPoint, circleR1);

        /* draw the line between all the points */
        ctx.strokeStyle = baseStroke;
        ctx.lineWidth = baseStrokeWidth;
        drawLine();

        /* draw the red current location circle */
        drawFilledCircle(lastPoint, circleR2);
    }

    return {
        addPoint: addPoint,
        clear: clear,
        draw: draw
    };
}