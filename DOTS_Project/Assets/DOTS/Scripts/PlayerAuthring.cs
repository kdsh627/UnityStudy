using Unity.Entities;
using UnityEngine;

class PlayerAuthring : MonoBehaviour
{
    class PlayerAuthringBaker : Baker<PlayerAuthring>
    {
        public override void Bake(PlayerAuthring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent(entity, new Player());
        }
    }
}

public struct Player : IComponentData
{

}

