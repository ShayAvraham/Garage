using System;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Customer
    {
        public enum eVehicleStatusInTheGarage
        {
            InRepair = 1,
            Fixed = 2,
            PaidUp = 3
        }

        // Data members
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eVehicleStatusInTheGarage m_VehicleStatusInTheGarage = eVehicleStatusInTheGarage.InRepair;

        public Customer(string i_OwnerName, string i_OwnerPhoneNumber)
        {
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
        }

        // Properties
        public string OwnerName
        {
            get { return m_OwnerName; }
            set { m_OwnerName = value; }
        }

        public string OwnerPhonerNamber
        {
            get { return m_OwnerPhoneNumber; }
            set { m_OwnerPhoneNumber = value; }
        }

        public eVehicleStatusInTheGarage VehicleStatusInTheGarage
        {
            get { return m_VehicleStatusInTheGarage; }
            set { m_VehicleStatusInTheGarage = value; }
        }

        public override string ToString()
        {
            StringBuilder customerDataBuilder = new StringBuilder();

            customerDataBuilder.AppendFormat("Customer name: {0}{1}", m_OwnerName, Environment.NewLine);
            customerDataBuilder.AppendFormat("Vehicle model: {0}{1}", m_VehicleStatusInTheGarage, Environment.NewLine);

            return customerDataBuilder.ToString();
        }
    }
}
