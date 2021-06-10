using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseCursor : MonoBehaviour
{
    private Vector2 lastMousePos;
    private bool initialized = false;
    private bool warped = false;
    private bool cursorHidden = false;
    void Start()
    {
        lastMousePos = Mouse.current.position.ReadValue();
        Cursor.visible = false;
        cursorHidden = true;
    }

    void Update()
    {
        if(!initialized && Mouse.current.wasUpdatedThisFrame)
        {
            lastMousePos = Mouse.current.position.ReadValue();
            initialized = true;
        }
        Vector2 newPos = Mouse.current.position.ReadValue();
        if(!warped)
        {
            Vector2 delta = newPos - lastMousePos;
            transform.position += new Vector3(delta.x, delta.y, 0) / 100;
            lastMousePos = newPos;
            
        }
        Vector2 screenCenter = new Vector2(Screen.width, Screen.height) / 2;
        float distanceToCenter = (newPos - screenCenter).magnitude;
        if(cursorHidden)
        {
            if(Cursor.visible)
            {
                cursorHidden = false;
                Debug.Log("HIDDEN " + transform.position);
                Mouse.current.WarpCursorPosition(new Vector3(screenCenter.x, screenCenter.y, 0) + transform.position * 100);
                lastMousePos = new Vector3(screenCenter.x, screenCenter.y, 0) + transform.position * 100;
                warped = true;
            }
            if(distanceToCenter > Mathf.Min(Screen.width, Screen.height) / 4)
            {
                if(!Cursor.visible)
                {
                    Mouse.current.WarpCursorPosition(screenCenter);
                    lastMousePos = screenCenter;
                    warped = true;
        
                }
                
            }
            else
            {
                warped = false;
            }

        }
    }
}
