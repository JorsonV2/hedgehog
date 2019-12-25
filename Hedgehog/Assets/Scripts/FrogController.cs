using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogController : MonoBehaviour
{
    public float speed = 1f;
    public float gravity = -8f;
    public float smooth = 6f;
    public float jumpForce = 10f;
    public float jumpInterval = 1f;
    public Vector3 spawnPosition;
    public GameObject groundChceck;


    private CharacterController controller;
    private RaycastHit hit;
    private Quaternion rot;
    private Vector3 velocity = new Vector3(0, 0, 0);
    private float direction = 1;
    private float jumpTime = 0f;
    private bool IsGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        spawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        if(Physics.Raycast(ray, out hit, 2f)){
            rot = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * smooth);
        }
        IsGrounded = Physics.CheckSphere(groundChceck.transform.position, 0.1f, LayerMask.GetMask("Default"));

        if(IsGrounded){
            jumpTime += Time.deltaTime;
            if(jumpTime >= jumpInterval){
                velocity.y = jumpForce;
                jumpTime = 0;
            }
            if(Vector3.Distance(spawnPosition, transform.position) > 1f){
                direction = -direction;
            }   
        }
        else{
            controller.Move(new Vector3(0, 0, direction) * speed * Time.deltaTime);
            velocity.y -= gravity * Time.deltaTime;
        }
            
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "player"){
            Destroy(transform.gameObject);
        }
            
    }
}
