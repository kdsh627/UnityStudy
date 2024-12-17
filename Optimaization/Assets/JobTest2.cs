using UnityEngine;

public class JobTest2 : MonoBehaviour
{

    void Update()
    {
        float startTime = Time.realtimeSinceStartup;

        ToughTask();

        Debug.Log((Time.realtimeSinceStartup - startTime) * 1000f + " ms ");
    }

    void ToughTask()
    {
        float value = 0;

        for(int i = 0; i < 100000; i++)
        {
        }
    }

}
