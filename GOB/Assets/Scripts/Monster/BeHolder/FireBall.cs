using System.Collections;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] GameObject fireBallEffect;
    [SerializeField] GameObject explosionEffect;

    private bool isHit = false; 

    private void OnEnable()
    {
        isHit = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" || other.tag == "Ground")
        {
            gameObject.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
            fireBallEffect.SetActive(false);
            explosionEffect.SetActive(true);
            StartCoroutine(OffEffect());
        }

        if(other.tag == "Player" && !isHit)
        {
            GameManager.Instance.Player.DecreaseHp();
            isHit = true;
        }
    }

    IEnumerator OffEffect()
    {
        yield return new WaitForSeconds(2.5f);
        fireBallEffect.SetActive(true);
        explosionEffect.SetActive(false);
        gameObject.SetActive(false);
    }
}
