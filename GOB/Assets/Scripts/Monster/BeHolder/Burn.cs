using UnityEngine;

public class Burn : MonoBehaviour
{
    private bool isHit;
    private void OnEnable()
    {
        isHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !isHit)
        {
            GameManager.Instance.Player.DecreaseHp();
            isHit = true;
        }
    }
}
