using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        public enum eCarColor
        {
            Yellow,
            White,
            Red,
            Black
        }

        public enum eNumOfCarDoors
        {
            Two = 2,
            Three = 3,
            Four = 4,
            Five = 5
        }

        // Data members
        private eCarColor m_CarColor;
        private readonly eNumOfCarDoors r_NumOfCarDoors;

        public Car(string i_ModelName, string i_LicenseNumber, List<Wheel> i_Wheel, Motor i_Motor, eCarColor i_CarColor, eNumOfCarDoors i_NumOfCarDoors) : base(i_ModelName, i_LicenseNumber, i_Wheel, i_Motor)
        {
            m_CarColor = i_CarColor;
            r_NumOfCarDoors = i_NumOfCarDoors;
        }

        public override string ToString()
        {
            StringBuilder carDataBuilder = new StringBuilder();

            carDataBuilder.AppendLine("---Car Details---");
            carDataBuilder.AppendFormat("{0}{1}", base.ToString(), Environment.NewLine);
            carDataBuilder.AppendLine("---Unique Car Details---");
            carDataBuilder.AppendFormat("Car Color: {0}{1}", m_CarColor, Environment.NewLine);
            carDataBuilder.AppendFormat("Number Of Car Doors: {0}{1}", r_NumOfCarDoors, Environment.NewLine);

            return carDataBuilder.ToString();
        }
    }
}
