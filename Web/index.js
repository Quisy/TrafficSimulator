let app = new PIXI.Application({ 
    width: 500,         // default: 800
    height: 500,        // default: 600
    antialias: true,    // default: false
    transparent: true, // default: false
    resolution: 1       // default: 1
  }
);
// app.renderer.backgroundColor = 0xFFFFFF ;
document.body.appendChild(app.view);

PIXI.loader
  .add("src/images/map.png")
  .add("src/images/car.png")
  .load(setup);


  function setup() {
    let map = new PIXI.Sprite(PIXI.loader.resources["src/images/map.png"].texture);
    let car = new PIXI.Sprite(PIXI.loader.resources["src/images/car.png"].texture);
    car.x = 176;
    car.y = 460;
    app.stage.addChild(map);
    app.stage.addChild(car);

    app.ticker.add(() => {
      // each frame we spin the bunny around a bit
      car.y -= 1;
      if(car.y <= 0){
        car.y = 460;
      }
 });

  }
//Add the canvas that Pixi automatically created for you to the HTML document
