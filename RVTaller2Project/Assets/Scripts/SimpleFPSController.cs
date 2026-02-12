using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class SimpleFPSController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 6f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    [Header("Mouse")]
    public float mouseSensitivity = 100f;

    [Header("Fall Check")]
    public float fallLimitY = -10f;

    [Header("Head Bob")]
    public float bobSpeed = 14f;
    public float bobAmount = 0.05f;

    [Header("Audio")]
    public AudioSource footstepAudio;


    float defaultCamY;
    float bobTimer;


    float yVelocity;
    float xRotation = 0f;

    CharacterController controller;
    Transform cam;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>().transform;
        defaultCamY = cam.localPosition.y;


        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Look();
        Move();
        CheckFall();
        HandleHeadBob();

    }

    void Look()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);

        cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (controller.isGrounded && yVelocity < 0)
            yVelocity = -2f;

        if (Input.GetButtonDown("Jump") && controller.isGrounded)
            yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

        yVelocity += gravity * Time.deltaTime;

        Vector3 velocity = move * speed;
        velocity.y = yVelocity;

        controller.Move(velocity * Time.deltaTime);
    }

    void HandleHeadBob()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        bool isMoving = Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;

        if (isMoving && controller.isGrounded)
        {
            bobTimer += Time.deltaTime * bobSpeed;

            float newY = defaultCamY + Mathf.Sin(bobTimer) * bobAmount;

            Vector3 camPos = cam.localPosition;
            camPos.y = newY;
            cam.localPosition = camPos;

            if (!footstepAudio.isPlaying)
                footstepAudio.Play();
        }
        else
        {
            bobTimer = 0;

            Vector3 camPos = cam.localPosition;
            camPos.y = Mathf.Lerp(cam.localPosition.y, defaultCamY, Time.deltaTime * 5f);
            cam.localPosition = camPos;

            footstepAudio.Stop();
        }
    }



    void CheckFall()
    {
        if (transform.position.y < fallLimitY)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(
                UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
            );
        }
    }
}
