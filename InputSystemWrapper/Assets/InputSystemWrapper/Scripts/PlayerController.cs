using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Controls controls;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        controls = GetComponent<Controls>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controls.GetButtonDown("Jump"))
        {
            rigidbody.AddForce(new Vector3(0,10f,0), ForceMode.Impulse);
        }
        rigidbody.AddForce(controls.GetAxes("Move").x * 1000f * Time.deltaTime, 0,controls.GetAxes("Move").y * 1000f * Time.deltaTime);
    }
}
