using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComfortHomeClassLibrary
{
    public class Fan
    {
        public enum States { OFF, LOW, MEDIUM, HIGH }; //Initializes the different speed settings of the fan.
        public States state;

        public string co2Reading;
        public string vocReading;
        public string tempReading;
        public string humidReading;

        public void Prioritize()
        {
            int readingSum = 0;

            //Add all the most recent readings to a list
            List<string> readings = new List<string>();
            readings.Add(co2Reading);       //Index 0
            readings.Add(vocReading);       //Index 1
            readings.Add(tempReading);      //Index 2
            readings.Add(humidReading);     //Index 3

            //Assign numerical values to create a weighted average
            foreach (string r in readings)
            {
                if (r == "HIGH") readingSum += 9;
                if (r == "MEDIUM") readingSum += 5;
                if (r == "LOW" || r == "OFF") readingSum += 1;
            }

            int conclusion = readingSum / 4;    //Division to create a workable average
            if (conclusion > 4)                 //Since the highest possible state is HIGH, anything
                conclusion = 4;                 //reaching an average > 4 will automatically set the system to high.

            switch (conclusion)
            {
                case 1:
                    state = States.OFF;
                    break;
                case 2:
                    state = States.LOW;
                    break;
                case 3:
                    state = States.MEDIUM;
                    break;
                case 4:
                    state = States.HIGH;
                    break;
            }
        }

        public void SetState(string stateToSet) // Function foe setting the state of the fan
        {
            switch (stateToSet)
            {
                case "LOW":
                    state = States.LOW;
                    break;
                case "MEDIUM":
                    state = States.MEDIUM;
                    break;
                case "HIGH":
                    state = States.HIGH;
                    break;
                default:
                    state = States.OFF;
                    break;
            }
        }
    }
}
