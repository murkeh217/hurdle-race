using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    bool alive = true;
    public float speed = 5;
    [SerializeField] Rigidbody rb;
    public float horizontalMultiplier = 1.2f;
    [SerializeField] float horizontalInput;
    public float speedIncreasePerPoint = 0.1f;
    [SerializeField] float jumpForce = 400f;
    [SerializeField] LayerMask groundMask;
    void FixedUpdate()
    {
        if (!alive) return;

        Vector3 forwardMove = transform.forward * speed * Time.fixedDeltaTime;
        Vector3 horizontalMove = transform.right * horizontalInput * speed * Time.fixedDeltaTime * horizontalMultiplier;
        rb.MovePosition(rb.position + forwardMove + horizontalMove);
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            Jump();
        }

        if (transform.position.y < -5) 
        {
            Die();
        }
    }

    public void Die() 
    {
        alive = false;
        //restart game
        Invoke("Restart", 2);
    }

    void Restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    void Jump() 
    {
        //check we are grounded
        float height = GetComponent<Collider>().bounds.size.y;
        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, (height / 2) + 0.1f, groundMask);

        //if grounded, jump
        rb.AddForce(Vector3.up * jumpForce);
    }
}
