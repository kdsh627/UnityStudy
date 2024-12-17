using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class PlayerClass : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject attackDownEffect;
    [SerializeField] private float rayLength = 3.0f;
    [SerializeField] private float boundForce = 8.0f;
    [SerializeField] private GameObject[] hpImage;

    private Rigidbody rb;
    private Vector3 normalVector;
    private Vector3 inputDirection;
    private AttackDown attackDown;
    private AttackForward attackForward;
    private BackStep backStep;
    private int hp = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        attackDown = GameManager.Instance.SkillDict["AttackDown"] as AttackDown;
        attackForward = GameManager.Instance.SkillDict["AttackForward"] as AttackForward;
        backStep = GameManager.Instance.SkillDict["BackStep"] as BackStep;
    }


    private void FixedUpdate()
    {
        Move();
    }

    //움직이기 전용
    private void Move()
    {
        Camera main = Camera.main;

        Vector3 cameraForward = main.transform.forward;
        Vector3 cameraRight = main.transform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 direction = (cameraForward * inputDirection.z + cameraRight * inputDirection.x).normalized;

        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    private void OnCollisionStay(Collision collision)
    {
        normalVector = collision.contacts[0].normal;

        if (normalVector.y > 0.75f)
        {
            Vector3 v = rb.linearVelocity;
            v.y = boundForce;
            rb.linearVelocity = v;

            //내려찍기 해제 로직
            if (Skill.executeState == State.AttackDown)
            {
                attackDown.ReleaseAttackDown(collision, rb, attackDownEffect);
            }
        }
    }

    //움직이기 키
    private void OnMove(InputValue value)
    {
        //키보드 입력 값
        Vector2 inputVec2 = value.Get<Vector2>();
        if (inputVec2 != null)
        {
            //움직이고 회전
            inputDirection = new Vector3(inputVec2.x, 0, inputVec2.y);
        }
    } 

    //내려찍기 키
    private void OnAttackDown()
    {
        //레이캐스트를 발사, 거리 내에 아무것도 없으면 실행
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, rayLength))
        {
            return;
        }
        else
        {
            attackDown.Execute(rb);
        }
    }

    //백스텝 키
    private void OnBackStep()
    {
        if(Skill.executeState == State.AttackDown)
        {
            attackDown.ForcedRealseAttackDown(rb);
        }
        attackForward.Effect.SetActive(false);

        backStep.Execute(rb);
    }

    //앞 공격 키
    private void OnAttackForward()
    {
        //레이캐스트를 발사, 거리 내에 아무것도 없으면 실행
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, rayLength))
        {
            return;
        }
        else
        {
            attackForward.Execute(rb);
        }
    }

    public void DecreaseHp()
    {
        hp--;
        hpImage[hp].SetActive(false);
        if (hp == 0)
        {
            GameManager.Instance.UI.OnGameOver();
            Time.timeScale = 0;
        }
    }
}
