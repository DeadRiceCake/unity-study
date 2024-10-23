using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        
    }

    public void OnPressStartButton()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void OnPressExitButton()
    {
        Application.Quit();
    }
}
