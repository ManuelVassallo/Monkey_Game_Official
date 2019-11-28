using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D myBody;

    public float move_Speed = 2f;

    public float normal_Push = 10f;
    public float extra_Push = 14f;

    private bool initial_Push;

    private int push_Count;

    private bool player_Died;

    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {

        if (player_Died)
            return;

        //player(monkey) movement
        if(Input.GetAxisRaw("Horizontal") > 0)
        {
            myBody.velocity = new Vector2(move_Speed, myBody.velocity.y);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            myBody.velocity = new Vector2(-move_Speed, myBody.velocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {

        if(player_Died)
            return;

        if (target.tag == "ExtraPush")
        {
            if (!initial_Push)
            {
                initial_Push = true;

                myBody.velocity = new Vector2(myBody.velocity.x, 18f);

                target.gameObject.SetActive(false);

                SoundManager.instance.JumpSoundFX();

                // exit from the on trigger enter becuase of initial push
                return;

                // initial push

            }


        }
        //initial push

        if(target.tag == "NormalPush")
        {

            // when player hits the  banana game object
            // player will jump the normal jump height

            myBody.velocity = new Vector2(myBody.velocity.x, normal_Push);

            target.gameObject.SetActive(false);

            push_Count++;

            SoundManager.instance.JumpSoundFX();

            // score increase +1
            ScoreScript.scoreValue += 1;
        }

        if (target.tag == "ExtraPush")
        {

            // when player hits the larger banana game object
            // player will jump twice the normal jump height

            myBody.velocity = new Vector2(myBody.velocity.x, extra_Push);

            target.gameObject.SetActive(false);

            push_Count++;

            SoundManager.instance.JumpSoundFX();

            // score increase +2
            ScoreScript.scoreValue += 2;
        }

        if(push_Count == 2)
        {
            push_Count = 0;
            PlatformSpawner.instance.SpawnPlatforms();
        }

        if(target.tag == "FallDown" || target.tag == "Bird")
        {
            player_Died = true;

            // when player dies set score back to 0
            ScoreScript.scoreValue = 0;

            SoundManager.instance.GameOverSoundFX();

            GameManager.instance.RestartGame();
        }

    }





}
