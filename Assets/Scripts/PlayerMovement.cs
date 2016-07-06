using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody2D rbody;
    Animator anim;
    public static bool shouldUpdate;
    private static bool playerExists;

    public void updateCase(bool casd)
    {
        shouldUpdate = casd;
    }

    // Use this for initialization
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        shouldUpdate = true;
        if (!playerExists)
        {
            playerExists = true;
            DontDestroyOnLoad(transform.gameObject);
        }
        else {
            transform.gameObject.SetActive(false);
        }


    }

    void FixedUpdate()
    {
        if (shouldUpdate)
        {

            Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

            if (movement_vector != Vector2.zero)
            {
                anim.SetBool("iswalking", true);
                anim.SetFloat("input_x", movement_vector.x);
                anim.SetFloat("input_y", movement_vector.y);
            }
            else {
                anim.SetBool("iswalking", false);
            }

            rbody.MovePosition(rbody.position + movement_vector * Time.deltaTime);
        }
        else if (shouldUpdate == false)
        {
            anim.SetBool("iswalking", false);
        }

    }
}
