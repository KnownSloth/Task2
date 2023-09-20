using SampleHierarchies.Data;
using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace SampleHierarchies.Gui
{
    public sealed class PolarBearsScreen : Screen
    {
        #region Properties And Ctor

        /// <summary>
        /// Data service.
        /// </summary>
        private IDataService _dataService;
        private IScreenDefinitionService _screenDefinitionService;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="dataService">Data service reference</param>
        public PolarBearsScreen(IDataService dataService, IScreenDefinitionService screenDefinitionService)
        {
            _dataService = dataService;
            _screenDefinitionService = screenDefinitionService;
            ScreenDefinitionJson = "polarBearsScreen.json";
        }

        #endregion Properties And Ctor
        #region Public Methods

        /// <inheritdoc/>
        public override void Show()
        {
            Console.Clear();
            while (true)
            {
                while (true)
                {
                    ScreenDefinition dynamicMenu = (ScreenDefinition)_screenDefinitionService.Load(ScreenDefinitionJson);
                    if (dynamicMenu != null)
                    {
                        foreach (IScreenEntry item in dynamicMenu.LineEntries)
                        {
                            Console.BackgroundColor = item.BackgroundColor;
                            Console.ForegroundColor = item.ForegroundColor;
                            Console.WriteLine(item.Text);
                        }

                    }
                    else
                    {
                        mockedMenu();
                    }

                    string? choiceAsString = Console.ReadLine();

                    // Validate choice
                    try
                    {
                        if (choiceAsString is null)
                        {
                            throw new ArgumentNullException(nameof(choiceAsString));
                        }

                        PolarBearsScreenChoices choice = (PolarBearsScreenChoices)Int32.Parse(choiceAsString);
                        switch (choice)
                        {
                            case PolarBearsScreenChoices.List:
                                ListPolarBears();
                                break;

                            case PolarBearsScreenChoices.Create:
                                AddPolarBear(); break;

                            case PolarBearsScreenChoices.Delete:
                                DeletePolarBear();
                                break;

                            case PolarBearsScreenChoices.Modify:
                                EditPolarBearMain();
                                break;

                            case PolarBearsScreenChoices.Exit:
                                Console.WriteLine("Going back to parent menu.");
                                Console.Clear();
                                return;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Invalid choice. Try again.");
                    }
                }
            }
        }
            private void mockedMenu()
            {
            Console.WriteLine();
            Console.WriteLine("Your available choices are:");
            Console.WriteLine("0. Exit");
            Console.WriteLine("1. List all polar bears");
            Console.WriteLine("2. Create a new polar bear");
            Console.WriteLine("3. Delete existing polar bear");
            Console.WriteLine("4. Modify existing polar bear");
            Console.Write("Please enter your choice: ");
        }
            #endregion // Public Methods

            #region Private Methods

            /// <summary>
            /// List all bears.
            /// </summary>
            private void ListPolarBears()
        {
            Console.WriteLine();
            if (_dataService?.Animals?.Mammals?.PolarBears is not null &&
                _dataService.Animals.Mammals.PolarBears.Count > 0)
            {
                Console.WriteLine("Here's a list of bears :");
                int i = 1;
                foreach (PolarBear bear in _dataService.Animals.Mammals.PolarBears)
                {
                    Console.Write($"Bear number {i}, ");
                    bear.Display();
                    i++;
                }
            }
            else
            {
                Console.WriteLine("The list of bears is empty.");
            }
        }

        /// <summary>
        /// Add a bear.
        /// </summary>
        private void AddPolarBear()
        {
            try
            {
                PolarBear bear = AddEditPolarBear();
                _dataService?.Animals?.Mammals?.PolarBears?.Add(bear);
                Console.WriteLine("Bear with name: {0} has been added to a list of bears", bear.Name);
            }
            catch
            {
                Console.WriteLine("Invalid input.");
            }
        }

        /// <summary>
        /// Deletes a bears.
        /// </summary>
        private void DeletePolarBear()
        {
            try
            {
                Console.Write("What is the name of the polar bear you want to delete? ");
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                PolarBear? bear = (PolarBear?)(_dataService?.Animals?.Mammals?.PolarBears
                       ?.FirstOrDefault(p => p is not null && string.Equals(p.Name, name)));
                if (bear is not null)
                {
                    _dataService?.Animals?.Mammals?.PolarBears?.Remove(bear);
                    Console.WriteLine("Polar bear with name: {0} has been deleted from a list of polar bears", bear.Name);
                }
                else
                {
                    Console.WriteLine("Polar bear not found.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid input.");
            }
        }

        /// <summary>
        /// Edits an existing bear after choice made.
        /// </summary>
        private void EditPolarBearMain()
        {
            try
            {
                Console.Write("What is the name of the polar bear you want to edit? ");
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                PolarBear? bear = (PolarBear?)(_dataService?.Animals?.Mammals?.PolarBears
                    ?.FirstOrDefault(p => p is not null && string.Equals(p.Name, name)));
                if (bear is not null)
                {
                    PolarBear bearEdited = AddEditPolarBear();
                    bear.Copy(bearEdited);
                    Console.Write("Polar bear after edit:");
                    bear.Display();
                }
                else
                {
                    Console.WriteLine("Polar bear not found.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid input. Try again.");
            }
        }

        /// <summary>
        /// Adds/edit specific bear.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        private PolarBear AddEditPolarBear()
        {
            Console.Write("What name of the polar bear ? ");
            string? name = Console.ReadLine();
            Console.Write("What is the polar bear's age? ");
            string? ageAsString = Console.ReadLine();
            Console.Write("What is the polar bear`s thick fur coat? ");
            string? thickFurCoat = Console.ReadLine();
            Console.Write("Which paws has polar bear? ");
            string? largePaws = Console.ReadLine();
            Console.Write("Which diet has polar bear? ");
            string? carnivorousDiet = Console.ReadLine();
            Console.Write("Is your polar bear semi-aquatic? (True/False) ");
            string? semiAquasticAsString = Console.ReadLine();
            Console.Write("Which sense of smell has your polar bear? ");
            string? excellentSenseOfSmell = Console.ReadLine();

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (ageAsString is null)
            {
                throw new ArgumentNullException(nameof(ageAsString));
            }
            if (thickFurCoat is null)
            {
                throw new ArgumentNullException(nameof(thickFurCoat));
            }
            if (largePaws is null)
            {
                throw new ArgumentNullException(nameof(largePaws));
            }
            if (carnivorousDiet is null)
            {
                throw new ArgumentNullException(nameof(carnivorousDiet));
            }
            if (semiAquasticAsString is null)
            {
                throw new ArgumentNullException(nameof(semiAquasticAsString));
            }
            if (excellentSenseOfSmell is null)
            {
                throw new ArgumentNullException(nameof(excellentSenseOfSmell));
            }

            int age = Int32.Parse(ageAsString);
            bool semiAquastic = bool.Parse(semiAquasticAsString);
            PolarBear bear = new PolarBear(name, age, thickFurCoat, largePaws, carnivorousDiet, semiAquastic, excellentSenseOfSmell);

            return bear;
        }

        #endregion // Private Methods
    }
}

