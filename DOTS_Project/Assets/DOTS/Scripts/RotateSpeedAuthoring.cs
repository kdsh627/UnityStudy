using System.ComponentModel;
using Unity.Entities;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class RotateSpeedAuthoring : MonoBehaviour
{
    //Authring�� IComponentData�� Entity�� ���̴� ������ �Ѵ�
    //Editor������ ���������, ���� ���忡�� ������� �ʴ´�.

    public float value;

    private class Baker : Baker<RotateSpeedAuthoring>
    {
        public override void Bake(RotateSpeedAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic); //��ǥ�� ���ڴٴ� ��. �Ⱦ��Ÿ� None

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