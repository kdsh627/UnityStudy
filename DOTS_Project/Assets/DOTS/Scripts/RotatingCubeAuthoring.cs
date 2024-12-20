using Unity.Entities;
using UnityEngine;

class RotatingCubeAuthoring : MonoBehaviour
{
    class RotatingCubeBaker : Baker<RotatingCubeAuthoring>
    {
        public override void Bake(RotatingCubeAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic); //��ǥ�� ���ڴٴ� ��. �Ⱦ��Ÿ� None

            AddComponent(entity, new RotatingCube());
        }
    }
}

public struct RotatingCube : IComponentData
{

}