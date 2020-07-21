using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class CreateNewVehicle
    {
        public enum eVehicleType
        {
            Fuel_Motorcycle = 1,
            Electric_Motorcycle,
            Fuel_Car,
            Electric_Car,
            Truck
        }

        // Defines
        public const float k_MinAirPressureInWheels = 0;
        public const float k_MinAmountOfEnergy = 0;
        // Motorcycle 
        public const int k_MotorcycleNumOfWheels = 2;
        public const float k_MotorcycleWheelsMaxAirPressure = 28;
        public const Motor.eFuelType k_MotorcycleFuelType = Motor.eFuelType.Octan95;
        public const float k_MotorcycleFuelTankCapacity = 5.5f;
        public const float k_MotorcycleMaxBatteryTime = 1.6f;
        // Car 
        public const int k_CarNumOfWheels = 4;
        public const float k_CarWheelsMaxAirPressure = 30;
        public const Motor.eFuelType k_CarFuelType = Motor.eFuelType.Octan96;
        public const float k_CarFuelTankCapacity = 42f;
        public const float k_CarMaxBatteryTime = 2.5f;
        // Truck 
        public const int k_TruckNumOfWheels = 16;
        public const float k_TruckWheelsMaxAirPressure = 26;
        public const Motor.eFuelType k_TruckFuelType = Motor.eFuelType.Soler;
        public const float k_TruckCapacityFuelTank = 120;

        public static Vehicle CreateVehicle(eVehicleType i_VehicleType, string i_VehicleLicenseNumber, string i_VehicleModelName, string i_VehicleWheelsManufacturerName, float i_CurrentAirPressureInWheels, float i_CurrentAmountOfEnergy, List<Object> i_UniqueVehiclePropertiesArr)
        {
            Vehicle vehicleToCreate = null;
            List <Wheel> vehicleWheels = createNewVehicleWheels(i_VehicleType, i_VehicleWheelsManufacturerName, i_CurrentAirPressureInWheels);
            Motor vehicleMotor = createNewVehicleMotor(i_VehicleType, i_CurrentAmountOfEnergy);

            switch (i_VehicleType)
            {
                case eVehicleType.Fuel_Motorcycle:
                    vehicleToCreate = new Motorcycle(i_VehicleModelName, i_VehicleLicenseNumber, vehicleWheels, vehicleMotor, (Motorcycle.eLicenseType)i_UniqueVehiclePropertiesArr[0], (int)i_UniqueVehiclePropertiesArr[1]);
                    break;
                case eVehicleType.Electric_Motorcycle:
                    vehicleToCreate = new Motorcycle(i_VehicleModelName, i_VehicleLicenseNumber, vehicleWheels, vehicleMotor, (Motorcycle.eLicenseType)i_UniqueVehiclePropertiesArr[0], (int)i_UniqueVehiclePropertiesArr[1]);
                    break;
                case eVehicleType.Fuel_Car:
                    vehicleToCreate = new Car(i_VehicleModelName, i_VehicleLicenseNumber, vehicleWheels, vehicleMotor, (Car.eCarColor)i_UniqueVehiclePropertiesArr[0],(Car.eNumOfCarDoors)i_UniqueVehiclePropertiesArr[1]);
                    break;
                case eVehicleType.Electric_Car:
                    vehicleToCreate = new Car(i_VehicleModelName, i_VehicleLicenseNumber, vehicleWheels, vehicleMotor, (Car.eCarColor)i_UniqueVehiclePropertiesArr[0], (Car.eNumOfCarDoors)i_UniqueVehiclePropertiesArr[1]);
                    break;
                case eVehicleType.Truck:
                    vehicleToCreate = new Truck(i_VehicleModelName, i_VehicleLicenseNumber, vehicleWheels, vehicleMotor, (float)i_UniqueVehiclePropertiesArr[0], (bool)i_UniqueVehiclePropertiesArr[1]);
                    break;
            }

            return vehicleToCreate;
        }

        private static List<Wheel> createNewVehicleWheels(eVehicleType i_VehicleType, string i_WheelManufacturerName, float i_CurrentAirPressureInWheels)
        {
            List<Wheel> newVehicleWheels = new List<Wheel>();
            int numOfWheels = 0;
            float wheelMaxAirPressure = 0;

            switch (i_VehicleType)
            {
                case eVehicleType.Fuel_Motorcycle:
                case eVehicleType.Electric_Motorcycle:
                    numOfWheels = k_MotorcycleNumOfWheels;
                    wheelMaxAirPressure = k_MotorcycleWheelsMaxAirPressure;
                    break;
                case eVehicleType.Fuel_Car:
                case eVehicleType.Electric_Car:
                    numOfWheels = k_CarNumOfWheels;
                    wheelMaxAirPressure = k_CarWheelsMaxAirPressure;
                    break;
                case eVehicleType.Truck:
                    numOfWheels = k_TruckNumOfWheels;
                    wheelMaxAirPressure = k_TruckWheelsMaxAirPressure;
                    break;
            }

            for (int i = 0; i < numOfWheels; i++)
            {
                newVehicleWheels.Add(new Wheel(i_WheelManufacturerName, wheelMaxAirPressure, i_CurrentAirPressureInWheels));
            }

            return newVehicleWheels;
        }

        private static Motor createNewVehicleMotor(eVehicleType i_VehicleType, float i_CurrentAmountOfEnergy)
        {
            Motor newVehicleMotor = null;

            switch (i_VehicleType)
            {
                case eVehicleType.Fuel_Motorcycle:
                    newVehicleMotor = new FuelMotor(k_MotorcycleFuelTankCapacity, i_CurrentAmountOfEnergy, k_MotorcycleFuelType);
                    break;
                case eVehicleType.Electric_Motorcycle:
                    newVehicleMotor = new ElectricMotor(k_MotorcycleMaxBatteryTime, i_CurrentAmountOfEnergy);
                    break;
                case eVehicleType.Fuel_Car:
                    newVehicleMotor = new FuelMotor(k_CarFuelTankCapacity, i_CurrentAmountOfEnergy, k_CarFuelType);
                    break;
                case eVehicleType.Electric_Car:
                    newVehicleMotor = new ElectricMotor(k_CarMaxBatteryTime, i_CurrentAmountOfEnergy);
                    break;
                case eVehicleType.Truck:
                    newVehicleMotor = new FuelMotor(k_TruckCapacityFuelTank, i_CurrentAmountOfEnergy, k_TruckFuelType);
                    break;
            }

            return newVehicleMotor;
        }
    }
}
