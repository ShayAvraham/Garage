using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        // Defines
        private const string k_Yes = "Yes";
        private const string k_No = "No";

        // Data members
        private float m_CargoCapacity;
        private bool m_IsTransportingHazardousMaterials;

        public Truck(string i_ModelName, string i_LicenseNumber,List<Wheel> i_Wheel, Motor i_Motor, float i_CargoCapacity,bool i_IsTransportingHazardousMaterials) : base(i_ModelName, i_LicenseNumber, i_Wheel, i_Motor)
        {
            m_CargoCapacity = i_CargoCapacity;
            m_IsTransportingHazardousMaterials = i_IsTransportingHazardousMaterials;
        }

        public override string ToString()
        {
            StringBuilder truckDataBuilder = new StringBuilder();

            truckDataBuilder.AppendLine("---Truck Details---");
            truckDataBuilder.AppendFormat("{0}{1}", base.ToString(), Environment.NewLine);
            truckDataBuilder.AppendLine("---Unique Truck Details---");
            truckDataBuilder.AppendFormat("Cargo Capacity: {0}{1}", m_CargoCapacity, Environment.NewLine);
            truckDataBuilder.AppendFormat("Is The Truck Transporting Hazardous Materials: {0}{1}", m_IsTransportingHazardousMaterials ? k_Yes: k_No, Environment.NewLine);

            return truckDataBuilder.ToString();
        }
    }
}
