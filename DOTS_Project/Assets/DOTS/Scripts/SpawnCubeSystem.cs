using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class SpawnCubeSystem : SystemBase
{
    protected override void OnCreate()
    {
        RequireForUpdate<SpawnCubeConfig> ();
    }

    protected override void OnUpdate()
    {
        //싱글톤 사용시에는 무조건 하나는 있어야함 안그럼 에러
        //여러개여도 에러
        //이 컴포넌트를 가진 엔티티가 하나여야한다구
        SpawnCubeConfig spawnCubeConfig = SystemAPI.GetSingleton<SpawnCubeConfig>();

        //spwan함
        for(int i = 0; i<spawnCubeConfig.amountToSpawn;i++)
        {
            Entity entity = EntityManager.Instantiate(spawnCubeConfig.cubePrefabEntity);
            EntityManager.SetComponentData(entity, new LocalTransform
            {
                Position = new float3(UnityEngine.Random.Range(-10f, 5f), 0.6f, UnityEngine.Random.Range(-4, 7f)),
                Rotation = quaternion.identity,
                Scale = 1f
            });
        }

        //이러면 한 번 실행하고 업데이트를 진행하지않음
        this.Enabled = false; 
    }
}
