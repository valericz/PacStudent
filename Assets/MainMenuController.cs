using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public void StartGame()
    {
        // Replace "GameLevel" with the name of your game scene
        SceneManager.LoadScene("GameLevel");
    }

    public void ShowInstructions()
    {
        // Placeholder for showing instructions
        Debug.Log("Instructions button clicked!");
        // You could load another scene or show an instructions panel
    }

    public void ExitGame()
    {
        // Exits the game
        Application.Quit();
        Debug.Log("Exit button clicked!"); // This will show in the console during development
    }
}
