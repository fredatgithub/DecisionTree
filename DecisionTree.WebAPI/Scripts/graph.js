var nodeCount = 0;
var startPoint = {};
var rubberbandDrawingActive = false;

var yesConnectorPaintStyle = {
    lineWidth: 6,
    strokeStyle: "#0CA900",
    joinstyle: "round",
    outlineColor: "#056300",
    outlineWidth: 2
};

var yesEndpoint = {
    endpoint: "Dot",
    paintStyle: {
        strokeStyle: "black",
        fillStyle: "#0CA900",
        radius: 7,
        lineWidth: 2
    },
    connectorStyle: yesConnectorPaintStyle,
    isSource: true,
    maxConnections: 50,
    connector: ["Bezier", { curviness: 100 }],
    dragOptions: { cursor: "crosshair" }
};

var noConnectorPaintStyle = {
    lineWidth: 6,
    strokeStyle: "#E90020",
    joinstyle: "round",
    outlineColor: "#630C00",
    outlineWidth: 2
};

var noEndpoint = {
    endpoint: "Dot",
    paintStyle: {
        strokeStyle: "black",
        fillStyle: "#E90020",
        radius: 7,
        lineWidth: 2
    },
    connectorStyle: noConnectorPaintStyle,
    isSource: true,
    maxConnections: 50,
    connector: ["Bezier", { curviness: 100 }],
    dragOptions: { cursor: "crosshair" }
};

var targetEndpoint = {
    endpoint: "Dot",
    paintStyle: {
        strokeStyle: "black",
        fillStyle: "#E9A200",
        radius: 7,
        lineWidth: 2
    },
    isSource: false,
    isTarget: true,
    connector: "Bezier",
    maxConnections: 2,
    dragAllowedWhenFull: false,
    dragOptions: { cursor: "crosshair" }
};

function isNull(obj) {
    return (obj === undefined || obj === null);
}

function isDescendantOf(node, possibleAncestor) {
    if (node === possibleAncestor) return true;
    var conns = jsPlumb.getConnections({ target: node }, true);
    if (isNull(conns) || conns.length === 0) return false;
    if (conns[0].source === possibleAncestor) return true;
    return isDescendantOf(jsPlumb, conns[0].source, possibleAncestor);
}

function removeExistingConnection(targetEndpoint) {
    var connections = jsPlumb.getConnections({ target: targetEndpoint.element }, true);
    connections.forEach(function (connection) {
        jsPlumb.detach(connection);
    });
}

function addQuestionEndpoints(toId) {
    jsPlumb.addEndpoint(toId, targetEndpoint, {
        anchor: "Left", uuid: toId + "Input"
    });

    jsPlumb.addEndpoint(toId, yesEndpoint, {
        anchor: [1, .33, 1, 0],
        uuid: toId + "Output1"
    });

    jsPlumb.addEndpoint(toId, noEndpoint, {
        anchor: [1, .66, 1, 0],
        uuid: toId + "Output2"
    });
}

function makeQuestionNode(x, y) {
    nodeCount++;
    var node = $("<div id='node" + nodeCount + "' class='window'><p>Node " + nodeCount + "</p></div>");
    node.appendTo("#canvas");
    node.css("left", x - node.width() / 2);
    node.css("top", y - node.height() / 2);
    addQuestionEndpoints("node" + nodeCount);
    jsPlumb.draggable(node);
}

function diagramContainer_MouseDown(event) {
    startPoint.x = event.pageX;
    startPoint.y = event.pageY;

    $("#rubberband").css({ top: startPoint.y, left: startPoint.x, height: 1, width: 1, position: 'absolute' });
    $("#rubberband").show();
}

function diagramContainer_MouseMove(event) {
    if ($("#rubberband").is(":visible") !== true) { return; }

    var t = (event.pageY > startPoint.y) ? startPoint.y : event.pageY;
    var l = (event.pageX >= startPoint.x) ? startPoint.x : event.pageX;

    wcalc = event.pageX - startPoint.x;
    var w = (event.pageX > startPoint.x) ? wcalc : (wcalc * -1);

    hcalc = event.pageY - startPoint.y;
    var h = (event.pageY > startPoint.y) ? hcalc : (hcalc * -1);

    $("#rubberband").css({ top: t, left: l, height: h, width: w, position: 'absolute' });
}

function diagramContainer_MouseUp(event) {
    diagramContainer_FindSelectedItem();
    $("#rubberband").hide();
}

function diagramContainer_FindSelectedItem() {
    if ($("#rubberband").is(":visible") !== true) { return; }

    var rubberbandOffset = getTopLeftOffset($("#rubberband"));

    jsPlumb.clearDragSelection();

    $(".window").each(function () {
        $(this).removeClass("selected-node");
        var itemOffset = getTopLeftOffset($(this));
        if (itemOffset.top > rubberbandOffset.top &&
            itemOffset.left > rubberbandOffset.left &&
            itemOffset.right < rubberbandOffset.right &&
            itemOffset.bottom < rubberbandOffset.bottom) {
            $(this).addClass("selected-node");

            var elementid = $(this).attr('id');
            jsPlumb.addToDragSelection(elementid);
        }
    });
}

function getTopLeftOffset(element) {
    var elementDimension = {};
    elementDimension.left = element.offset().left;
    elementDimension.top = element.offset().top;
    elementDimension.right = elementDimension.left + element.outerWidth();
    elementDimension.bottom = elementDimension.top + element.outerHeight();

    return elementDimension;
}

function diagramContainer_Click(event) {
    if (startPoint.x === event.pageX && startPoint.y === event.pageY) {
        jsPlumb.clearDragSelection();
        $(".window").each(function () {
            $(this).removeClass("selected-node");
        });
    }
}

function loadDiagram(graph) {
    
}

jsPlumb.ready(function () {
    
    jsPlumb.setContainer("canvas");
    $("#canvas").mousedown(diagramContainer_MouseDown);
    $("#canvas").mousemove(diagramContainer_MouseMove);
    $("#canvas").mouseup(diagramContainer_MouseUp);
    $("#canvas").click(diagramContainer_Click);

    jsPlumb.bind("connection", function (info) {
        info.connection.setPaintStyle(info.sourceEndpoint.connectorStyle);
    });

    jsPlumb.bind("beforeDrop", function (info) {
        var sourceNode = info.connection.source;
        var targetNode = info.dropEndpoint.element;
        if (isDescendantOf(sourceNode, targetNode)) {
            return false;
        }

        removeExistingConnection(info.dropEndpoint);

        return true;
    });

    $("#clear-button").click(function () {
        jsPlumb.empty("canvas");
        $("#canvas").append("<div id='rubberband' class='selection-rect'></div>");
        nodeCount = 0;
    });

    $("#add-button").click(function () {
        makeQuestionNode(150, 100);
    });

    $("#canvas").dblclick(function (event) {
        var x = event.pageX, y = event.pageY;
        makeQuestionNode(x, y);
    });

    $("#canvas").on("click", ".window", function (event) {
        if (!$(this).hasClass("selected-node")) {
            jsPlumb.clearDragSelection();
            $(".window").each(function () { $(this).removeClass("selected-node"); });
            $(this).addClass("selected-node");
            //jsPlumb.addToDragSelection($(this));
        }
        jsPlumb.repaintEverything();
    });
});