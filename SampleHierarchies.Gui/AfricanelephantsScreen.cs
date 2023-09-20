using SampleHierarchies.Data.Mammals;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SampleHierarchies.Services;
using SampleHierarchies.Data;
using SampleHierarchies.Interfaces.Data;

namespace SampleHierarchies.Gui
{
    public sealed class AfricanelephantsScreen : Screen
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
        public AfricanelephantsScreen(IDataService dataService,
        IScreenDefinitionService screenDefinitionService)
        {
            _dataService = dataService;
            _screenDefinitionService = screenDefinitionService;
            ScreenDefinitionJson = "africanElephantsScreen.json";

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

                        AfricanElephantsScreenChoices choice = (AfricanElephantsScreenChoices)Int32.Parse(choiceAsString);
                        switch (choice)
                        {
                            case AfricanElephantsScreenChoices.List:
                                ListElephants();
                                break;

                            case AfricanElephantsScreenChoices.Create:
                                AddElephant(); break;

                            case AfricanElephantsScreenChoices.Delete:
                                DeleteAfricanElephant();
                                break;

                            case AfricanElephantsScreenChoices.Modify:
                                EditElephantMain();
                                break;

                            case AfricanElephantsScreenChoices.Exit:
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
            Console.WriteLine("1. List all African elephants");
            Console.WriteLine("2. Create a new elephant");
            Console.WriteLine("3. Delete existing elephant");
            Console.WriteLine("4. Modify existing elephant");
            Console.Write("Please enter your choice: ");
        }
        #endregion // Public Methods

        #region Private Methods

        /// <summary>
        /// List all elephants.
        /// </summary>
        private void ListElephants()
        {
            Console.WriteLine();
            if (_dataService?.Animals?.Mammals?.AfricanElephants is not null &&
                _dataService.Animals.Mammals.AfricanElephants.Count > 0)
            {
                Console.WriteLine("Here's a list of elephants:");
                int i = 1;
                foreach (AfricanElephant elephant in _dataService.Animals.Mammals.AfricanElephants)
                {
                    Console.Write($"Elephant number {i}, ");
                    elephant.Display();
                    i++;
                }
            }
            else
            {
                Console.WriteLine("The list of elephants is empty.");
            }
        }

        /// <summary>
        /// Add an elephant.
        /// </summary>
        private void AddElephant()
        {
            try
            {
                AfricanElephant elephant = AddEditElephant();
                _dataService?.Animals?.Mammals?.AfricanElephants?.Add(elephant);
                Console.WriteLine("Elephant with name: {0} has been added to a list of elephants", elephant.Name);
            }
            catch
            {
                Console.WriteLine("Invalid input.");
            }
        }

        /// <summary>
        /// Deletes an elephant.
        /// </summary>
        private void DeleteAfricanElephant()
        {
            try
            {
                Console.Write("What is the name of the elephant you want to delete? ");
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                AfricanElephant? elephant = (AfricanElephant?)(_dataService?.Animals?.Mammals?.AfricanElephants
                       ?.FirstOrDefault(e => e is not null && string.Equals(e.Name, name)));
                if (elephant is not null)
                {
                    _dataService?.Animals?.Mammals?.AfricanElephants?.Remove(elephant);
                    Console.WriteLine("African Elephant with name: {0} has been deleted from a list of elephants", elephant.Name);
                }
                else
                {
                    Console.WriteLine("African elephant not found.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid input.");
            }
        }

        /// <summary>
        /// Edits an existing elephant after choice made.
        /// </summary>
        private void EditElephantMain()
        {
            try
            {
                Console.Write("What is the name of the african elephant you want to edit? ");
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                AfricanElephant? elephant = (AfricanElephant?)(_dataService?.Animals?.Mammals?.AfricanElephants
                    ?.FirstOrDefault(e =>e is not null && string.Equals(e.Name, name)));
                if (elephant is not null)
                {
                    AfricanElephant elephantEdited = AddEditElephant();
                    elephant.Copy(elephantEdited);
                    Console.Write("African elephant after edit:");
                    elephant.Display();
                }
                else
                {
                    Console.WriteLine("Elephant not found.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid input. Try again.");
            }
        }

        /// <summary>
        /// Adds/edit specific elephant.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        private AfricanElephant AddEditElephant()
        {
            Console.Write("What name of the elephant ? ");
            string? name = Console.ReadLine();
            Console.Write("What is the elephant's age? ");
            string? ageAsString = Console.ReadLine();
            Console.Write("What is the elelephant`s height? ");
            string? heightAsString= Console.ReadLine();
            Console.Write("What is the elelephant`s weight? ");
            string? weightAsString = Console.ReadLine();
            Console.Write("What is the elelephant`s tusk lenght? ");
            string? tuskLenghtAsString = Console.ReadLine();
            Console.Write("What is the elelephant`s long lifespan? ");
            string? longLifespanAsString = Console.ReadLine();
            Console.Write("Which social behavior have your elephant? ");
            string? socialBehavior =Console.ReadLine();
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (ageAsString is null)
            {
                throw new ArgumentNullException(nameof(ageAsString));
            }
            if (socialBehavior is null)
            {
                throw new ArgumentNullException(nameof(socialBehavior));
            }
            if (longLifespanAsString is null)
            {
                throw new ArgumentNullException(nameof(longLifespanAsString));
            }
            if (heightAsString is null)
            {
                throw new ArgumentNullException(nameof(heightAsString));
            }
            if (weightAsString is null)
            {
                throw new ArgumentNullException(nameof(weightAsString));
            }
            if (tuskLenghtAsString is null)
            {
                throw new ArgumentNullException(nameof(tuskLenghtAsString));
            }
            int age = Int32.Parse(ageAsString);
            float height = float.Parse(heightAsString);
            float weight = float.Parse(weightAsString);
            float tuskLenght = float.Parse(tuskLenghtAsString);
            int longLifespan = Int32.Parse(longLifespanAsString);
            AfricanElephant elephant = new AfricanElephant(name, age, height, weight, tuskLenght, longLifespan, socialBehavior);

            return elephant;
        }

        #endregion // Private Methods
    }
}
