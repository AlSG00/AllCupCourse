using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour {
    // private static string AxisHorizontal = "Horizontal";
    // private static string AxisVertical = "Vertical";

    [SerializeField] private Player player;
    [SerializeField] private Transform canvasTransform;
    [SerializeField] private GameObject gameOverScreen;
    
    private bool shownGameOverScreen = false;

    private void Update() {
        if (player != null) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                player.TryToShoot();
            }

            Vector3 playerMoveDirection = Vector3.zero;
            if (Input.GetKey(KeyCode.W))
            {
                playerMoveDirection += Vector3.up;
            }

            if (Input.GetKey(KeyCode.S))
            {
                playerMoveDirection += Vector3.down;
            }

            // Движение вверх
            if (Input.GetKey(KeyCode.A)) {
                playerMoveDirection += Vector3.left;
            }
            // Движение вниз
            if (Input.GetKey(KeyCode.D)) {
                playerMoveDirection += Vector3.right;
            }
            playerMoveDirection *= Time.deltaTime;
            // Vector3 playerMoveDirection = Time.fixedDeltaTime * (Vector3.right * Input.GetAxis(AxisHorizontal) + Vector3.up * Input.GetAxis(AxisVertical));
            player.Move(playerMoveDirection);
        } else {
            if (!shownGameOverScreen) {
                Instantiate(gameOverScreen, canvasTransform);
                shownGameOverScreen = true;
            }
            if (Input.GetKeyDown(KeyCode.R)) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

}
