using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private void Start()
    {
        IBuilder builder = new Builder();
        builder.Animator_Part(MonsterState.Defalut);
        builder.State_Part(10, 3, MonsterState.Defalut);
        GameObject monster = builder.Result();

        Instantiate(monster);
    }
}
