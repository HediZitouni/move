using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{

    [SerializeField] private Transform groundTransform = null;
    [SerializeField] LayerMask playerMask;
    private bool jumpPressed = false;
    private float jumpForce = 3.0f;

    private float horitontalInput;
    private float verticalInput;
    private float horitontalForce = 7.0f;
    private float verticalForce = 7.0f;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space")) {
            jumpPressed = true;
        }

        horitontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate() {
        rb.velocity = new Vector3(horitontalInput * horitontalForce, rb.velocity.y, verticalInput * verticalForce);

        if (Physics.OverlapSphere(groundTransform.position, 0.1f, playerMask).Length == 0) {
            return;
        }

        if (jumpPressed) {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpPressed = false;
        }
    }

    public void respawn() {
        rb.position = new Vector3(0,1,0);
    }
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.name == "deathZone") {
            respawn();
        }
    }
}
