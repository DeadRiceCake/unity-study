using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public void OnPressPlayAgainButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnPressGoToMenuButton()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
