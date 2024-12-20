using System.ComponentModel;
using Unity.Entities;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class RotateSpeedAuthoring : MonoBehaviour
{
    //Authring은 IComponentData를 Entity에 붙이는 역할을 한다
    //Editor에서는 실행되지만, 실제 빌드에선 실행되지 않는다.

    public float value;

    private class Baker : Baker<RotateSpeedAuthoring>
    {
        public override void Bake(RotateSpeedAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic); //좌표를 쓰겠다는 뜻. 안쓸거면 None

            AddComponent(entity, new RotateSpeed
            {
                value = authoring.value
            });
        }
    }

}

public struct RotateSpeed : IComponentData
{
    public float value;
}