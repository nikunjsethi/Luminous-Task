using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMovement : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float sensitivity = 0.005f;
    public GameObject sphereHolder;
    private Vector2 moveInput;
    private Vector2 mouseInput;
   
    public bool hasHit = false;
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {

        Movement();
        Raycast();
    }


    void Movement()
    {
        // Get keyboard input
        moveInput = Keyboard.current.wKey.isPressed ? Vector2.up : (Keyboard.current.sKey.isPressed ? Vector2.down : Vector2.zero) +
                    (Keyboard.current.dKey.isPressed ? Vector2.right : (Keyboard.current.aKey.isPressed ? Vector2.left : Vector2.zero));

        // Move the camera based on keyboard input
        Vector3 moveDirection = new Vector3(moveInput.x, 0, moveInput.y);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Get mouse input
        mouseInput = Mouse.current.delta.ReadValue();

        // Rotate the camera based on mouse input
        Vector3 rotation = new Vector3(-mouseInput.y, mouseInput.x, 0) * sensitivity;
        transform.Rotate(rotation);

        transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0f);
    }

    void Raycast()
    {
        //To randomize RGBA values
        float RandomRed = Random.Range(0f, 1f);
        float RandomGreen = Random.Range(0f, 1f);
        float RandomBlue = Random.Range(0f, 1f);
        float RandomAlpha = Random.Range(0f, 1f);

        RaycastHit hit = HitCheck();
        if (hit.collider != null)
        {
            //This condition is for the first hit, because sphereHolder will be empty in the starting 
            if ((sphereHolder.gameObject==null || sphereHolder.gameObject.name == hit.collider.name) && !hasHit)
            {
                hasHit = true;
                hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = new Color(RandomRed, RandomGreen, RandomBlue, RandomAlpha);
                sphereHolder = hit.collider.gameObject;
                Debug.Log("Hit: " + hit.collider.gameObject.name);
            }
            //This is when we directly move our mouse from one sphere to another sphere, changing back the color of the old sphere to red, and new sphere to random color
            else if(hit.collider.name != sphereHolder.gameObject.name)
            {
                hasHit = true;
                sphereHolder.GetComponent<MeshRenderer>().material.color = Color.red;
                hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = new Color(RandomRed, RandomGreen, RandomBlue, RandomAlpha);
                sphereHolder = hit.collider.gameObject;
            }
        }
        //This is when the raycasting is not hitting anything, so it will change the color of the last hit back to red
        else
        {
            hasHit = false;
            if (sphereHolder != null)
                sphereHolder.GetComponent<MeshRenderer>().material.color = Color.red;
        }

    }
    

    RaycastHit HitCheck()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        RaycastHit hit;

        Physics.Raycast(ray, out hit);
        return hit;
    }
}
