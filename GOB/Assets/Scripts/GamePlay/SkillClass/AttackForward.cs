using System.Collections;
using UnityEngine;

public class AttackForward : Skill
{
    [SerializeField] private int damage;
    [SerializeField] private GameObject effect;
    public GameObject Effect => effect;

    public override void Execute(Rigidbody rb)
    {
        bool ExcutePossible = IsMinCoolTime() && executeState == State.Idle;

        if (ExcutePossible)
        {
            InitCoolTime();
            StartCoroutine(ExecuteRoutine(rb));
            ReduceCoolTime();
        }
    }

    protected override IEnumerator ExecuteRoutine(Rigidbody rb)
    {
        if (executeState == State.Idle)
        {
            executeState = State.AttackForward;
            effect.SetActive(true);

            Vector3 cameraForward = Camera.main.transform.forward;
            Vector3 cameraDown = -Camera.main.transform.up;

            cameraForward.y = 0;

            cameraDown.x = 0;
            cameraDown.z = 0;

            cameraForward.Normalize();
            cameraDown.Normalize();

            Vector3 force = cameraForward * speed * 2 + cameraDown * speed;

            rb.linearVelocity = Vector3.zero;
            rb.AddForce(force, ForceMode.Impulse);
            yield return new WaitForSeconds(0.3f);

            if(executeState == State.AttackForward)
            {
                rb.linearVelocity = Vector3.zero;
            }
            executeState = State.Idle;

            yield return new WaitForSeconds(0.2f);
            effect.SetActive(false);
        }
        yield return null;
    }
}
