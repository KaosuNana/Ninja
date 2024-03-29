using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetAxis("Vertical") == 1)
        {
            this.gameObject.transform.Translate(Vector3.up * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetAxis("Vertical") == -1)
        {
            this.gameObject.transform.Translate(Vector3.down * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetAxis("Horizontal") == -1)
        {
            this.gameObject.transform.Translate(Vector3.left * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetAxis("Horizontal") == 1)
        {
            this.gameObject.transform.Translate(Vector3.right * Time.deltaTime);
        }
    }
}
