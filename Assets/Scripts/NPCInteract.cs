using UnityEngine;
using System.Collections;

public class NPCInteract : MonoBehaviour
{

    public static bool isTalking = false;

    public GameObject NPC;
    private GameObject player;

    Animator anim;

    void Start()
    {
        player = GameObject.Find("Player");
        anim = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.Space) &&
                    PlayerMovement.playerGuyOrientation == PlayerMovement.playerOrientation.RIGHT &&
                    NPC.transform.position.x - player.transform.position.x < 0.35 &&
                    NPC.transform.position.x - player.transform.position.x > 0 &&
                    Mathf.Abs(NPC.transform.position.y - player.transform.position.y) < 0.08)
        {
            isTalking = true;
            try
            {
                anim.SetBool("isFacingLeft", true);
                anim.SetBool("isFacingRight", false);
                anim.SetBool("isFacingUp", false);
                anim.SetBool("isFacingDown", false);
            }
            catch
            {
            }
            PlayerMovement.shouldUpdate = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) &&
            PlayerMovement.playerGuyOrientation == PlayerMovement.playerOrientation.LEFT &&
            player.transform.position.x - NPC.transform.position.x < 0.35 &&
            player.transform.position.x - NPC.transform.position.x > 0 &&
            Mathf.Abs(NPC.transform.position.y - player.transform.position.y) < 0.08)
        {
            isTalking = true;
            try
            {
                anim.SetBool("isFacingLeft", false);
                anim.SetBool("isFacingRight", true);
                anim.SetBool("isFacingUp", false);
                anim.SetBool("isFacingDown", false);
            }
            catch
            {
            }

            PlayerMovement.shouldUpdate = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) &&
            PlayerMovement.playerGuyOrientation == PlayerMovement.playerOrientation.UP &&
            Mathf.Abs(NPC.transform.position.x - player.transform.position.x) < 0.08 &&
            NPC.transform.position.y - player.transform.position.y < 0.35 &&
            NPC.transform.position.y - player.transform.position.y > 0)
        {
            isTalking = true;
            try
            {
                anim.SetBool("isFacingLeft", false);
                anim.SetBool("isFacingRight", false);
                anim.SetBool("isFacingUp", false);
                anim.SetBool("isFacingDown", true);
            }
            catch
            {
            }
            PlayerMovement.shouldUpdate = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) &&
            PlayerMovement.playerGuyOrientation == PlayerMovement.playerOrientation.DOWN &&
            Mathf.Abs(NPC.transform.position.x - player.transform.position.x) < 0.08 &&
            player.transform.position.y - NPC.transform.position.y < 0.35 &&
            player.transform.position.y - NPC.transform.position.y > 0)
        {
            isTalking = true;
            try
            {
                anim.SetBool("isFacingLeft", false);
                anim.SetBool("isFacingRight", false);
                anim.SetBool("isFacingUp", true);
                anim.SetBool("isFacingDown", false);
            }
            catch
            {
            }
            PlayerMovement.shouldUpdate = false;
        }
        else
        {
            isTalking = false;
        }
    }
}
