using SampleHierarchies.Data;
using SampleHierarchies.Enums;
using SampleHierarchies.Interfaces.Data;
using SampleHierarchies.Interfaces.Services;
using SampleHierarchies.Services;

namespace SampleHierarchies.Gui;

/// <summary>
/// Application main screen.
/// </summary>
public sealed class MainScreen : Screen
{
    #region Properties And Ctor

    /// <summary>
    /// Data service.
    /// </summary>
    private IDataService _dataService;

    /// <summary>
    /// Setting service.
    /// </summary>
    private ISettingsService _settingService;

    /// <summary>
    /// Screen Definition Service.
    /// </summary>
    private IScreenDefinitionService _screenDefinitionService;

    /// <summary>
    /// Animals screen.
    /// </summary>
    private AnimalsScreen _animalsScreen;
    /// <summary>
    /// Setting Screen.
    /// </summary>
    private SettingScreen _settingScreen;

    /// <summary>
    /// Ctor.
    /// </summary>
    /// <param name="dataService">Data service reference</param>
    /// <param name="settingService">Settingservice reference</param>
    /// <param name="screenDefinitionService">Screen definition service reference</param>
    /// <param name="animalsScreen">Animals screen</param>
    /// <param name="settingScreen">Setting Screen</param>
    public MainScreen(
        IDataService dataService,
    ISettingsService settingService,
    IScreenDefinitionService screenDefinitionService,
        AnimalsScreen animalsScreen,
        SettingScreen settingScreen)
    {
        _dataService = dataService;
        _settingService = settingService;
        _screenDefinitionService = screenDefinitionService;
        _animalsScreen = animalsScreen;
        _settingScreen = settingScreen;
        ScreenDefinitionJson = "mainScreen.json";
    }

    #endregion Properties And Ctor

    #region Public Methods

    /// <inheritdoc/>
    public override void Show()
    {
        Console.Clear();
        while (true)
        {
            ScreenDefinition dynamicMenu = (ScreenDefinition)_screenDefinitionService.Load(ScreenDefinitionJson);
            if(dynamicMenu != null)
            {
                foreach (IScreenEntry item in dynamicMenu.LineEntries)
                {
                    Console.BackgroundColor = item.BackgroundColor;
                    Console.ForegroundColor= item.ForegroundColor;
                    Console.WriteLine(item.Text);
                }
                
            } else
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

                MainScreenChoices choice = (MainScreenChoices)Int32.Parse(choiceAsString);
                switch (choice)
                {
                    case MainScreenChoices.Animals:
                        _animalsScreen.Show();
                        break;

                    case MainScreenChoices.Settings:
                        _settingScreen.Show();
                        break;

                    case MainScreenChoices.Exit:
                        Console.WriteLine("Goodbye.");
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

    private void mockedMenu()
    {
        Console.BackgroundColor = _settingService.settings.MainScreen;
        Console.WriteLine();
        Console.WriteLine("Your available choices are:");
        Console.WriteLine("0. Exit");
        Console.WriteLine("1. Animals");
        Console.WriteLine("2. Create a new settings");
        Console.Write("Please enter your choice: ");
        Console.BackgroundColor = ConsoleColor.Black;
    }

    #endregion // Public Methods
}