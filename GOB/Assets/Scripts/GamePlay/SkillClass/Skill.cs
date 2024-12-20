using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum State
{
    Idle,
    AttackDown,
    BackStep,
    AttackForward
}


[Serializable]
public abstract class Skill : MonoBehaviour
{
    static public State executeState = State.Idle;

    [SerializeField] private string skillName;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] private Image coolTimeImage;
    [SerializeField] private float maxCoolTime;
    [SerializeField] protected float speed;

    private float coolTime;

    public abstract void Execute(Rigidbody rb);

    protected abstract IEnumerator ExecuteRoutine(Rigidbody rb);

    public void ReduceCoolTime()
    {
        StartCoroutine(ReduceTimeRoutine());
    }

    protected bool IsMinCoolTime()
    {
        return coolTime == 0.0f;
    }

    protected void InitCoolTime()
    {
        coolTimeImage.fillAmount = 0;
        coolTime = maxCoolTime;
    }

    private IEnumerator ReduceTimeRoutine()
    {
        while (coolTime >= 0.0f)
        {
            float reductionTime = Time.deltaTime / maxCoolTime;
            coolTime -= Time.deltaTime;

            coolTimeImage.fillAmount = coolTime / maxCoolTime;

            string t = TimeSpan.FromSeconds(coolTime).ToString("ss\\:ff");
            string[] tokens = t.Split(':');
            timeText.text = string.Format("{0}:{1}", tokens[0], tokens[1]);

            yield return new WaitForFixedUpdate();
        }
        coolTime = 0.0f;
        timeText.text = "00:00";
    }
}
