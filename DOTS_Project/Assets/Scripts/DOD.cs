using UnityEngine;

public class DOD : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] int maxObject = 100000;

    Transform[] transforms;
    Vector3[] positions;
    Vector3[] velocities;

    void Start()
    {
        transforms = new Transform[maxObject];
        positions = new Vector3[maxObject];
        velocities = new Vector3[maxObject];

        for(int i = 0; i < maxObject; i++)
        {
            transforms[i] = Instantiate(prefab, transform).transform;
            velocities[i] = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < maxObject; i++)
        {
            positions[i] += velocities[i] * Time.deltaTime;
            transforms[i].localPosition = positions[i];
        }
    }
}
