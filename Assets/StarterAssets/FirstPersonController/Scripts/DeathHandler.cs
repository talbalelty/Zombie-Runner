using UnityEngine;
using UnityEngine.InputSystem;

// Show Game Over screen when player dies
public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;

    // Start is called before the first frame update
    void Start()
    {
        gameOverCanvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HandleDeath()
    {
        gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
