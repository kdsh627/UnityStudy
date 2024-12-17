using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class JobTest : MonoBehaviour
{
    void Start()
    {
        NativeArray<int> array = new NativeArray<int>(1, Allocator.TempJob);
        NativeArray<int> array2 = new NativeArray<int>(1, Allocator.TempJob);

        SimpleJob job = new SimpleJob
        {
            a = 1,
            b = 2,
            result = array
        };

        SimpleJob job2 = new SimpleJob
        {
            a = 2,
            b = 3,
            result = array2
        };

        JobHandle handle = job.Schedule(); //멀티쓰레드로 만들기
        JobHandle handle2 = job2.Schedule(handle); //위에 쓰레드가 끝나고 실행됨

        handle2.Complete(); //핸들에 들어있는 Job이 끝날때까지 기다리기

        Debug.Log(job.result[0]); //수행이 끝난 후 접근
        Debug.Log(job2.result[0]); //수행이 끝난 후 접근


        array.Dispose(); //메모리에서 삭제
        array2.Dispose(); //메모리에서 삭제
    }


    public struct SimpleJob : IJob
    {
        public int a;
        public int b;
        public NativeArray<int> result;
        public void Execute()
        {
            result[0] = a + b;
        }
    }

}
