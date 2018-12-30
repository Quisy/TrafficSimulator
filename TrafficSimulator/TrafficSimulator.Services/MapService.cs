using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TrafficSimulator.Enums;
using TrafficSimulator.Models.Maps;
using TrafficSimulator.Services.Interfaces; 

namespace TrafficSimulator.Services
{
    public class MapService : IMapService
    {
        private readonly int metersPerPoint = 100;

        public RoadsMap ReadMapFromFile(string mapFilePath, string descriptionFilePath)
        {
            var mapArray = this.ReadMapFromTextFile(mapFilePath);
            var rawDescription = File.ReadAllText(descriptionFilePath);
            var mapDescription = JsonConvert.DeserializeObject<MapDescription>(rawDescription);

            List<MapElement> mapElements = new List<MapElement>();

            for (int i = 0; i < mapArray.Length; i++)
            {
                for (int j = 0; j < mapArray[i].Length; j++)
                {
                    var mapElement = new MapElement
                    {
                        MapElementType = MapElementType.None,
                        Position = new Point(j, i),
                        Mark = mapArray[i][j],
                        MeterPosition = new MeterPosition
                        {
                            X = j * metersPerPoint,
                            Y = i * metersPerPoint,
                        }

                    };

                    mapElements.Add(mapElement);
                }
            }

            foreach (var trafficLineDescription in mapDescription.TrafficLineDescriptions)
            {
                var trafficLineElements = mapElements
                    .Where(e => e.Mark == trafficLineDescription.MapMark
                                || (trafficLineDescription.SpawnPointMark.HasValue && e.Mark.Equals(trafficLineDescription.SpawnPointMark.Value))
                          )
                    .ToList();


                foreach (var mapElement in trafficLineElements)
                {
                    mapElement.MapElementType = MapElementType.TrafficLane;
                }

            }

            foreach (var crossLineDescription in mapDescription.CrossroadsDescriptions)
            {
                var crossroadsElements = mapElements
                    .Where(e => e.Mark == crossLineDescription.MapMark)
                    .ToList();


                foreach (var mapElement in crossroadsElements)
                {
                    mapElement.MapElementType = MapElementType.CrossRoads;
                }

            }

            foreach (var signDescription in mapDescription.SignDescriptions)
            {
                var signElements = mapElements
                    .Where(e => e.Mark == signDescription.MapMark)
                    .ToList();


                foreach (var mapElement in signElements)
                {
                    mapElement.MapElementType = MapElementType.Sign;
                    mapElement.Properties = signDescription.Properties;
                }

            }

            RoadsMap map = new RoadsMap
            {
                Name = mapDescription.Name,
                MapElements = mapElements.ToArray()
            };

            return map;
        }

        private char[][] ReadMapFromTextFile(string mapFilePath)
        {
            int linesQuant = File.ReadAllLines(mapFilePath).Length;
            char[][] mapArray = new char[linesQuant][];
            FileStream fileStream = new FileStream(mapFilePath, FileMode.Open);
            using (StreamReader reader = new StreamReader(fileStream))
            {
                for (int i = 0; i < linesQuant; i++)
                {
                    string line = reader.ReadLine();

                    if (line == null)
                        continue;

                    mapArray[i] = line.ToArray();
                }
            }

            return mapArray;
        }
    }
}
