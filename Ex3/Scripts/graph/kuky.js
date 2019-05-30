// parse a line containing comma separated floats
function readFloats(line) {
    var split = line.split(",");
    var floats = [];

    for (var i in split) {
        floats.push(parseFloat(split[i]));
    }

    return floats;
}

// read floats from a given url and run the function recv on an array of these floats
function getFloats(url, recv) {
    $.post(url, function (data) {
        var floats = readFloats(data);
        recv(floats);
    });
}

// recieves an array of floats and puts them inside the graph as a 2d point
function addFloats(floats, graph) {
    graph.addPoint(new Point(floats[1], floats[0]));
}

function freq_to_delay(freq) {
    return (1 / freq) * 1000;
}

// displays point data from url on the given graph
function display_once(url, graph) {
    getFloats(url, function (floats) {
        addFloats(floats, graph);
        graph.clear();
        graph.draw();
    });
}

// displays point data from url on the given graph with repetition delays
// between points addition
// returns a function which stops the interval
function display_multiple(url, graph, freq) {

    // stops the interval
    var clear;

    var timerid = setInterval(function () {
        getFloats(url, function (floats) {
            // if done with stream
            if (floats[0] == NaN) {
                clear();
                return;
            }

            // if recieved a point
            addFloats(floats, graph);
            graph.clear();
            graph.draw();
        });
    }, freq_to_delay(freq));

    clear = () => clearInterval(timerid);

    return clear;
}