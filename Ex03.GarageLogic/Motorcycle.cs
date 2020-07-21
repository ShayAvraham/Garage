using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        public enum eLicenseType
        {
            A,
            A1,
            AB,
            B1
        }

        // Data members
        private eLicenseType m_LicenseType;
        private int m_MotorCapacity;

        public Motorcycle(string i_ModelName, string i_LicenseNumber, List<Wheel> i_Wheel, Motor i_Motor, eLicenseType i_LicenseType, int i_MotorCapacity) : base(i_ModelName, i_LicenseNumber, i_Wheel, i_Motor)
        {
            m_LicenseType = i_LicenseType;
            m_MotorCapacity = i_MotorCapacity;
        }

        public override string ToString()
        {
            StringBuilder motorcycleDataBuilder = new StringBuilder();

            motorcycleDataBuilder.AppendLine("---Motorcycle Details---");
            motorcycleDataBuilder.AppendFormat("{0}{1}", base.ToString(), Environment.NewLine);
            motorcycleDataBuilder.AppendLine("---Unique Motorcycle Details---");
            motorcycleDataBuilder.AppendFormat("Engine Capacity: {0}{1}", m_MotorCapacity, Environment.NewLine);
            motorcycleDataBuilder.AppendFormat("License Type: {0}{1}", m_LicenseType.ToString(), Environment.NewLine);

            return motorcycleDataBuilder.ToString();
        }
    }
}
