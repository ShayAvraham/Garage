using System;
using System.Text;
using System.Collections.Generic;
using Ex03.GarageLogic;

namespace Ex03.ConsoleUI
{
    public class GarageUI
    {
        private enum eGarageStartMenuOptions
        {
            InsertVehicleToGarage = 1,
            ShowAllLicensesNumbers,
            ChangeVehicleStatus,
            InflateVehicleWheelsToMax,
            RefuelVehicle,
            ChargeElectricVehicle,
            ShowAllVehicleInfo,
            Quit
        }

        // Defines
        private const string k_ValueRangeErrorMessage = "Error: The quantity does not match the desired range of values";
        private const string k_InputIsNotBooleanErrorMessage = "Error: The answer must be Y for yes of N for no";
        private const string k_InputIsNotValidNameErrorMessage = "Error: input must contain only letters ";
        private const string k_InputIsNotNumberErrorMessage = "Error: input must be a number ";
        private const string k_InputOutOfRangeErrorMessage = "Error: input is out of range of valid values ";
        private const string k_VehicleNotExistErrorMessage = "Error: vehicle with this license number does not exist in the garage ";
        private const string k_EmptyInputErrorMessage = "Error: input cant be empty ";
        private const string k_CustomerName = "customer name";
        private const string k_CustomerPhoneNumber = "customer phoner number";
        private const string k_VehicleLicenseNumber = "vehicle license number";
        private const string k_CurrentAirPressureInWheels = "current air pressure in wheels";
        private const string k_CurrentAmountOfEnergy = "current amount of energy in the vehicle";
        private const string k_VehicleModelName = "vehicle model name";
        private const string k_WheelsManufacturerName = "vehicle wheels manufacturer name";
        private const string k_VehicleType = "vehicle types";
        private const string k_VehicleFuelType = "fuel types";
        private const string k_VehicleStatus = "modes of vehicle in the garage";
        private const string k_MotorCycleLicenseType = "motorcycle license type";
        private const string k_MotorCycleEngineCapacity = "motorcycle engine capacity";
        private const string k_CarColor = "car color";
        private const string k_CarNumOfDoors = "car number of doors";
        private const string k_TruckCargoCapacity = "truck cargo capacity";
        private const string k_NumberOfHoursToRecharge = "the number of hours to recharge the battery ";
        private const string k_AmountOfFuel = "the amount of fuel to refuel";
        private const string k_IsTruckTransportingHazardousMaterials = "if truck transporting hazardous materials (enter Y/N)";
        private const string k_DoYouWantFilterResults = "do you want to filter the results by vehicle stats (Y/N)";
        private const string k_Yes = "Y";
        private const string k_No = "N";

        // Data members
        private readonly Garage r_Garage;   

        
        public GarageUI()
        {
            this.r_Garage = new Garage();
        }

        
        public void Run()
        {
            int userStartMenuChoise = 0;

            Console.WriteLine("Welcome To Our Garage");
            while ((eGarageStartMenuOptions)userStartMenuChoise != eGarageStartMenuOptions.Quit)
            {
                showGarageStartMenu();
                userStartMenuChoise = getUserStartMenuChoise();
                try
                {
                    executeUserSelectedAction((eGarageStartMenuOptions)userStartMenuChoise);
                }
                catch(Exception ex)
                {
                    StringBuilder errorMessage = new StringBuilder();
                    errorMessage.AppendFormat("{0}{1}", Environment.NewLine, ex.Message);
                    Console.WriteLine(errorMessage);
                }

                Console.WriteLine("Press any key to go back to the start menu");
                Console.ReadLine();
                Console.Clear();
            }
        }

        private void showGarageStartMenu()
        {
            string startMenuStr = string.Format(@"Please choose one of the following action:
1. Insert a vehicle to the garage.
2. Show all licenses numbers of the vehicles in the garage.
3. Change vehicle status.
4. Inflate the vehicle wheels to maximum.
5. Refuel a Vehicle.
6. Charge an electric vehicle.
7. Show all vehicle information.
8. Quit

The desired action number: ");
            Console.Write(startMenuStr);
        }

        private int getUserStartMenuChoise()
        {
            int userStartMenuChoise = 0;
            bool isInputValid = false;

            while (!isInputValid)
            {
                try
                {
                    string userStartMenuChoiseInput = Console.ReadLine();
                    userStartMenuChoise = isEnumValid(userStartMenuChoiseInput, new eGarageStartMenuOptions());
                    break;
                }
                catch (Exception ex)
                {
                    StringBuilder outputMessage = new StringBuilder();
                    outputMessage.AppendFormat("{0}{1}{2}", Environment.NewLine, ex.Message, Environment.NewLine);
                    outputMessage.Append("Please reenter the desired action number: ");
                    Console.Write(outputMessage);
                }
            }
            
            return userStartMenuChoise;
        }
          
        private void executeUserSelectedAction(eGarageStartMenuOptions i_UserStartMenuChoise)
        {
            switch (i_UserStartMenuChoise)
            {
                case eGarageStartMenuOptions.InsertVehicleToGarage:
                    insertVehicleToGarage();
                    break;
                case eGarageStartMenuOptions.ShowAllLicensesNumbers:
                    showAllLicensesList();
                    break;
                case eGarageStartMenuOptions.ChangeVehicleStatus:
                    changeVehicleStatus();
                    break;
                case eGarageStartMenuOptions.InflateVehicleWheelsToMax:
                    inflateVehicleWheelsToMax();
                    break;
                case eGarageStartMenuOptions.RefuelVehicle:
                    refuelVehicle();
                    break;
                case eGarageStartMenuOptions.ChargeElectricVehicle:
                    chargeElectricVehicle();
                    break;
                case eGarageStartMenuOptions.ShowAllVehicleInfo:
                    showAllVehicleInfo();
                    break;
                case eGarageStartMenuOptions.Quit:
                    break;
            }
        }

        private void insertVehicleToGarage()
        {
            Vehicle vehicleToAdd = null;
            Customer customerToAdd = null;
            string vehicleToAddLicenseNumber = getNotEmptyStringFromUser(k_VehicleLicenseNumber);
            bool isVehicleAlreadyInTheGarage = r_Garage.IsVehicleInTheGarage(vehicleToAddLicenseNumber);

            if (isVehicleAlreadyInTheGarage)
            {
                r_Garage.ChangeVehicleStatus(vehicleToAddLicenseNumber, Customer.eVehicleStatusInTheGarage.InRepair);
                Console.Write("The vehicle with license number {0} is already in the garage. his status changed to in repair.{1}", vehicleToAddLicenseNumber, Environment.NewLine);
            }
            else
            {
                vehicleToAdd = getVehicleFromUser(vehicleToAddLicenseNumber);
                customerToAdd = getCustomerFromUser();
                r_Garage.AddVehicleToGarage(vehicleToAdd, customerToAdd);
                Console.WriteLine("{0}Successfully insert vehicle To Garage", Environment.NewLine);
            }
        }

        private void showAllLicensesList()
        {
            StringBuilder listOfLicenseNumbers = new StringBuilder();
            string userInput = getValidNameAsStringFromUser(k_DoYouWantFilterResults);
            string allLicensesListAcordingToStatus;

            if (isValidBooleanOptionFromUser(userInput))
            {
                Customer.eVehicleStatusInTheGarage vehicleStatus = (Customer.eVehicleStatusInTheGarage)getValidEnumOptionFromUser(k_VehicleStatus, new Customer.eVehicleStatusInTheGarage());
                allLicensesListAcordingToStatus = r_Garage.ShowListOfLicenseNumbersAccordingToStatus(vehicleStatus);
            }
            else
            {
                allLicensesListAcordingToStatus = r_Garage.ShowListOfLicenseNumbersAccordingToStatus();
            }

            listOfLicenseNumbers.AppendFormat("{0}The License Numbers Are:{0}", Environment.NewLine);
            listOfLicenseNumbers.AppendFormat(allLicensesListAcordingToStatus);


            Console.WriteLine(listOfLicenseNumbers);
        }

        private void changeVehicleStatus()
        {
            string licenseNumber = getNotEmptyStringFromUser(k_VehicleLicenseNumber);

            if (!r_Garage.IsVehicleInTheGarage(licenseNumber))
            {
                throw new Exception(k_VehicleNotExistErrorMessage);

            }

            Customer.eVehicleStatusInTheGarage newVehicleStatus = (Customer.eVehicleStatusInTheGarage)getValidEnumOptionFromUser(k_VehicleStatus, new Customer.eVehicleStatusInTheGarage());
            r_Garage.ChangeVehicleStatus(licenseNumber, newVehicleStatus);
            Console.WriteLine("{0}Successfully changed vehicle status",Environment.NewLine);
        }

        private void inflateVehicleWheelsToMax()
        {
            string licenseNumber = getNotEmptyStringFromUser(k_VehicleLicenseNumber);

            if (r_Garage.IsVehicleInTheGarage(licenseNumber))
            {
                r_Garage.InflateAllVehicleWheelsToMax(licenseNumber);
                Console.WriteLine("{0}Successfully inflate all vehicle wheels to maximum", Environment.NewLine);
            }
            else
            {
                throw new Exception(k_VehicleNotExistErrorMessage);
            }
        }

        private void refuelVehicle()
        {
            string licenseNumber = getNotEmptyStringFromUser(k_VehicleLicenseNumber);

            if (r_Garage.IsVehicleInTheGarage(licenseNumber))
            {
                if (r_Garage.CheckMotorType(licenseNumber,typeof(FuelMotor)))
                {
                    int fuelTypeChoise = getValidEnumOptionFromUser(k_VehicleFuelType, new Motor.eFuelType());
                    float amountOfFuelToRefuel = getValidFloatNumberFromUser(k_AmountOfFuel);
                    r_Garage.AddEnergyToVehicle(licenseNumber, amountOfFuelToRefuel, (Motor.eFuelType)fuelTypeChoise);
                    Console.WriteLine("{0}Successfully refuel the vehicle",Environment.NewLine);
                }
                else
                {
                    throw new ArgumentException("Error: You cant charge a fuel car");
                }
            }
            else
            {
                throw new Exception(k_VehicleNotExistErrorMessage);
            }
        }

        private void chargeElectricVehicle()
        {
            string licenseNumber = getNotEmptyStringFromUser(k_VehicleLicenseNumber);

            if (r_Garage.IsVehicleInTheGarage(licenseNumber))
            {
                if (r_Garage.CheckMotorType(licenseNumber, typeof(ElectricMotor)))
                {
                    float numOfHoursToRecharge = getValidFloatNumberFromUser(k_NumberOfHoursToRecharge);
                    r_Garage.AddEnergyToVehicle(licenseNumber, numOfHoursToRecharge);
                    Console.WriteLine("{0}Successfully charge the vehicle battery", Environment.NewLine);
                }
                else
                {
                    throw new ArgumentException("Error: You cant put fuel in electric car");
                }
            }
            else
            {
                throw new Exception(k_VehicleNotExistErrorMessage);
            }
        }

        private void showAllVehicleInfo()
        {
            string licenseNumber = getNotEmptyStringFromUser(k_VehicleLicenseNumber);

            if (r_Garage.IsVehicleInTheGarage(licenseNumber))
            {
                Console.WriteLine(r_Garage.ShowVehicleFullData(licenseNumber));
            }
            else
            {
                throw new Exception(k_VehicleNotExistErrorMessage);
            }
        }

        private void showEnumOptions(Enum i_EnumType)
        {
            int indexEnum = 1;
            StringBuilder enumBuilder = new StringBuilder();

            foreach (Enum enumValue in Enum.GetValues(i_EnumType.GetType()))
            {
                enumBuilder.AppendFormat("{0}. {1}{2}", indexEnum, enumValue.ToString(), Environment.NewLine);
                indexEnum++;
            }

            enumBuilder.Append("The desired option number: ");
            Console.Write(enumBuilder);
        }

        private int isEnumValid(string i_UserInput, Enum i_EnumType)
        {
            int userStartMenuChoise;
            bool isUserInputIsNumber = int.TryParse(i_UserInput, out userStartMenuChoise);
            int numOfEnumValues = i_EnumType.GetType().GetEnumValues().Length;

            if (!isUserInputIsNumber)
            {
                throw new FormatException(k_InputIsNotNumberErrorMessage);
            }

            if (userStartMenuChoise < 1 || userStartMenuChoise > numOfEnumValues)
            {
                throw new Exception(k_InputOutOfRangeErrorMessage);
            }

            return userStartMenuChoise;
        }


        // $G$ CSS-013 (-5) Bad variable name (should be in the form of: i_CamelCase).
        private bool isValidBooleanOptionFromUser(string i_userInput)
        {
            bool userChoise = false;

            if ((!i_userInput.Equals(k_Yes)) && (!i_userInput.Equals(k_No)))
            {
                throw new FormatException(k_InputIsNotBooleanErrorMessage);
            }
            else if (i_userInput.Equals(k_Yes))
            {
                userChoise = true;
            }

            return userChoise;
        }

        private Vehicle getVehicleFromUser(string i_vehicleLicenseNumber)
        {
            Vehicle newVehicle = null;
            CreateNewVehicle.eVehicleType vehicleType = (CreateNewVehicle.eVehicleType)getValidEnumOptionFromUser(k_VehicleType, new CreateNewVehicle.eVehicleType());
            string modelName = getNotEmptyStringFromUser(k_VehicleModelName);
            string wheelsManufacturerName = getNotEmptyStringFromUser(k_WheelsManufacturerName);
            float currentAirPressureInWheels = getCurrentAirPressureInWheelsFromUser(vehicleType);
            float currentAmountOfEnergy = getCurrentAmountOfEnergyFromUser(vehicleType);
            List<Object> uniqueVehiclePropertiesArr = getUniqueVehiclePropertiesListFromUser(vehicleType);
            newVehicle = r_Garage.CreateVehicle(vehicleType, i_vehicleLicenseNumber, modelName, wheelsManufacturerName, currentAirPressureInWheels, currentAmountOfEnergy, uniqueVehiclePropertiesArr);

            return newVehicle;
        }

        // $G$ DSN-002 (-10) The UI should not know Car\Truck\Motorcycle
        private List<Object> getUniqueVehiclePropertiesListFromUser(CreateNewVehicle.eVehicleType vehicleType)
        {
            List<Object> uniqueVehiclePropertiesList = null;

            switch (vehicleType)
            {
                case CreateNewVehicle.eVehicleType.Electric_Motorcycle:
                case CreateNewVehicle.eVehicleType.Fuel_Motorcycle:
                    uniqueVehiclePropertiesList = getUniqueMotorCyclePropertiesListFromUser();
                    break;
                case CreateNewVehicle.eVehicleType.Electric_Car:
                case CreateNewVehicle.eVehicleType.Fuel_Car:
                    uniqueVehiclePropertiesList = getUniqueCarPropertiesListFromUser();
                    break;
                case CreateNewVehicle.eVehicleType.Truck:
                    uniqueVehiclePropertiesList = getUniqueTruckPropertiesListFromUser();
                    break;
            }

            return uniqueVehiclePropertiesList;
        }

        private List<Object> getUniqueMotorCyclePropertiesListFromUser()
        {
            List<Object> uniqueMotorCyclePropertiesList = new List<Object>();
            Motorcycle.eLicenseType licenseType = (Motorcycle.eLicenseType)getValidEnumOptionFromUser(k_MotorCycleLicenseType, new Motorcycle.eLicenseType());
            int motorCapacity = int.Parse(getValidIntegerNumberAsStringFromUser(k_MotorCycleEngineCapacity));

            uniqueMotorCyclePropertiesList.Add(licenseType);
            uniqueMotorCyclePropertiesList.Add(motorCapacity);

            return uniqueMotorCyclePropertiesList;
        }

        // $G$ DSN-002 (-5) The UI should not know Car\Truck\Motorcycle
        private List<Object> getUniqueCarPropertiesListFromUser()
        {
            List<Object> uniqueCarPropertiesList = new List<Object>();
            Car.eCarColor carColor = (Car.eCarColor)getValidEnumOptionFromUser(k_CarColor, new Car.eCarColor());
            Car.eNumOfCarDoors numOfCarDoors = (Car.eNumOfCarDoors)getValidEnumOptionFromUser(k_CarNumOfDoors, new Car.eNumOfCarDoors());

            uniqueCarPropertiesList.Add(carColor);
            uniqueCarPropertiesList.Add(numOfCarDoors);

            return uniqueCarPropertiesList;
        }

        private List<Object> getUniqueTruckPropertiesListFromUser()
        {
            List<Object> uniqueTruckPropertiesList = new List<Object>();
            float cargoCapacity = float.Parse(getValidIntegerNumberAsStringFromUser(k_TruckCargoCapacity));
            string isTransportingHazardousMaterialsInput = getValidNameAsStringFromUser(k_IsTruckTransportingHazardousMaterials);
            bool isTransportingHazardousMaterials = isValidBooleanOptionFromUser(isTransportingHazardousMaterialsInput);

            uniqueTruckPropertiesList.Add(cargoCapacity);
            uniqueTruckPropertiesList.Add(isTransportingHazardousMaterials);

            return uniqueTruckPropertiesList;
        }

        private Customer getCustomerFromUser()
        {
            Customer newCustomer;
            string ownerName = getValidNameAsStringFromUser(k_CustomerName);
            string ownerPhoneNumber = getValidIntegerNumberAsStringFromUser(k_CustomerPhoneNumber);

            newCustomer = r_Garage.CreateCustomer(ownerName, ownerPhoneNumber);

            return newCustomer;
        }
 
        private string getNotEmptyStringFromUser(string i_RequestedInputFormat)
        {
            string inputString;

            Console.Write("{0}Please enter {1}: ", Environment.NewLine , i_RequestedInputFormat);
            inputString = Console.ReadLine();
            if (String.IsNullOrWhiteSpace(inputString))
            {
                throw new Exception(k_EmptyInputErrorMessage);
            }

            return inputString;
        }

        private string getValidNameAsStringFromUser(string i_RequestedInputFormat)
        {
            string inputString = getNotEmptyStringFromUser(i_RequestedInputFormat);
            bool isStringValidName = true;

            foreach(char character in inputString.ToCharArray())
            {
                if(!Char.IsLetter(character))
                {
                    isStringValidName = false;
                    break;
                }
            }

            if (!isStringValidName)
            {
                throw new Exception(k_InputIsNotValidNameErrorMessage);
            }

            return inputString;
        }

        private string getValidIntegerNumberAsStringFromUser(string i_RequestedInputFormat)
        {
            string inputString = getNotEmptyStringFromUser(i_RequestedInputFormat);
            int inputNumber;
            bool isStringValidNumber;

            isStringValidNumber = int.TryParse(inputString, out inputNumber);
            if (!isStringValidNumber)
            {
                throw new Exception(k_InputIsNotNumberErrorMessage);
            }

            return inputString;
        }

        // $G$ CSS-013 (-5) Bad variable name (should be in the form of: i_CamelCase).
        // $G$ DSN-002 (-5) The UI should not know Car\Truck\Motorcycle
        private float getCurrentAirPressureInWheelsFromUser(CreateNewVehicle.eVehicleType vehicleType)
        {
            float currentAirPressureInWheels;
            string userInput = getValidFloatNumberAsStringFromUser(k_CurrentAirPressureInWheels);

            currentAirPressureInWheels = float.Parse(userInput);
            switch (vehicleType)
            {
                case CreateNewVehicle.eVehicleType.Electric_Motorcycle:
                case CreateNewVehicle.eVehicleType.Fuel_Motorcycle:
                    if (currentAirPressureInWheels < CreateNewVehicle.k_MinAirPressureInWheels || currentAirPressureInWheels > CreateNewVehicle.k_MotorcycleWheelsMaxAirPressure)
                    {
                        throw new ValueOutOfRangeException(CreateNewVehicle.k_MinAirPressureInWheels, CreateNewVehicle.k_MotorcycleWheelsMaxAirPressure, k_ValueRangeErrorMessage);
                    }
                    break;
                case CreateNewVehicle.eVehicleType.Electric_Car:
                case CreateNewVehicle.eVehicleType.Fuel_Car:
                    if (currentAirPressureInWheels < CreateNewVehicle.k_MinAirPressureInWheels || currentAirPressureInWheels > CreateNewVehicle.k_CarWheelsMaxAirPressure)
                    {
                        throw new ValueOutOfRangeException(CreateNewVehicle.k_MinAirPressureInWheels, CreateNewVehicle.k_MotorcycleWheelsMaxAirPressure, k_ValueRangeErrorMessage);
                    }
                    break;
                case CreateNewVehicle.eVehicleType.Truck:
                    if (currentAirPressureInWheels < CreateNewVehicle.k_MinAirPressureInWheels || currentAirPressureInWheels > CreateNewVehicle.k_TruckWheelsMaxAirPressure)
                    {
                        throw new ValueOutOfRangeException(CreateNewVehicle.k_MinAirPressureInWheels, CreateNewVehicle.k_MotorcycleWheelsMaxAirPressure, k_ValueRangeErrorMessage);
                    }
                    break;
            }

            return currentAirPressureInWheels;
        }

        // $G$ CSS-013 (-5) Bad variable name (should be in the form of: i_CamelCase).
        // $G$ DSN-002 (-5) The UI should not know Car\Truck\Motorcycle
        private float getCurrentAmountOfEnergyFromUser(CreateNewVehicle.eVehicleType vehicleType)
        {
            float currentAmountOfEnergy;
            string userInput = getValidFloatNumberAsStringFromUser(k_CurrentAmountOfEnergy);

            currentAmountOfEnergy = float.Parse(userInput);
            switch (vehicleType)
            {
                case CreateNewVehicle.eVehicleType.Electric_Motorcycle:
                    if (currentAmountOfEnergy < CreateNewVehicle.k_MinAmountOfEnergy || currentAmountOfEnergy > CreateNewVehicle.k_MotorcycleMaxBatteryTime)
                    {
                        throw new ValueOutOfRangeException(CreateNewVehicle.k_MinAmountOfEnergy, CreateNewVehicle.k_MotorcycleMaxBatteryTime, k_ValueRangeErrorMessage);
                    }
                    break;
                case CreateNewVehicle.eVehicleType.Fuel_Motorcycle:
                    if (currentAmountOfEnergy < CreateNewVehicle.k_MinAmountOfEnergy || currentAmountOfEnergy > CreateNewVehicle.k_MotorcycleFuelTankCapacity)
                    {
                        throw new ValueOutOfRangeException(CreateNewVehicle.k_MinAmountOfEnergy, CreateNewVehicle.k_MotorcycleFuelTankCapacity, k_ValueRangeErrorMessage);
                    }
                    break;
                case CreateNewVehicle.eVehicleType.Electric_Car:
                    if (currentAmountOfEnergy < CreateNewVehicle.k_MinAmountOfEnergy || currentAmountOfEnergy > CreateNewVehicle.k_CarMaxBatteryTime)
                    {
                        throw new ValueOutOfRangeException(CreateNewVehicle.k_MinAmountOfEnergy, CreateNewVehicle.k_CarMaxBatteryTime, k_ValueRangeErrorMessage);
                    }
                    break;
                case CreateNewVehicle.eVehicleType.Fuel_Car:
                    if (currentAmountOfEnergy < CreateNewVehicle.k_MinAmountOfEnergy || currentAmountOfEnergy > CreateNewVehicle.k_CarFuelTankCapacity)
                    {
                        throw new ValueOutOfRangeException(CreateNewVehicle.k_MinAmountOfEnergy, CreateNewVehicle.k_CarFuelTankCapacity, k_ValueRangeErrorMessage);
                    }
                    break;
                case CreateNewVehicle.eVehicleType.Truck:
                    if (currentAmountOfEnergy < CreateNewVehicle.k_MinAmountOfEnergy || currentAmountOfEnergy > CreateNewVehicle.k_TruckCapacityFuelTank)
                    {
                        throw new ValueOutOfRangeException(CreateNewVehicle.k_MinAmountOfEnergy, CreateNewVehicle.k_TruckCapacityFuelTank, k_ValueRangeErrorMessage);
                    }
                    break;
            }

            return currentAmountOfEnergy;
        }

        private string getValidFloatNumberAsStringFromUser(string i_RequestedInputFormat)
        {
            string inputString = getNotEmptyStringFromUser(i_RequestedInputFormat);
            float inputNumber;
            bool isStringValidNumber;

            isStringValidNumber = float.TryParse(inputString, out inputNumber);
            if (!isStringValidNumber)
            {
                throw new Exception(k_InputIsNotNumberErrorMessage);
            }

            return inputString;
        }

        private float getValidFloatNumberFromUser(string i_RequestedInputFormat)
        {
            string inputString = getNotEmptyStringFromUser(i_RequestedInputFormat);
            float inputNumber;
            bool isStringValidNumber;

            isStringValidNumber = float.TryParse(inputString, out inputNumber);
            if (!isStringValidNumber)
            {
                throw new Exception(k_InputIsNotNumberErrorMessage);
            }

            return inputNumber;
        }

        private int getValidEnumOptionFromUser(string i_RequestedInputFormat, Enum i_EnumType)
        {
            int userEnumChoise = 0;
            string userEnumChoiseInput;

            Console.WriteLine("{0}Please choose one of the following {1}: ",Environment.NewLine , i_RequestedInputFormat);
            showEnumOptions(i_EnumType);
            userEnumChoiseInput = Console.ReadLine();
            userEnumChoise = isEnumValid(userEnumChoiseInput, i_EnumType);
            userEnumChoise = (int)i_EnumType.GetType().GetEnumValues().GetValue(userEnumChoise - 1);
           
            return userEnumChoise;
        }
    }
}
