using Unity.Entities;
using UnityEngine;

public class SpawnCubeConfigAuthoring : MonoBehaviour
{
    public GameObject cubePrefab;
    public int amountToSpawn;

    public class Baker : Baker<SpawnCubeConfigAuthoring>
    {
        public override void Bake(SpawnCubeConfigAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.None); //��� �Ŵ����� ��ǥ �ʿ����
            AddComponent(entity, new SpawnCubeConfig
            {
                cubePrefabEntity = GetEntity(authoring.cubePrefab, TransformUsageFlags.Dynamic),
                amountToSpawn = authoring.amountToSpawn
            });
        }
    }
}

public partial struct SpawnCubeConfig : IComponentData
{
    public Entity cubePrefabEntity;
    public int amountToSpawn;
}
