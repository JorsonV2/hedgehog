using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{


    public float Speed = 100f;
    public float Gravity = -50f;
    public float GroundCheckRadius = 0.1f;
    public float WallCheckRadius = 0.35f;
    public float JumpForce = 10f;
    public float XSpeed = 100f;
    public float CurrentXPosition = 0;
    public float smooth = 6f;
    public float ChangeXJumpForce = 1f;
    public GameObject GroundCheck;
    public GameObject RightCheck;
    public GameObject LeftCheck;
    public GameObject Player;
    public GameObject Model;

    private CharacterController controller;
    private Rigidbody rb;
    private Animator animator;
    private Vector3 velocity;
    private bool IsGrounded = false;
    private bool isRolling = false;
    private bool RightWall = false;
    private bool LeftWall = false;
    private RaycastHit hit;
    private Quaternion rot;
    private float YModelRotation = 270f;




    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();
        for (var i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "groundcheck")
                GroundCheck = transform.GetChild(i).gameObject;
            else if (transform.GetChild(i).tag == "player")
            {
                Player = transform.GetChild(i).gameObject;
                Model = Player.transform.GetChild(0).gameObject;
                animator = Model.GetComponent<Animator>();
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(Player.transform.position, -Player.transform.up);
        if (Physics.Raycast(ray, out hit, 0.5f))
        {
            //Debug.DrawLine(Player.transform.position, hit.point, Color.red);
            rot = Quaternion.FromToRotation(Player.transform.up, hit.normal) * Player.transform.rotation;
            Player.transform.rotation = Quaternion.Lerp(Player.transform.rotation, rot, Time.deltaTime * smooth);
        }
        else
            Player.transform.rotation = Quaternion.Lerp(Player.transform.rotation, transform.rotation, Time.deltaTime * smooth);

        IsGrounded = Physics.CheckSphere(GroundCheck.transform.position, GroundCheckRadius, LayerMask.GetMask("Default"));

        RightWall = Physics.CheckSphere(RightCheck.transform.position, WallCheckRadius, LayerMask.GetMask("Default"));

        LeftWall = Physics.CheckSphere(LeftCheck.transform.position, WallCheckRadius, LayerMask.GetMask("Default"));
        //Debug.Log(RightWall);

        animator.SetBool("grounded", IsGrounded);
        //Debug.Log(IsGrounded);

        if (IsGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                velocity.y = JumpForce;
            }
        }
        else
        {
            velocity.y += Gravity * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.A))
            YModelRotation = 270f;
        if (Input.GetKeyDown(KeyCode.D))
            YModelRotation = 90f;
        if (Input.GetKeyDown(KeyCode.W) && !RightWall){
            CurrentXPosition -= 1;
            if(IsGrounded)
                velocity.y = ChangeXJumpForce;
        }     
        if (Input.GetKeyDown(KeyCode.S) && !LeftWall){
            CurrentXPosition += 1;
            if(IsGrounded)
                velocity.y = ChangeXJumpForce;
        }

        //Debug.Log(IsGrounded);
           

        Model.transform.localRotation = Quaternion.Lerp(Model.transform.localRotation, Quaternion.Euler(0, YModelRotation, 0), Time.deltaTime * 6f);

        //Debug.Log(Player.transform.eulerAngles);

        Vector3 move = new Vector3(0, 0, Input.GetAxis("Horizontal"));
        if (move != Vector3.zero && IsGrounded)
            animator.SetBool("walking", true);
        else
            animator.SetBool("walking", false);

        controller.Move(move * Time.deltaTime * Speed);
        controller.Move(velocity * Time.deltaTime);
        rb.velocity = new Vector3(0, velocity.y * Time.deltaTime, move.z * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(CurrentXPosition, transform.position.y, transform.position.z), XSpeed * Time.deltaTime);
    }
}
