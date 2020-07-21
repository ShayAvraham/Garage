using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public abstract class Motor
    {
        protected const float k_MinCurrentAmountOfEnergy = 0;
        protected const string k_ValueRangeErrorMessage = "Error: The quantity does not match the desired range of values";

        public enum eFuelType
        {
            Octan95 = 1,
            Octan96 = 2,
            Octan98 = 3,
            Soler = 4
        }

        // Data members
        protected readonly float r_MaxAmountOfEnergy;
        protected float m_CurrentAmountOfEnergy;

        public Motor(float i_MaxAmountOfEnergy, float i_CurrentAmountOfEnergy)
        {
            r_MaxAmountOfEnergy = i_MaxAmountOfEnergy;
            m_CurrentAmountOfEnergy = i_CurrentAmountOfEnergy;
        }

        public abstract void AddEnergy(float i_AmountOfEnergyToAdd, FuelMotor.eFuelType i_FuelType = 0);

        // Properties
        public float CurrentAmountOfEnergy
        {
            get { return m_CurrentAmountOfEnergy; }
        }

        public float MaxAmountOfEnergy
        {
            get { return r_MaxAmountOfEnergy; }
        }

        public override string ToString()
        {
            StringBuilder electricMotorDataBuilder = new StringBuilder();

            electricMotorDataBuilder.AppendLine("---Motor Details---");

            return electricMotorDataBuilder.ToString();
        }
    }
}
