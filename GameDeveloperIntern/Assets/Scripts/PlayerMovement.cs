using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    
    public float playerMovementSpeed = 5f;
    public GameObject gameController;


    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("game_controller");
    }

    // Update is called once per frame
    void Update()
    {
        float xMove = Input.GetAxis("Horizontal") * Time.deltaTime * playerMovementSpeed;
        if (xMove != 0 && gameController.GetComponent<GameController>().isStop)
        {
            gameController.GetComponent<GameController>().startGame();

        }
        
        if (Input.touchCount > 0)
        {
            if(gameController.GetComponent<GameController>().isStop){
                gameController.GetComponent<GameController>().startGame();
            }
            Touch touch = Input.GetTouch(0);
            xMove = 0;
            if (touch.position.x > Screen.width / 2) {
                xMove = 1 * Time.deltaTime * playerMovementSpeed;
            } else {
                xMove = -1 * Time.deltaTime * playerMovementSpeed;
            }
        }

        this.transform.Translate(0f, xMove, 0f);

        // initially, the temporary vector should equal the player's position
        Vector3 clampedPosition = this.transform.position;
        // Now we can manipulte it to clamp the y element
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -3f, 3f);
        // re-assigning the transform's position will clamp it
        this.transform.position = clampedPosition;
    }
}
