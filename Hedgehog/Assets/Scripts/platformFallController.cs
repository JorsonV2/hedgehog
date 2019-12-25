using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformFallController : MonoBehaviour
{

    public float standStillTime = 2;

    private bool isStanding = false;
    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isStanding && standStillTime >= 0)
            standStillTime -= Time.deltaTime;

        if(standStillTime < 0){
            rigidbody.useGravity = true;
            rigidbody.constraints = RigidbodyConstraints.None;
        }
            

        if(transform.position.y < -10)
            Destroy(transform.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "player")
            isStanding = true;
    }
}
