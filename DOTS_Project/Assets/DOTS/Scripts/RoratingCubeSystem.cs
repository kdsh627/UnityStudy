using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;


//public partial class RotatingCubeSystem : SystemBase  //managed object�� �� �� �ִ�. �� burst compile�� �ȵȴ�.
public partial struct RoratingCubeSystem : ISystem //managed object�� �� �� ����.
{
    void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<RotateSpeed>();
    }

    //��ƼƼ �� ���� ó���ؾ��ϴ� �����͸� ����ִ� �ֵ��� ��� ����
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //RotatingCubeJob job = new RotatingCubeJob
        //{
        //    deltaTime = SystemAPI.Time.DeltaTime
        //};
        ////job.Run(); //���� �ٷ� �����Ѵ�. ������Ҷ� ����
        ////job.Schedule();
        //job.ScheduleParallel(); //��ƼƼ�� ���� ������ �̰� �����ص� ��Ƽ������� ������ ����
        //                        //���� ������ �ƴ� ��ƼƼ�� ������ �߿�

        //�����͸� �������� ���� �ƴ� ������Ʈ�� �پ��ִ��� ���θ� Ȯ��
        foreach(var (localTransform, rotateSpeed) //var���°Ͱ� �Ⱦ��� ��
            //foreach((RefRW<LocalTransform>, RefRO<RotateSpeed>) 
            in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>>().WithAll<RotatingCube>()) //.WithNone<Player>()���÷��̾� Ÿ�Կ��� �����Ű�� ����
        {
            float power = 1f;
            for(int i = 0; i < 100000; i++)
            {
                power *= 2;
                power /= 2;
            }

            //localTransform.ValueRW.Rotate(); => ��� ������ �����°Ŷ� ��¥�� ��������
            localTransform.ValueRW = localTransform.ValueRO.RotateY(rotateSpeed.ValueRO.value * SystemAPI.Time.DeltaTime);
        }
    }
}

[WithNone(typeof(Player))] //�÷��̾��� Ÿ���� �پ������� ���� �����Ű������
public partial struct RotatingCubeJob : IJobEntity
{
    public float deltaTime;
    public void Execute(ref LocalTransform localTransform, in RotateSpeed rotateSpeed)
    {
        float power = 1f;
        for (int i = 0; i < 100000; i++)
        {
            power *= 2;
            power /= 2;
        }

        //localTransform.ValueRW.Rotate() => ��� ������ �����°Ŷ� ��¥�� ��������
        localTransform= localTransform.RotateY(rotateSpeed.value * deltaTime);

    }
}
