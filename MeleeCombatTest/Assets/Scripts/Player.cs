using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{

    public float MoveSpeed = 10.0f;
    public Vector3 JumpVelocity = new Vector3(0, 15.0f, 0);
    private Vector2 oldMousePos;
    private bool canMove = true;
    private bool canAction = true;
    private bool jumping = false;
    Animator animator;
    Rigidbody rbody;
    NavMeshAgent navagent;
    // Use this for initialization
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rbody = GetComponent<Rigidbody>();
        foreach (Box box in this.GetComponentsInChildren<Box>())
        {
            box.owner = GetComponent<Unit>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (canMove)
        {
            Vector3 movedir = GetMovementVector();
            if (!jumping)
                animator.SetBool("Moving", movedir != Vector3.zero);
            transform.position += movedir * MoveSpeed * Time.deltaTime;
        }
        float horizontal = Input.GetAxis("Mouse X") * 360 * Time.deltaTime;
        transform.Rotate(0, horizontal, 0);
    }
    private void Update()
    {
        if (canAction)
        {
            if (Input.GetMouseButton(0))
            {
                canAction = false;
                StartCoroutine(_LockMovementAndAttack(0, .5f));
                animator.SetTrigger("Attack3Trigger");
            }
            else if (Input.GetMouseButton(1))
            {
                canAction = false;
                StartCoroutine(_LockMovementAndAttack(0, .5f));
                animator.SetTrigger("Attack6Trigger");
            }
            else if (Input.GetKey(KeyCode.Q))
            {
                canAction = false;
                StartCoroutine(_LockMovementAndAttack(0, .8f));
                animator.SetTrigger("AttackKick1Trigger");
            }
            else if (Input.GetKey(KeyCode.E))
            {
                canAction = false;
                StartCoroutine(_LockMovementAndAttack(0, .8f));
                animator.SetTrigger("AttackKick2Trigger");
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rbody.velocity = 5 * Vector3.up;
            rbody.detectCollisions = false;
            StartCoroutine(_Jump());

        }

    }
    private void LateUpdate()
    {

    }

    Vector3 GetMovementVector()
    {
        Vector3 output = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            output += transform.forward;
        if (Input.GetKey(KeyCode.A))
            output -= transform.right;
        if (Input.GetKey(KeyCode.S))
            output -= transform.forward;
        if (Input.GetKey(KeyCode.D))
            output += transform.right;
        if (output == Vector3.zero)
            return output;
        return output.normalized;
    }

    public IEnumerator _LockMovementAndAttack(float delayTime, float lockTime)
    {
        canMove = false;
        yield return new WaitForSeconds(delayTime);
        animator.SetBool("Moving", false);
        animator.applyRootMotion = true;
        yield return new WaitForSeconds(lockTime);
        canMove = true;
        canAction = true;
        animator.applyRootMotion = false;
    }

    public IEnumerator _Jump()
    {
        animator.SetInteger("Jumping", 1);
        animator.SetTrigger("JumpTrigger");
        yield return new WaitForSeconds(0.5f);
        rbody.detectCollisions = true;
        animator.SetInteger("Jumping", 0);
    }
}

