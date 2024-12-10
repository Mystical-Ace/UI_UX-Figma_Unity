using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public Button[] menuButtons; // Assign menu buttons in the Inspector
    private int currentIndex = 0; // Tracks the currently selected button index


    public GameObject mainMenuFrame;
    public GameObject levelSelectFrame;
    public GameObject optionsFrame;
    public GameObject highScoresFrame;

    private enum Frame { MainMenu, Options, HighScores, LevelSelect }
    private Frame currentFrame = Frame.MainMenu; // Tracks the currently actiavte frame

    void Start()
    {
        HighlightButton(currentIndex); // Highlight the first button
        ShowMainMenu(); // Activate the Main Menu at start
    }

    void Update()
    {
        // Handle navigation only if on the Main Menu frame
        if (currentFrame == Frame.MainMenu)
        {
            // Move up with the W key
            if (Input.GetKeyDown(KeyCode.W))
            {
                currentIndex = (currentIndex - 1 + menuButtons.Length) % menuButtons.Length;
                HighlightButton(currentIndex);
            }

            // Move down with the S key
            if (Input.GetKeyDown(KeyCode.S))
            {
                currentIndex = (currentIndex + 1) % menuButtons.Length;
                HighlightButton(currentIndex);
            }

            // Activate the button with the E key
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Activated: " + menuButtons[currentIndex].name); // Log activated button name

                if (currentIndex == 0) // Start Button
                {
                    ShowLevelSelect();
                }
                else if (currentIndex == 1) // Options Button
                {
                    ShowOptions();
                }
                else if (currentIndex == 2) // High Scores Button
                {
                    ShowHighScores();
                }
            }
        }
        else
        {
            // On other frames, pressing E returns to the Main Menu
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("Returning to Main Menu from: " + currentFrame);
                ShowMainMenu();
            }
        }
    }

    private void HighlightButton(int index)
    {
        // Reset all buttons to their default colour
        for (int i = 0; i < menuButtons.Length; i++)
        {
            var colors = menuButtons[i].colors;
            colors.normalColor = Color.white; // Default colour
            menuButtons[i].colors = colors;
        }

        // Highlight the currently selected button
        var selectedColors = menuButtons[index].colors;
        selectedColors.normalColor = Color.gray; // Highlighted colour
        menuButtons[index].colors = selectedColors;
    }

    // Frame activation methods
    public void ShowMainMenu()
    {
        currentFrame = Frame.MainMenu;
        mainMenuFrame.SetActive(true);
        optionsFrame.SetActive(false);
        highScoresFrame.SetActive(false);
        levelSelectFrame.SetActive(false);
    }

    public void ShowOptions()
    {
        currentFrame = Frame.Options;
        mainMenuFrame.SetActive(false);
        optionsFrame.SetActive(true);
        highScoresFrame.SetActive(false);
        levelSelectFrame.SetActive(false);
    }

    public void ShowHighScores()
    {
        currentFrame = Frame.HighScores;
        mainMenuFrame.SetActive(false);
        optionsFrame.SetActive(false);
        highScoresFrame.SetActive(true);
        levelSelectFrame.SetActive(false);
    }

    public void ShowLevelSelect()
    {
        currentFrame = Frame.LevelSelect;
        mainMenuFrame.SetActive(false);
        optionsFrame.SetActive(false);
        highScoresFrame.SetActive(false);
        levelSelectFrame.SetActive(true);
    }
}
