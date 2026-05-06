using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    public GameObject screen;
    public Button retry;
    public Button menu;
    public GameObject player;
    public Rigidbody playerRb;

    private PlayerScript playerScript;

    private void Awake()
    {
        playerScript = player.GetComponent<PlayerScript>();

        retry.onClick.AddListener(() =>
        {
            screen.SetActive(false);
            player.transform.position = playerScript.spawnZone;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            playerRb.useGravity = true;
            playerScript.finish = false;
        });

        menu.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });
    }
}
