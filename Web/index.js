let app = new PIXI.Application({
  width: 500,         // default: 800
  height: 500,        // default: 600
  antialias: true,    // default: false
  transparent: true, // default: false
  resolution: 1       // default: 1
}
);

let simulationCurrentTick = 0;
let simulationData = [];
let alreadyRotated = false;
// app.renderer.backgroundColor = 0xFFFFFF ;
document.body.appendChild(app.view);

readSimulationFile();

PIXI.loader
  .add("src/images/map.png")
  .add("src/images/car.png")
  .load(setup);


function setup() {
  let map = new PIXI.Sprite(PIXI.loader.resources["src/images/map.png"].texture);
  let car = new PIXI.Sprite(PIXI.loader.resources["src/images/car.png"].texture);
  car.x = simulationData[0].Cars[0].Position.X - getCarOffsetX(simulationData[0].Cars[0].Direction, 0);
  car.y = simulationData[0].Cars[0].Position.Y - getCarOffsetY(simulationData[0].Cars[0].Direction, 0);
  car.anchor.x = 0.5;
  car.anchor.y = 0.5;
  app.stage.addChild(map);
  app.stage.addChild(car);

  app.ticker.add(() => {

    if (simulationCurrentTick >= simulationData.length)
      return;

    let simulationEntry = simulationData[simulationCurrentTick];

    if (simulationCurrentTick > 0 && !alreadyRotated) {
      let rotation = getCarRotation(simulationData[simulationCurrentTick - 1].Cars[0].Direction, simulationEntry.Cars[0].Direction);
      car.rotation += rotation;
      alreadyRotated = true;
    }

    let simulationPositionX = simulationEntry.Cars[0].Position.X - getCarOffsetX(simulationEntry.Cars[0].Direction, 0);
    let simulationPositionY = simulationEntry.Cars[0].Position.Y - getCarOffsetY(simulationEntry.Cars[0].Direction, 0);

    moveVector = {
      x: simulationPositionX != car.x ? 1 : 0,
      y: simulationPositionY != car.y ? -1 : 0,
    }

    car.y = car.y + moveVector.y;
    car.x = car.x + moveVector.x;

    if (car.x == simulationPositionX && car.y == simulationPositionY){
      alreadyRotated = false;
      simulationCurrentTick++;
    }
      
  });

}

function getCarRotation(prevDirection, nextDirection) {
  if (prevDirection == 0 && nextDirection == 3) {
    return Math.PI / 2;
  }
  else {
    return 0;
  }
}

function getCarOffsetX(direction, carWidth) {
  var offset = 0;
  if (direction == 0) {
    offset = carWidth / 2;
  }
  else if (direction == 1) {
    offset = -carWidth / 2;
  }
  return offset;
}

function getCarOffsetY(direction, carWidth) {
  var offset = 0;
  if (direction == 2) {
    offset = -carWidth / 2;
  }
  else if (direction == 3) {
    offset = carWidth / 2;
  }
  return offset;
}

function readSimulationFile() {
  $.getJSON("files/simulation.json", function (data) {
    simulationData = data;
    console.log(simulationData);
  });
}
