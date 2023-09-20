﻿using SampleHierarchies.Data;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Animals main screen.
/// </summary>
public sealed class AnimalsScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Data service.
    /// </summary>
    private IDataService _dataService;
    private ISettingsService _settingsService;

    /// <summary>
    /// Animals screen.
    /// </summary>
    private MammalsScreen _mammalsScreen;
    private IScreenDefinitionService _screenDefinitionService;
    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="animalsScreen">Animals screen</param>
    public AnimalsScreen(ISettingsService settingsService,
        IDataService dataService,
        MammalsScreen mammalsScreen, IScreenDefinitionService screenDefinitionService)
    {
        _settingsService = settingsService;
        _dataService = dataService;
        _mammalsScreen = mammalsScreen;
        _screenDefinitionService = screenDefinitionService;
        ScreenDefinitionJson = "animalsScreen.json";
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

                    AnimalsScreenChoices choice = (AnimalsScreenChoices)Int32.Parse(choiceAsString);
                    switch (choice)
                    {
                        case AnimalsScreenChoices.Mammals:
                            _mammalsScreen.Show();
                            break;

                        case AnimalsScreenChoices.Read:
                            ReadFromFile();
                            break;

                        case AnimalsScreenChoices.Save:
                            SaveToFile();
                            break;

                        case AnimalsScreenChoices.Exit:
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
        Console.BackgroundColor = _settingsService.settings.AnimalsScreen;
        Console.WriteLine();
        Console.WriteLine("Your available choices are:");
        Console.WriteLine("0. Exit");
        Console.WriteLine("1. Mammals");
        Console.WriteLine("2. Save to file");
        Console.WriteLine("3. Read from file");
        Console.Write("Please enter your choice: ");
        Console.BackgroundColor = ConsoleColor.Black;
    }
        #endregion // Public Methods

        #region Private Methods

        /// <summary>
        /// Save to file.
        /// </summary>
        private void SaveToFile()
        {
            try
            {
                Console.Write("Save data to file: ");
                var fileName = Console.ReadLine();
                if (fileName is null)
                {
                    throw new ArgumentNullException(nameof(fileName));
                }
                _dataService.Write(fileName);
                Console.WriteLine("Data saving to: '{0}' was successful.", fileName);
            }
            catch
            {
                Console.WriteLine("Data saving was not successful.");
            }
        }

        /// <summary>
        /// Read data from file.
        /// </summary>
        private void ReadFromFile()
        {
            try
            {
                Console.Write("Read data from file: ");
                var fileName = Console.ReadLine();
                if (fileName is null)
                {
                    throw new ArgumentNullException(nameof(fileName));
                }
                _dataService.Read(fileName);
                Console.WriteLine("Data reading from: '{0}' was successful.", fileName);
            }
            catch
            {
                Console.WriteLine("Data reading from was not successful.");
            }
        }

        #endregion // Private Methods
    } 

