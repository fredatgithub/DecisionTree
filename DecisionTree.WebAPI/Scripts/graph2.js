var sourceEndpoint = {
    endpoint: "Dot",
    paintStyle: {
        strokeStyle: "black",
        fillStyle: "#0CA900",
        radius: 7,
        lineWidth: 2
    },
    connectorStyle: {
        lineWidth: 6,
        strokeStyle: "#0CA900",
        joinstyle: "round",
        outlineColor: "#056300",
        outlineWidth: 2
    },
    isSource: true,
    maxConnections: 50,
    connector: ["Bezier", { curviness: 100 }],
    dragOptions: {}
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
    dragOptions: {}
};

function addEndpoints(toId) {
    jsPlumb.addEndpoint(toId, sourceEndpoint, {
        anchor: "Right",
        uuid: toId + "Output"
    });
    
    jsPlumb.addEndpoint(toId, targetEndpoint, {
        anchor: "Left", 
        uuid: toId + "Input"
    });
}

jsPlumb.ready(function () {
    jsPlumb.setContainer("canvas");
    
    addEndpoints("node1");
    addEndpoints("node2");

    jsPlumb.connect({ uuids: ["node1Output", "node2Input"], editable: false });

    jsPlumb.draggable(jsPlumb.getSelector(".window"));

    jsPlumb.addToDragSelection("node1");
    jsPlumb.addToDragSelection("node2");
    
    // if the following is uncommented, the endpoints stop jumping
    $("#canvas").on("click", ".window", function (event) {
        jsPlumb.repaintEverything();
    });
});