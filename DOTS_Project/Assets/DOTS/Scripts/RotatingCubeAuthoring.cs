using Unity.Entities;
using UnityEngine;

class RotatingCubeAuthoring : MonoBehaviour
{
    class RotatingCubeBaker : Baker<RotatingCubeAuthoring>
    {
        public override void Bake(RotatingCubeAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic); //좌표를 쓰겠다는 뜻. 안쓸거면 None

            AddComponent(entity, new RotatingCube());
        }
    }
}

public struct RotatingCube : IComponentData
{

}