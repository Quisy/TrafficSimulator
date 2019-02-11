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
let alreadyRotated2 = false;
let car = null;
let car2 = null;
// app.renderer.backgroundColor = 0xFFFFFF ;
document.body.appendChild(app.view);

readSimulationFile();

PIXI.loader
  .add("src/images/map.png")
  .add("src/images/car.png")
  .load(setup);


function setup() {
  let map = new PIXI.Sprite(PIXI.loader.resources["src/images/map.png"].texture);

  car = new PIXI.Sprite(PIXI.loader.resources["src/images/car.png"].texture);
  car.x = simulationData[0].Cars[0].Position.X - getCarOffsetX(simulationData[0].Cars[0].Direction, 0);
  car.y = simulationData[0].Cars[0].Position.Y - getCarOffsetY(simulationData[0].Cars[0].Direction, 0);
  car.anchor.x = 0.5;
  car.anchor.y = 0.5;

  car2 = new PIXI.Sprite(PIXI.loader.resources["src/images/car.png"].texture);
  car2.x = simulationData[0].Cars[1].Position.X - getCarOffsetX(simulationData[0].Cars[1].Direction, 0);
  car2.y = simulationData[0].Cars[1].Position.Y - getCarOffsetY(simulationData[0].Cars[1].Direction, 0);
  car2.anchor.x = 0.5;
  car2.anchor.y = 0.5;
  car2.rotation += Math.PI / 2;

  app.stage.addChild(map);
  app.stage.addChild(car);
  app.stage.addChild(car2);

  app.ticker.add(() => {

    if (simulationCurrentTick >= simulationData.length)
      return;

    let simulationEntry = simulationData[simulationCurrentTick];

    for (let index = 0; index < simulationEntry.Cars.length; index++) {

      let tempCar = simulationEntry.Cars[index];
      let simulationCar = getCarByIndex(index);
      let rotated = carAlreadyRotated(index);

      if (simulationCurrentTick > 0 && !rotated) {
        let rotation = getCarRotation(tempCar.Direction, simulationCar.Direction);
        simulationCar.rotation += rotation;
        rotated = true;
      }

      for (let index = 0; index < tempCar.VisibleElements.length; index++) {
        const element = tempCar.VisibleElements[index];
        addLog("car " + index + "(visible object): " + element.Id);
      }

      for (let index = 0; index < tempCar.Collisions.length; index++) {
        const element = tempCar.Collisions[index];
        addLog("car " + index + "(collision): " + element);
      }

      let simulationPositionX = tempCar.Position.X - getCarOffsetX(tempCar.Direction, 0);
      let simulationPositionY = tempCar.Position.Y - getCarOffsetY(tempCar.Direction, 0);

      moveVector = {
        x: simulationPositionX != simulationCar.x ? 1 : 0,
        y: simulationPositionY != simulationCar.y ? -1 : 0,
      }

      simulationCar.y = simulationCar.y + moveVector.y;
      simulationCar.x = simulationCar.x + moveVector.x;

      if (simulationCar.x == simulationPositionX && simulationCar.y == simulationPositionY) {
        simulationCurrentTick++;
        rotated = false;
      }
    }

    
  });

}

function carAlreadyRotated(index){
  if (index == 0)
  return alreadyRotated;
if (index == 1)
  return alreadyRotated2;

return false;
}

function getCarByIndex(index) {
  if (index == 0)
    return car;
  if (index == 1)
    return car2;

  return null;
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
  $.getJSON("files/simulation2.json", function (data) {
    simulationData = data;
    console.log(simulationData);
  });
}

function addLog(logText) {
  document.getElementById("log").value += logText;
  document.getElementById("log").value += "\n\n";
}
