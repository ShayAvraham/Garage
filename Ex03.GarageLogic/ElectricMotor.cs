using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class ElectricMotor : Motor
    {
        // Defines
        private const float k_MinBatteryCapacity = 0;
        public ElectricMotor(float i_MaxAmountOfEnergy, float i_CurrentAmountOfEnergy) : base(i_MaxAmountOfEnergy, i_CurrentAmountOfEnergy)
        {
        }

        public override void AddEnergy(float i_AmountOfEnergyToAdd, eFuelType i_FuelType = 0)
        {
            if ((CurrentAmountOfEnergy + i_AmountOfEnergyToAdd > MaxAmountOfEnergy) || i_AmountOfEnergyToAdd < 0)
            {
                throw new ValueOutOfRangeException(k_MinBatteryCapacity, MaxAmountOfEnergy, k_ValueRangeErrorMessage);
            }

            m_CurrentAmountOfEnergy += i_AmountOfEnergyToAdd;
        }

        public override string ToString()
        {
            StringBuilder electricMotorDataBuilder = new StringBuilder();
            electricMotorDataBuilder.Append(base.ToString());
            electricMotorDataBuilder.AppendLine("Motor type : Electric");
            electricMotorDataBuilder.AppendFormat("Current amount battery time: {0}{1}", m_CurrentAmountOfEnergy, Environment.NewLine);
            electricMotorDataBuilder.AppendFormat("Maximum battery time: {0}{1}", r_MaxAmountOfEnergy, Environment.NewLine);

            return electricMotorDataBuilder.ToString();
        }
    }
}
