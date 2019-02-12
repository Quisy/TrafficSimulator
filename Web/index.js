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
let cars = [];

// app.renderer.backgroundColor = 0xFFFFFF ;
document.body.appendChild(app.view);

readSimulationFile();

PIXI.loader
  .add("src/images/map.png")
  .add("src/images/car.png")
  .load(setup);

function setup() {
  
  let map = new PIXI.Sprite(PIXI.loader.resources["src/images/map.png"].texture);
  let carx = new PIXI.Sprite(PIXI.loader.resources["src/images/car.png"].texture);
  app.stage.addChild(map);
  app.stage.addChild(carx);
  createCarModels();

  cars.forEach(car => {
    app.stage.addChild(car.sprite);
  });


  app.ticker.add(() => {

    if (simulationCurrentTick >= simulationData.length)
      return;

    let simulationEntry = simulationData[simulationCurrentTick];

    for (let index = 0; index < simulationEntry.Cars.length; index++) {

      let tempCar = simulationEntry.Cars[index];
      let simulationCar = getCarById(tempCar.Id);

      if (simulationCurrentTick > 0 && !simulationCar.rotated) {
        let rotation = getCarRotation(tempCar.Direction, simulationCar.direction);
        simulationCar.sprite.rotation += rotation;
        simulationCar.rotated = true;
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
        x: simulationPositionX != simulationCar.sprite.x ? 1 : 0,
        y: simulationPositionY != simulationCar.sprite.y ? -1 : 0,
      }

      simulationCar.sprite.y = simulationCar.sprite.y + moveVector.y;
      simulationCar.sprite.x = simulationCar.sprite.x + moveVector.x;

      if (simulationCar.sprite.x == simulationPositionX && simulationCar.sprite.y == simulationPositionY) {
        simulationCurrentTick++;
        simulationCar.rotated = false;
      }
    }


  });

}

function getCarById(id) {
  var result = cars.find(car => {
    return car.id === id
  });

  return result;
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
  });
}

function createCarModels(){
  simulationData[0].Cars.forEach(car => {
    let carModel = new Car();
    carModel.id = car.Id;
    carModel.sprite = new PIXI.Sprite(PIXI.loader.resources["src/images/car.png"].texture);
    carModel.sprite.x = car.Position.X - getCarOffsetX(car.Direction, 0);
    carModel.sprite.y = car.Position.Y - getCarOffsetY(car.Direction, 0);
    carModel.sprite.anchor.x = 0.5;
    carModel.sprite.anchor.y = 0.5;

    cars.push(carModel);
  });
}

function addLog(logText) {
  document.getElementById("log").value += logText;
  document.getElementById("log").value += "\n\n";
}
