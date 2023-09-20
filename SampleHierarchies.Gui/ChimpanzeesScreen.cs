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
    public sealed class ChimpanzeesScreen : Screen
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
        public ChimpanzeesScreen(IDataService dataService, IScreenDefinitionService screenDefinitionService)
        {
            _dataService = dataService;
            _screenDefinitionService = screenDefinitionService;
            ScreenDefinitionJson = "chimpanzeesScreen.json";
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

                        ChimpanzeesScreenChoices choice = (ChimpanzeesScreenChoices)Int32.Parse(choiceAsString);
                        switch (choice)
                        {
                            case ChimpanzeesScreenChoices.List:
                                ListChimpanzee();
                                break;

                            case ChimpanzeesScreenChoices.Create:
                                AddChimpanzee(); break;

                            case ChimpanzeesScreenChoices.Delete:
                                DeleteChimpanzee();
                                break;

                            case ChimpanzeesScreenChoices.Modify:
                                EditChimpanzeeMain();
                                break;

                            case ChimpanzeesScreenChoices.Exit:
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
            Console.WriteLine("1. List all Chimpanzees");
            Console.WriteLine("2. Create a new Chimpanzee");
            Console.WriteLine("3. Delete existing Chimpanzee");
            Console.WriteLine("4. Modify existing Chimpanzee");
            Console.Write("Please enter your choice: ");
        }

        #endregion // Public Methods

        #region Private Methods

        /// <summary>
        /// List all chimpanzees.
        /// </summary>
        private void ListChimpanzee()
        {
            Console.WriteLine();
            if (_dataService?.Animals?.Mammals?.Chimpanzees is not null &&
                _dataService.Animals.Mammals.Chimpanzees.Count > 0)
            {
                Console.WriteLine("Here's a list of chimpanzees :");
                int i = 1;
                foreach (Chimpanzee bear in _dataService.Animals.Mammals.Chimpanzees)
                {
                    Console.Write($"Chimpanzee number {i}, ");
                    bear.Display();
                    i++;
                }
            }
            else
            {
                Console.WriteLine("The list of chimpanzees is empty.");
            }
        }

        /// <summary>
        /// Add a chimpanzee.
        /// </summary>
        private void AddChimpanzee()
        {
            try
            {
                Chimpanzee chimpanzee = AddEditChimpanzee();
                _dataService?.Animals?.Mammals?.Chimpanzees?.Add(chimpanzee);
                Console.WriteLine("Chimpanzee with name: {0} has been added to a list of bears", chimpanzee.Name);
            }
            catch
            {
                Console.WriteLine("Invalid input.");
            }
        }

        /// <summary>
        /// Deletes a chimpanzees.
        /// </summary>
        private void DeleteChimpanzee()
        {
            try
            {
                Console.Write("What is the name of the chimpanzee you want to delete? ");
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Chimpanzee? chimpanzee = (Chimpanzee?)(_dataService?.Animals?.Mammals?.Chimpanzees
                       ?.FirstOrDefault(c => c is not null && string.Equals(c.Name, name)));
                if (chimpanzee is not null)
                {
                    _dataService?.Animals?.Mammals?.Chimpanzees?.Remove(chimpanzee);
                    Console.WriteLine("Chimpanzee with name: {0} has been deleted from a list of chimpanzees", chimpanzee.Name);
                }
                else
                {
                    Console.WriteLine("Chimpanzee not found.");
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
        private void EditChimpanzeeMain()
        {
            try
            {
                Console.Write("What is the name of the chimpanzee you want to edit? ");
                string? name = Console.ReadLine();
                if (name is null)
                {
                    throw new ArgumentNullException(nameof(name));
                }
                Chimpanzee? chimpanzee = (Chimpanzee?)(_dataService?.Animals?.Mammals?.Chimpanzees
                    ?.FirstOrDefault(c => c is not null && string.Equals(c.Name, name)));
                if (chimpanzee is not null)
                {
                    Chimpanzee chimpanzeeEdited = AddEditChimpanzee();
                    chimpanzee.Copy(chimpanzeeEdited);
                    Console.Write("Chimpanzee after edit:");
                    chimpanzee.Display();
                }
                else
                {
                    Console.WriteLine("Chimpanzee not found.");
                }
            }
            catch
            {
                Console.WriteLine("Invalid input. Try again.");
            }
        }

        /// <summary>
        /// Adds/edit specific chimpanzee.
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        private Chimpanzee AddEditChimpanzee()
        {
            Console.Write("What name of the chimpanzee ? ");
            string? name = Console.ReadLine();
            Console.Write("What is the chimpanzee's age? ");
            string? ageAsString = Console.ReadLine();
            Console.Write("Does he have opposable thumbs? (True/False) ");
            string? opposableThumbsAsString = Console.ReadLine();
            Console.Write("Which social behavior has your chimpanzee? ");
            string? complexSocialBehavior = Console.ReadLine();
            Console.Write("Can he use tools? (True/False) ");
            string? toolUseAsString = Console.ReadLine();
            Console.Write("How high is your chimpanzee's intelligence? ");
            string? highIntelligenceAsString = Console.ReadLine();
            Console.Write("Which diet has your chimpanzee? ");
            string? flexibleDiet = Console.ReadLine();

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (ageAsString is null)
            {
                throw new ArgumentNullException(nameof(ageAsString));
            }
            if (opposableThumbsAsString is null)
            {
                throw new ArgumentNullException(nameof(opposableThumbsAsString));
            }
            if (complexSocialBehavior is null)
            {
                throw new ArgumentNullException(nameof(complexSocialBehavior));
            }
            if (toolUseAsString is null)
            {
                throw new ArgumentNullException(nameof(toolUseAsString));
            }
            if (highIntelligenceAsString is null)
            {
                throw new ArgumentNullException(nameof(highIntelligenceAsString));
            }
            if (flexibleDiet is null)
            {
                throw new ArgumentNullException(nameof(flexibleDiet));
            }

            int age = Int32.Parse(ageAsString);
            bool opposableThumbs = bool.Parse(opposableThumbsAsString);
            bool toolUse = bool.Parse(toolUseAsString);
            int highIntelligence = int.Parse(highIntelligenceAsString);
            Chimpanzee chimpanzee = new Chimpanzee(name, age, opposableThumbs, complexSocialBehavior, toolUse, highIntelligence, flexibleDiet );

            return chimpanzee;
        }

        #endregion // Private Methods
    }
}