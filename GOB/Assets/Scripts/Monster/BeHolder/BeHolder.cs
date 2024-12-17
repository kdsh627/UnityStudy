using System.Collections;
using UnityEngine;

public enum Boss
{
    Idle,
    FireBall,
    Earthquake,
    Burn,
    Summon,
    Hit,
    Die,
}


public class BeHolder : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform player;
    [SerializeField] private GameObject[] fireBallEffect;
    [SerializeField] private Transform fireBallPosition;
    [SerializeField] private float fireBallSpeed;
    [SerializeField] private GameObject burnEffect;
    [SerializeField] private GameObject earthquakeEffect;

    private int hp;
    private float randomValue;
    private Boss pattern;
    private Coroutine routine;

    public int Hp => hp;

    void Start()
    {
        hp = 10;
        pattern = Boss.Idle;
        routine = StartCoroutine(BossRoutine());
    }

    void Update()
    {
        if (routine == null)
        {
            routine = StartCoroutine(BossRoutine());
        }

        if (hp == 0)
        {
            if (routine != null)
            {
                StopCoroutine(routine);
                routine = null;
            }
        }

        Vector3 direction;
        Quaternion targetRotation;
        switch (pattern)
        {
            case Boss.Idle:
            case Boss.FireBall:
            case Boss.Earthquake:
                // 플레이어와의 방향 계산
                direction = player.position - transform.position;
                direction.y = 0; // y축 고정 (수평 회전만 적용)

                // 목표 회전 계산
                targetRotation = Quaternion.LookRotation(direction);

                // 부드러운 회전
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.5f * rotationSpeed * Time.deltaTime);
                break;
            case Boss.Burn:
                // 플레이어와의 방향 계산
                direction = player.position - transform.position;
                direction.y = 0; // y축 고정 (수평 회전만 적용)

                // 목표 회전 계산
                targetRotation = Quaternion.LookRotation(direction);

                // 부드러운 회전
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.3f * rotationSpeed * Time.deltaTime);
                break;
        }
    }

    IEnumerator BossRoutine()
    {
        anim.SetTrigger("Idle");
        pattern = Boss.Idle;
        float coolTime = 5.0f;

        while (coolTime >= 0.0f)
        {
            if (hp <= 0) yield break;
            coolTime -= Time.deltaTime;
            yield return null;
        }

        randomValue = UnityEngine.Random.Range(0, 100);
        Debug.Log(randomValue);

        //파이어볼, 어스퀘이크, 불뿜기, 소환
        if (randomValue < 50) pattern = Boss.FireBall;
        else if (randomValue < 80) pattern = Boss.Earthquake;
        else pattern = Boss.Burn;

        switch (pattern)
        {
            case Boss.FireBall:
                for (int i = 0; i < fireBallEffect.Length; i++)
                {
                    if (hp <= 0) yield break;
                    FireBall(i);
                    pattern = Boss.Idle;
                    yield return new WaitForSeconds(1.0f);
                    pattern = Boss.FireBall;
                }
                yield return new WaitForSeconds(1.5f);
                break;
            case Boss.Earthquake:
                if (hp <= 0) yield break;
                StartCoroutine(Gathering(2.0f));
                yield return new WaitForSeconds(2.0f);
                Earthquake();
                yield return new WaitForSeconds(3.5f);
                earthquakeEffect.SetActive(false);
                break;
            case Boss.Burn:
                if (hp <= 0) yield break;
                StartCoroutine(Gathering(6.5f));
                yield return new WaitForSeconds(2.0f);
                burnEffect.SetActive(true);
                yield return new WaitForSeconds(5.0f);
                burnEffect.SetActive(false);
                break;
        }

        routine = null;
    }

    private void FireBall(int index)
    {
        anim.SetTrigger("FireBall");
        fireBallEffect[index].transform.position = fireBallPosition.position;

        Vector3 target = player.position - fireBallPosition.position;
        target.Normalize();

        fireBallEffect[index].SetActive(true);
        fireBallEffect[index].GetComponent<Rigidbody>().AddForce(target * fireBallSpeed, ForceMode.Impulse);
    }
    private void Earthquake()
    {
        anim.SetTrigger("Earthquake");
        earthquakeEffect.SetActive(true);
    }

    IEnumerator Gathering(float time)
    {
        while(time >= 0)
        {
            Debug.Log("응애");
            anim.SetTrigger("Burn");
            yield return null;
            time -= Time.deltaTime;
        }
    }

    public void DecreaseHp()
    {
        hp--;
        GameManager.Instance.UI.SetBossHp(hp);
        if(hp == 0)
        {
            anim.SetTrigger("Die");
            GameManager.Instance.UI.OnClear();
            Time.timeScale = 0;
        }
    }

    public void OnHit()
    {
        anim.SetTrigger("Hit");
    }
}
