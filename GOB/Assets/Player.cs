using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Vector3 moveDirection;
    [SerializeField] private float speed = 2.0f;
    Rigidbody rb;
    Vector3 normalVector;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        moveDirection.y = rb.linearVelocity.y;
        rb.linearVelocity = moveDirection;
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    normalVector = collision.contacts[0].normal;
    //    if (normalVector.y > 0.75f)
    //    {
    //        Vector3 v = rb.linearVelocity;
    //        v.y = 8.0f;
    //        rb.linearVelocity = v;
    //    }
    //}

    private void OnCollisionStay(Collision collision)
    {
        normalVector = collision.contacts[0].normal;
        if (normalVector.y > 0.75f)
        {
            Vector3 v = rb.linearVelocity;
            v.y = 8.0f;
            rb.linearVelocity = v;
        }
    }

    private void OnMove(InputValue value)
    {
        Vector2 inputVec2 = value.Get<Vector2>();
        if(inputVec2 != null)
        {
            moveDirection = new Vector3(inputVec2.x, 0, inputVec2.y) * speed;
        }
    }

    private void OnAttack()
    {
        rb.AddForce(new Vector3(0, -20.0f, 0), ForceMode.Impulse);
    }
}
