using System.Collections;
using System.Linq.Expressions;
using UnityEngine;

public class HP : MonoBehaviour
{
    [SerializeField] private GameObject[] hp;

    private void Start()
    {
        foreach(GameObject go in hp)
        {
            go.SetActive(false);
        }

        StartCoroutine(AppearRoutine());
    }

    IEnumerator AppearRoutine()
    {
        foreach (GameObject go in hp)
        {
            yield return new WaitForSeconds(10.0f);
            go.SetActive(true);
        }
    }
}
