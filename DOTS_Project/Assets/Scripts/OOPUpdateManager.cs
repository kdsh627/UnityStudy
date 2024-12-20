using UnityEngine;

public class OOPUpdateManager : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int maxObject;

    MonsterUpdateManaged[] monsters;
    void Start()
    {
        monsters = new MonsterUpdateManaged[maxObject];
        for (int i = 0; i < maxObject; i++)
        {
            GameObject go = Instantiate(prefab, transform); //자신의 자식으로 생성
            monsters[i] = go.GetComponent<MonsterUpdateManaged>();
            monsters[i].velocity = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < maxObject; i++)
        {
            monsters[i].position += monsters[i].velocity * Time.deltaTime;
            monsters[i].transform.localPosition = monsters[i].position;
        }
    }
}
