using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject pauseMenu;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    public static bool isPauseMenuActive { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //print("trigger enter");

        if (collision.CompareTag("Collectible"))
        {
            HUD_Mngr.Instance.numCollectiblesFound++;
            UI_Utilities.Instance.TextSprites["NumCollectiblesFound"].text = HUD_Mngr.Instance.numCollectiblesFound.ToString();

            Destroy(collision.gameObject);
        }

        else if (collision.CompareTag("Key"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("VictoryTarget"))
        {
            SceneManager.LoadScene("VictoryScene");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("OpenableDoor"))
        {
            if (collision.transform.parent.childCount == 1)  //means we got the key
            {
                UI_Utilities.Instance.TextSprites["TextInfo"].text = "Press E to open the door";

                if (InputSysController.Instance.Inputs["OpenDoor"].IsPressed())
                {
                    //print("triggered");

                    Destroy(collision.transform.parent.gameObject);
                }
            }

            else
            {
                UI_Utilities.Instance.TextSprites["TextInfo"].text = "It's closed";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //print("trigger exit");

        if (collision.CompareTag("OpenableDoor"))
        {
            UI_Utilities.Instance.TextSprites["TextInfo"].text = "";
        }
    }

    public void FlipSpriteX()
    {
        Vector2 moveDir = InputSysController.Instance.PlayerMoveDir;

        if (moveDir.x != 0)
        {
            float dirX = moveDir.x;
            bool flipX;

            dirX = Mathf.Clamp(dirX, -1, 0);
            flipX = Convert.ToBoolean(Mathf.Abs(dirX));

            sprite.flipX = flipX;
        }
    }

    public void Move()
    {
        Vector2 moveDir = InputSysController.Instance.PlayerMoveDir;

        rb.AddForce(moveSpeed * Time.deltaTime * moveDir);
    }

    public void TogglePauseMenu(bool buttonClicked = false)
    {
        if (InputSysController.Instance.Inputs["TogglePauseMenu"].triggered || buttonClicked)
        {
            isPauseMenuActive = !isPauseMenuActive;
            pauseMenu.SetActive(isPauseMenuActive);
        }
    }

    // Update is called once per frame
    void Update()
    {
        TogglePauseMenu();

        FlipSpriteX();
        Move();

        //bool isOpenDoorInputPressed = InputSysController.Instance.Inputs["OpenDoor"].IsPressed();
        //print(isOpenDoorInputPressed);
    }
}
