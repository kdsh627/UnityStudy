using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;


//public partial class RotatingCubeSystem : SystemBase  //managed object를 쓸 수 있다. 즉 burst compile이 안된다.
public partial struct RoratingCubeSystem : ISystem //managed object를 쓸 수 없다.
{
    void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<RotateSpeed>();
    }

    //엔티티 중 내가 처리해야하는 데이터를 들고있는 애들을 골라낼 것임
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //RotatingCubeJob job = new RotatingCubeJob
        //{
        //    deltaTime = SystemAPI.Time.DeltaTime
        //};
        ////job.Run(); //잡을 바로 살행한다. 디버깅할때 편함
        ////job.Schedule();
        //job.ScheduleParallel(); //엔티티의 수가 적으면 이걸 적용해도 멀티쓰레드로 나뉘지 않음
        //                        //잡의 개수가 아닌 엔티티의 개수가 중요

        //데이터를 가져오는 것이 아닌 컴포넌트가 붙어있는지 여부만 확인
        foreach(var (localTransform, rotateSpeed) //var쓰는것과 안쓰는 것
            //foreach((RefRW<LocalTransform>, RefRO<RotateSpeed>) 
            in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>>().WithAll<RotatingCube>()) //.WithNone<Player>()은플레이어 타입에는 실행시키지 않음
        {
            float power = 1f;
            for(int i = 0; i < 100000; i++)
            {
                power *= 2;
                power /= 2;
            }

            //localTransform.ValueRW.Rotate(); => 얘는 복제를 돌리는거라서 진짜는 돌지않음
            localTransform.ValueRW = localTransform.ValueRO.RotateY(rotateSpeed.ValueRO.value * SystemAPI.Time.DeltaTime);
        }
    }
}

[WithNone(typeof(Player))] //플레이어라는 타입이 붙어있으면 잡을 실행시키지않음
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

        //localTransform.ValueRW.Rotate() => 얘는 복제를 돌리는거라서 진짜는 돌지않음
        localTransform= localTransform.RotateY(rotateSpeed.value * deltaTime);

    }
}
