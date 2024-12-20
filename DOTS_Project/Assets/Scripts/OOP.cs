using UnityEngine;

public class OOP : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int maxObject = 100000;
    void Start()
    {
        for(int i = 0; i < maxObject; i++)
        {
            GameObject go = Instantiate(prefab, transform); //�ڽ��� �ڽ����� ����
            go.GetComponent<Monster>().velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
    }
}
