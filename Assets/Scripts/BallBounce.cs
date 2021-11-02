using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBounce : MonoBehaviour
{
    public float bounceBaseForce;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        speed = GetComponent<Rigidbody>().velocity.magnitude;
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Platform")
        {
            Debug.DrawLine(collision.contacts[0].normal, collision.contacts[0].normal, Color.red, 5f);
            GameObject platform = collision.gameObject;
            GetComponent<Rigidbody>().AddForce(collision.contacts[0].normal * (bounceBaseForce * ((10+platform.transform.localScale.x) /platform.transform.localScale.x)+speed), ForceMode.Force);
        }
    }
}
