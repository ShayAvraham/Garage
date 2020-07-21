using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        // Defines
        private const float k_MinAirPressure = 0;
        private const string k_ValueRangeErrorMessage = "Error: The quantity does not match the desired range of values";

        // Data members
        private readonly string r_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressure;

        public Wheel(string i_ManufacturerName, float i_MaxAirPressure, float i_CurrentAirPressure)
        {
            r_ManufacturerName = i_ManufacturerName;
            r_MaxAirPressure = i_MaxAirPressure;
            m_CurrentAirPressure = i_CurrentAirPressure;
        }

        public void InflateTire(float i_AmountOfAirPressureToAdd)
        {
            if (m_CurrentAirPressure + i_AmountOfAirPressureToAdd > r_MaxAirPressure || i_AmountOfAirPressureToAdd < 0)
            {
                throw new ValueOutOfRangeException(k_MinAirPressure, r_MaxAirPressure, k_ValueRangeErrorMessage);
            }

            m_CurrentAirPressure += i_AmountOfAirPressureToAdd;
        }

        public void InflateWheelToMaxAirPressure()
        {
            m_CurrentAirPressure = r_MaxAirPressure;
        }

        // Properties
        public string ManufacturerName
        {
            get { return r_ManufacturerName; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }
        }

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }

        public override string ToString()
        {
            StringBuilder wheelDataBuilder = new StringBuilder();

            wheelDataBuilder.AppendLine("---Wheels Details---");
            wheelDataBuilder.AppendFormat("Manufacturer Name: {0}{1}", r_ManufacturerName, Environment.NewLine);
            wheelDataBuilder.AppendFormat("Current air pressure: {0}{1}", m_CurrentAirPressure, Environment.NewLine);
            wheelDataBuilder.AppendFormat("Maximum air pressure: {0}{1}", r_MaxAirPressure, Environment.NewLine);

            return wheelDataBuilder.ToString();
        }
    }
}
