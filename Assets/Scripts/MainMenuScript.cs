using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public Button startButton;
    public Button quitButton;

    private void Awake()
    {
        startButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });

        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
