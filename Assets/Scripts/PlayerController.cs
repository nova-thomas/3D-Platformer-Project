using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Public Variables
    public float moveSpeed;
    public float jumpForce;
    public float gravityScale;
    public CharacterController controller;
    public Animator Anim;


    // Private Variables
    private Vector3 moveDir;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponentInChildren<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float yStore = moveDir.y;

        // Movement & Jump
        if (controller.isGrounded)
        {
            moveDir.y = 0f;
            moveDir = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
            moveDir = moveDir.normalized * moveSpeed;
            moveDir.y = yStore;
            
            if (Input.GetButtonDown("Jump"))
            {
                moveDir.y = jumpForce * 10;
            }
        }
        else
        {

            moveDir = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
            moveDir = moveDir.normalized * moveSpeed * 0.7f;
            moveDir.y = yStore;
        }

        // Gravity
        moveDir.y = moveDir.y + (Physics.gravity.y * gravityScale / 10);

        // Applying movement
        controller.Move(moveDir * Time.deltaTime);

        Anim.SetBool("isGrounded", controller.isGrounded);
        Anim.SetFloat("Speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
    }
}
