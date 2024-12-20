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

        JobHandle handle = job.Schedule(); //��Ƽ������� �����
        JobHandle handle2 = job2.Schedule(handle); //���� �����尡 ������ �����

        handle2.Complete(); //�ڵ鿡 ����ִ� Job�� ���������� ��ٸ���

        Debug.Log(job.result[0]); //������ ���� �� ����
        Debug.Log(job2.result[0]); //������ ���� �� ����


        array.Dispose(); //�޸𸮿��� ����
        array2.Dispose(); //�޸𸮿��� ����
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
