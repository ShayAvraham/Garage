using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected readonly string r_ModelName;
        protected string m_LicenseNumber;
        protected float m_PercentageOfEnergyRemaining;
        protected Motor m_Motor;
        protected List<Wheel> m_Wheel;

        public Vehicle(string i_ModelName, string i_LicenseNumber, List<Wheel> i_Wheel, Motor i_Motor)
        {
            r_ModelName = i_ModelName;
            m_LicenseNumber = i_LicenseNumber;
            m_Wheel = i_Wheel;
            m_Motor = i_Motor;
            setPercentageOfEnergyRemaining();
        }

        public void AddEnergyToMotor(float i_AmountOfEnergyToAdd, FuelMotor.eFuelType i_FuelType = 0)
        {
            m_Motor.AddEnergy(i_AmountOfEnergyToAdd, i_FuelType);
            setPercentageOfEnergyRemaining();
        }

        private void setPercentageOfEnergyRemaining()
        {
            m_PercentageOfEnergyRemaining = m_Motor.CurrentAmountOfEnergy / m_Motor.MaxAmountOfEnergy;
        }

        public void InflateAllWheelsToMaxAirPressure()
        {
            foreach (Wheel wheel in m_Wheel)
            {
                wheel.InflateWheelToMaxAirPressure();
            }
        }

        public override string ToString()
        {
            StringBuilder vehicleDataBuilder = new StringBuilder();

            vehicleDataBuilder.AppendFormat("Vehicle License number: {0}{1}", m_LicenseNumber, Environment.NewLine);
            vehicleDataBuilder.AppendFormat("Vehicle model: {0}{1}{1}", r_ModelName, Environment.NewLine);
            vehicleDataBuilder.Append(m_Motor.ToString());
            vehicleDataBuilder.AppendFormat("Energy left (percentage): {0}{1}{1}", m_PercentageOfEnergyRemaining, Environment.NewLine);
            vehicleDataBuilder.Append(m_Wheel.ElementAt(0).ToString());
            vehicleDataBuilder.AppendFormat("Number of wheels: {0}{1}", m_Wheel.Count, Environment.NewLine);

            return vehicleDataBuilder.ToString();
        }

        // Properties
        public string LicenseNumber
        {
            get { return m_LicenseNumber; }
        }

        public Motor Motor
        {
            get { return m_Motor; }
        }
    }
}
