using UnityEngine;

public class Monster : MonoBehaviour
{
    public Vector3 position;
    public Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        position += velocity * Time.deltaTime;
        transform.localPosition = position;
    }
}
