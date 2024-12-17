using UnityEngine;

public class HpObj : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && (Skill.executeState == State.AttackForward || Skill.executeState == State.AttackDown))
        {
            GameManager.Instance.Boss.DecreaseHp();
            GameManager.Instance.Boss.OnHit();
            gameObject.SetActive(false);
        }
    }
}
