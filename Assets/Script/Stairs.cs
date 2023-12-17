using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    public float speed = 5.0f;
    public float gravity = 9.8f;
    public float jumpForce = 8.0f;

    private Rigidbody rb;
    private bool isOnStairs = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isOnStairs)
        {
            HandleStairsMovement();
        }
        else
        {
            HandleNormalMovement();
        }
    }

    void HandleNormalMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputDir = new Vector3(horizontalInput, 0, verticalInput);
        inputDir = Camera.main.transform.TransformDirection(inputDir);
        inputDir.y = 0; // Ensure the character stays level with the ground

        float currentSpeed = speed;
        if (rb.velocity.magnitude < speed)
        {
            rb.AddForce(inputDir * currentSpeed);
        }

        ApplyGravity();
    }

    void HandleStairsMovement()
    {
        float verticalInput = Input.GetAxis("Vertical");

        // Verifica si el jugador est� presionando un bot�n espec�fico para subir o bajar
        if (verticalInput > 0 && Input.GetButtonDown("Jump"))
        {
            // Aqu� puedes colocar la l�gica de movimiento hacia arriba
        }
        else if (verticalInput < 0 && Input.GetButtonDown("Jump"))
        {
            // Aqu� puedes colocar la l�gica de movimiento hacia abajo
        }

        // Puedes mantener el resto del c�digo para el movimiento b�sico en las escaleras
        Vector3 moveDirection = Vector3.up;
        Vector3 movement = moveDirection * verticalInput * speed * Time.deltaTime;
        transform.Translate(movement);
        rb.AddForce(Vector3.down * gravity * Time.deltaTime);
    }

    void ApplyGravity()
    {
        if (!isOnStairs)
        {
            rb.AddForce(Vector3.down * gravity);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stairs"))
        {
            isOnStairs = true;
            rb.useGravity = false; // Disable gravity when on stairs
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Stairs"))
        {
            isOnStairs = false;
            rb.useGravity = true; // Enable gravity when leaving stairs
        }
    }
}

