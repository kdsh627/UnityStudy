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
        //�̱��� ���ÿ��� ������ �ϳ��� �־���� �ȱ׷� ����
        //���������� ����
        //�� ������Ʈ�� ���� ��ƼƼ�� �ϳ������Ѵٱ�
        SpawnCubeConfig spawnCubeConfig = SystemAPI.GetSingleton<SpawnCubeConfig>();

        //spwan��
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

        //�̷��� �� �� �����ϰ� ������Ʈ�� ������������
        this.Enabled = false; 
    }
}
