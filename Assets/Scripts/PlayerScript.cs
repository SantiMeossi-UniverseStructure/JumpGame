using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    private PlayerMoves inputActions;

    public GameObject Camera;
    public Rigidbody playerRb;
    public GameObject winScreen;

    private Vector2 walk;
    private Vector2 view;
    private float rotationX = 0f;

    [HideInInspector] public bool finish = false;
    public float speed = 5f;
    public float camSensibility = 50f;
    public float jumpForce = 3f;
    public Vector3 spawnZone = new Vector3(0f, 11.2f, 0f);

    private bool checkFloor()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.2f);
    }
    private void Jump()
    {
        if (checkFloor())
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KillZone"))
        {
            transform.position = spawnZone;
            playerRb.linearVelocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
            view = new Vector2(0, 0);
            transform.rotation = Quaternion.Euler(0, 0, 0);
            Camera.transform.rotation = Quaternion.Euler(0, 0, 0);
            rotationX = 0f;
        }
        if (other.gameObject.CompareTag("Finish"))
        {
            finish = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            playerRb.linearVelocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;
            view = new Vector2(0, 0);
            rotationX = 0f;
            winScreen.SetActive(true);
            playerRb.useGravity = false;
        }
    }

    private void Awake()
    {
        inputActions = new PlayerMoves();

        inputActions.Movement.Walk.performed += ctx => walk = ctx.ReadValue<Vector2>();
        inputActions.Movement.Walk.canceled += ctx => walk = new Vector2(0, 0);

        inputActions.Movement.View.performed += ctx => view = ctx.ReadValue<Vector2>();
        inputActions.Movement.View.canceled += ctx => view = new Vector2(0, 0);

        inputActions.Movement.Jump.performed += ctx => Jump();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (finish == false)
        {
            Vector3 forward = transform.forward;
            Vector3 right = transform.right;

            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

            transform.Rotate(new Vector3(0, view.x, 0) * Time.deltaTime * camSensibility);

            rotationX -= view.y * Time.deltaTime * camSensibility;
            rotationX = Mathf.Clamp(rotationX, -90f, 90f);
            Camera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

            transform.position += (((forward * walk.y) + (right * walk.x)) * speed * Time.deltaTime);
        }
        playerRb.angularVelocity = Vector3.zero;
    }

    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();
}
