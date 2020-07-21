using System;
using System.Collections.Generic;
using System.Text;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        // Defines
        private const string k_NoVehiclesInTheGarageMessage = "There are no vehicles in the garage";
        private const string k_VehicleAlreadyExistsMessage = " Vehicle with this license number already exists in the garage";
        private const string k_VehicleNotFoundMessage = "Vehicle with this license number does not exist in the garage";
        private const string k_NoVehicleWithThisStatusMessage = "There are no vehicles in the garage under this status";

        // Data members
        private Dictionary<Vehicle, Customer> r_GarageDataBase;

        public Garage()
        {
            r_GarageDataBase = new Dictionary<Vehicle, Customer>();
        }

        public void AddVehicleToGarage(Vehicle i_VehicleToAdd, Customer i_Customer)
        {
            r_GarageDataBase.Add(i_VehicleToAdd, i_Customer);
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, Customer.eVehicleStatusInTheGarage i_NewVehicleStatus)
        {
            Vehicle desiredVehicle = getVehicleByLicenseNumber(i_LicenseNumber);

            if (desiredVehicle.Equals(null))
            {
                throw new KeyNotFoundException(k_VehicleNotFoundMessage);
            }

            r_GarageDataBase[desiredVehicle].VehicleStatusInTheGarage = i_NewVehicleStatus;
        }

        public void InflateAllVehicleWheelsToMax(string i_LicenseNumber)
        {
            Vehicle desiredVehicle = getVehicleByLicenseNumber(i_LicenseNumber);

            if (desiredVehicle.Equals(null))
            {
                throw new KeyNotFoundException(k_VehicleNotFoundMessage);
            }

            desiredVehicle.InflateAllWheelsToMaxAirPressure();
        }

        public void AddEnergyToVehicle(string i_LicenseNumber, float i_AmountOfEnergyToAdd, Motor.eFuelType i_FuelType = 0)
        {
            Vehicle desiredVehicle = getVehicleByLicenseNumber(i_LicenseNumber);

            if (desiredVehicle.Equals(null))
            {
                throw new KeyNotFoundException(k_VehicleNotFoundMessage);
            }

            desiredVehicle.AddEnergyToMotor(i_AmountOfEnergyToAdd, i_FuelType);
        }

        private Vehicle getVehicleByLicenseNumber(string i_LicenseNumber)
        {
            Vehicle desiredVehicle = null;

            foreach (Vehicle vehicle in r_GarageDataBase.Keys)
            {
                if (vehicle.LicenseNumber.Equals(i_LicenseNumber))
                {
                    desiredVehicle = vehicle;
                    break;
                }
            }

            return desiredVehicle;
        }

        public bool IsVehicleInTheGarage(string i_LicenseNumber)
        {
            bool isVehicleInTheGarage = false;

            foreach (Vehicle vehicle in r_GarageDataBase.Keys)
            {
                if (vehicle.LicenseNumber.Equals(i_LicenseNumber))
                {
                    isVehicleInTheGarage = true;
                    break;
                }
            }

            return isVehicleInTheGarage;
        }

        public Vehicle CreateVehicle(CreateNewVehicle.eVehicleType i_VehicleType, string i_VehicleLicenseNumber, string i_VehicleModelName, string i_VehicleWheelsManufacturerName, float i_CurrentAirPressureInWheels, float i_CurrentAmountOfEnergy, List<Object> i_UniqueVehiclePropertiesArr)
        {
            Vehicle vehicle = CreateNewVehicle.CreateVehicle(i_VehicleType, i_VehicleLicenseNumber, i_VehicleModelName, i_VehicleWheelsManufacturerName, i_CurrentAirPressureInWheels, i_CurrentAmountOfEnergy, i_UniqueVehiclePropertiesArr);

            return vehicle;
        }

        public Customer CreateCustomer(string i_OwnerName, string i_OwnerPhoneNumber)
        {
            Customer customer = new Customer(i_OwnerName, i_OwnerPhoneNumber);

            return customer;
        }
        
        public string ShowListOfLicenseNumbersAccordingToStatus(Customer.eVehicleStatusInTheGarage i_VehicleStatus = 0)
        {
            StringBuilder listOfLicenseNumbersBuilder = new StringBuilder();
            bool showAllVehicleLicenseNumber = false;
            int countLicenseNumber = 0;

            if (i_VehicleStatus == 0)
            {
                showAllVehicleLicenseNumber = true;
            }
            foreach (Vehicle vehicle in r_GarageDataBase.Keys)
            {
                if (showAllVehicleLicenseNumber || r_GarageDataBase[vehicle].VehicleStatusInTheGarage.Equals(i_VehicleStatus))
                {
                    listOfLicenseNumbersBuilder.AppendFormat("- {0}{1}", vehicle.LicenseNumber, Environment.NewLine);
                    countLicenseNumber++;
                }
            }
            if (countLicenseNumber == 0)
            {
                if(showAllVehicleLicenseNumber)
                {
                    throw new Exception(k_NoVehiclesInTheGarageMessage);
                }
                else
                {
                    throw new Exception(k_NoVehicleWithThisStatusMessage);
                }
            }

            return listOfLicenseNumbersBuilder.ToString();
        }

        public string ShowVehicleFullData(string i_LicenseNumber)
        {
            Vehicle desiredVehicle = getVehicleByLicenseNumber(i_LicenseNumber);

            if (desiredVehicle.Equals(null))
            {
                throw new KeyNotFoundException(k_VehicleNotFoundMessage);
            }

              StringBuilder vehicleDataBuilder = new StringBuilder();
              vehicleDataBuilder.AppendFormat("{0}{1}", Environment.NewLine, desiredVehicle.ToString());
          

            return vehicleDataBuilder.ToString();
        }


        public bool CheckMotorType(string i_LicenseNumber, Type i_MotorType)
        {
            bool isValidMotorType = false;
            Vehicle vehicle = getVehicleByLicenseNumber(i_LicenseNumber);
            if (vehicle.Motor.GetType().Equals(i_MotorType))
            {
                isValidMotorType = true;
            }
            return isValidMotorType;
        }

    }
}
