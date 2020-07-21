using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class FuelMotor : Motor
    {
        // Defines
        private const float k_MinFuelTankCapacity = 0;
        private const string k_WrongTypeOfFuelErrorMessage = "Error: Fuel type does not match";

        // Data members
        private eFuelType m_FuelType;

        public FuelMotor(float i_MaxAmountOfEnergy, float i_CurrentAmountOfEnergy, eFuelType i_FuelType) : base(i_MaxAmountOfEnergy, i_CurrentAmountOfEnergy)
        {
            m_FuelType = i_FuelType;
        }

        public override void AddEnergy(float i_AmountOfEnergyToAdd, eFuelType i_FuelType)
        {

            if (m_FuelType != i_FuelType)
            {
                throw new ArgumentException(k_WrongTypeOfFuelErrorMessage);
            }
            if ((CurrentAmountOfEnergy + i_AmountOfEnergyToAdd > MaxAmountOfEnergy) || i_AmountOfEnergyToAdd < 0)
            {
                throw new ValueOutOfRangeException(k_MinFuelTankCapacity, MaxAmountOfEnergy, k_ValueRangeErrorMessage);
            }

            m_CurrentAmountOfEnergy += i_AmountOfEnergyToAdd;
        }

        // Properties
        public eFuelType FuelType
        {
            get { return m_FuelType; }
            set { m_FuelType = value; }
        }

        public override string ToString()
        {
            StringBuilder fuelMotorDataBuilder = new StringBuilder();
            fuelMotorDataBuilder.Append(base.ToString());
            fuelMotorDataBuilder.AppendLine("Motor type: Fuel");
            fuelMotorDataBuilder.AppendFormat("Fuel type: {0}{1}", m_FuelType, Environment.NewLine);
            fuelMotorDataBuilder.AppendFormat("Current amount of fuel: {0}{1}", m_CurrentAmountOfEnergy, Environment.NewLine);
            fuelMotorDataBuilder.AppendFormat("Maximum amount of fuel: {0}{1}", r_MaxAmountOfEnergy, Environment.NewLine);

            return fuelMotorDataBuilder.ToString();
        }

        
    }
}
