using Formula1.Core.Contracts;
using Formula1.Models;
using Formula1.Models.Contracts;
using Formula1.Repositories;
using Formula1.Repositories.Contracts;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Formula1.Utilities;

namespace Formula1.Core
{
    internal class Controller : IController
    {
        private readonly PilotRepository pilotRepository;
        private readonly RaceRepository raceRepository;
        private readonly FormulaOneCarRepository carRepository;

        public Controller()
        {
            pilotRepository = new PilotRepository();
            raceRepository = new RaceRepository();
            carRepository = new FormulaOneCarRepository();
        }

        public string AddCarToPilot(string pilotName, string carModel)
        {
            var pilot = pilotRepository.FindByName(pilotName);
            var car = carRepository.FindByName(carModel);

            if (pilot == null || pilot.Car != null )
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));
            }
            else if (car == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.CarDoesNotExistErrorMessage, carModel));
            }
            else
            {

                var type = car.GetType().Name == "Ferrari" ? "Ferrari" : "Williams";
                pilot.AddCar(car);
                carRepository.Remove(car);
                return String.Format(OutputMessages.SuccessfullyPilotToCar, pilotName, type, carModel);
            }
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            var pilot = pilotRepository.FindByName(pilotFullName);
            var race = raceRepository.FindByName(raceName);

            if (race == null)
            {
                throw new NullReferenceException(String.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }
            else if (pilot == null || !pilot.CanRace || race.Pilots.Any(x => x.FullName == pilot.FullName))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            }
            else
            {
                race.AddPilot(pilot);
                return String.Format(OutputMessages.SuccessfullyAddPilotToRace, pilotFullName, raceName);
            }
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            if (type == "Ferrari")
            {
                if (carRepository.Models.Any(x => x.Model == model))
                {
                    throw new InvalidOperationException(string.Format(ExceptionMessages.CarExistErrorMessage, model));
                }
                else
                {
                    carRepository.Add(new Ferrari(model, horsepower, engineDisplacement));
                    return String.Format(OutputMessages.SuccessfullyCreateCar, type, model);
                }
            }
            else if (type == "Williams")
            {
                if (carRepository.Models.Any(x => x.Model == model))
                {
                    throw new InvalidOperationException(String.Format(ExceptionMessages.CarExistErrorMessage, model));
                }
                else
                {
                    carRepository.Add(new Williams(model, horsepower, engineDisplacement));
                    return String.Format(OutputMessages.SuccessfullyCreateCar, type, model);
                }
            }
            else
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.InvalidTypeCar, type));
            }
        }

        public string CreatePilot(string fullName)
        {
            if (pilotRepository.Models.Any(x => x.FullName == fullName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotExistErrorMessage, fullName));
            }
            else
            {
                pilotRepository.Add(new Pilot(fullName));
                return String.Format(OutputMessages.SuccessfullyCreatePilot, fullName);
            }
        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            if (raceRepository.Models.Any(x => x.RaceName == raceName))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.RaceExistErrorMessage, raceName));
            }
            else
            {
                raceRepository.Add(new Race(raceName, numberOfLaps));
                return String.Format(OutputMessages.SuccessfullyCreateRace, raceName);
            }
        }

        public string PilotReport()
        {
            StringBuilder sb = new StringBuilder();
            
            foreach (var pilot in pilotRepository.Models.OrderByDescending(x => x.NumberOfWins))
            {
                sb.AppendLine(pilot.ToString());
            }

            return sb.ToString().Trim();
        }

        public string RaceReport()
        {
            StringBuilder sb = new StringBuilder();
            
            foreach(var race in raceRepository.Models.Where(x => x.TookPlace == true))
            {
                sb.AppendLine(race.RaceInfo());
            }

            return sb.ToString().Trim();
        }

        public string StartRace(string raceName)
        {
            var race = raceRepository.FindByName(raceName);

            if (race == null)
            {
                throw new NullReferenceException(String.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }
            else if (race.Pilots.Count < 3)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.InvalidRaceParticipants, raceName));
            }
            else if (race.TookPlace)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.RaceTookPlaceErrorMessage, raceName));
            }
            else
            {
                race.TookPlace = true;

                List<IPilot> orderedRace = race.Pilots.OrderByDescending(x => x.Car.RaceScoreCalculator(race.NumberOfLaps)).Take(3).ToList();

                IPilot winner = orderedRace[0];
                IPilot p2 = orderedRace[1];
                IPilot p3 = orderedRace[2];

                winner.WinRace();

                StringBuilder sb = new StringBuilder();

                sb.AppendLine($"Pilot {winner.FullName} wins the {race.RaceName} race.");
                sb.AppendLine($"Pilot {p2.FullName} is second in the {race.RaceName} race.");
                sb.AppendLine($"Pilot {p3.FullName} is third in the {race.RaceName} race.");

                return sb.ToString().Trim();
            }
        }
    }
}
