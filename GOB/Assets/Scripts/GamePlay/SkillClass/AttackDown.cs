using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class AttackDown : Skill
{
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

    public void ReleaseAttackDown(Collision collision, Rigidbody rb, GameObject attackDownEffect)
    {
        //충돌 지점 계산
        Vector3 impactPoint = collision.contacts[0].point;

        //강체 constraints 설정
        rb.constraints &= ~(RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ);

        //비활성화 전 재실행을 고려하여 껐다 킴
        attackDownEffect.SetActive(false);
        attackDownEffect.SetActive(true);

        //충돌 지점으로 이펙트 이동
        attackDownEffect.transform.position = impactPoint;

        executeState = State.Idle;
    }

    protected override IEnumerator ExecuteRoutine(Rigidbody rb)
    {
        if (executeState == State.Idle)
        {
            executeState = State.AttackDown;
            rb.constraints |= RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            yield return new WaitForSeconds(0.3f);

            if (executeState == State.AttackDown)
            {
                rb.constraints &= ~RigidbodyConstraints.FreezePositionY;

                rb.AddForce(new Vector3(0, -speed, 0), ForceMode.Impulse);
            }
        }
    }

    public void ForcedRealseAttackDown(Rigidbody rb)
    {
        rb.constraints &= ~(RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ);
        executeState = State.Idle;
    }
}
