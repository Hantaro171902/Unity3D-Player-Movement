using UnityEngine;

public class PlayerBehavier : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 playerVelocity;
    private Vector3 moveDirection;
    private Vector3 targetVelocity;
    private Vector3 currentVelocity = Vector3.zero;

    private bool isGrounded;
    public float speed = 1f;
    public float gravity = -9.8f;
    public float jumpHeight = 1.5f;

    // Update to make movement more smooth
    public float acceleration = 10f;
    public float deceleration = 5f;

    // Add momentum to the player
    public float airControl = 0.4f;

    //public Animator animator;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        //animator = GetComponent<Animator>(); // Get the Animator component
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = controller.isGrounded;
        Debug.Log("Is Grounded: " + isGrounded);

    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Debug.Log("Collided with: " + hit.gameObject.name);
    }

    
    // Recieves input from the InputManager and apply to CharacterController
    public void ProcessMove(Vector2 input)
    {
        //Vector3 moveDirection = Vector3.zero;
        //moveDirection.x = input.x;
        //moveDirection.z = input.y;
        //controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);
        //playerVelocity.y += gravity * Time.deltaTime;

        // Calculate the target velocity
        targetVelocity =  transform.TransformDirection(new Vector3(input.x, 0, input.y)) * speed;
        moveDirection = new Vector3(input.x, 0, input.y);

        // Reduce movement in the air
        float controlFactor = isGrounded ? 1 : airControl;

        // Animations
        //if (moveDirection == Vector3.zero)
        //{
        //    // IdleA
        //    animator.SetFloat("Speed", 0f);

        //} else if (!Input.GetKey(KeyCode.LeftShift))
        //{
        //    // Walk
        //    animator.SetFloat("Speed", 0.3f);
        //}
        //else
        //{
        //    // Run
        //    animator.SetFloat("Speed", 0.7f);
        //}

        // Smmooth accerlation and deceleration
        currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, Time.deltaTime * acceleration * controlFactor);
        controller.Move(currentVelocity * Time.deltaTime);

        // controller.Move(transform.TransformDirection(moveDirection) * speed * Time.deltaTime);

        // Check if the player is on the ground
        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f; // Set a small negative value to keep the player grounded
        }
        else
        {
            playerVelocity.y += gravity * Time.deltaTime; // Apply gravity only when not grounded
        }

        controller.Move(playerVelocity * Time.deltaTime);
        Debug.Log(playerVelocity.y);

        
    }


    public void Jump()
    {
        if (isGrounded)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * 2.0f * -gravity);
        }
    }
}
