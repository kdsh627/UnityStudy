using Unity.Entities;
using UnityEngine;
using Unity.Burst;
using Unity.Transforms;

partial struct HandleCubeSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        //foreach(var (localTransform, rotateSpeed, movement)
        //    in SystemAPI.Query<RefRW<LocalTransform>, RefRO<RotateSpeed>, RefRO<Movement>>().WithAll<RotatingCube>())
        
        foreach(var rotatingMovingCubeAspect in SystemAPI.Query<RotationMovingCubeAspect>())
        {
            rotatingMovingCubeAspect.localTransform.ValueRW = rotatingMovingCubeAspect.localTransform.ValueRO.RotateY(rotatingMovingCubeAspect.rotateSpeed.ValueRO.value * SystemAPI.Time.DeltaTime)
                .Translate(rotatingMovingCubeAspect.movement.ValueRO.movmentVector * SystemAPI.Time.DeltaTime);
        }
    }
}
