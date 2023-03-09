using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private void Update()
    {
        transform.position = Input.mousePosition;
        Cursor.lockState = CursorLockMode.None;
    }
}
