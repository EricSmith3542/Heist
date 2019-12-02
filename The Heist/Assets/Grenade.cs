using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float speed;
    public RigidBody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void toss(Vector3 forward)
    {
        gameObject.Transform.Velocity = forward * speed;
    }
    void OnCollisionEnter(Collision collision)
    {
        //explode or emit smoke
    }
}
