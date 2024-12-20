using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct RotationMovingCubeAspect : IAspect
{
    public readonly RefRO<RotatingCube> rotatingCube;
    public readonly RefRW<LocalTransform> localTransform;
    public readonly RefRO<Movement> movement;
    public readonly RefRO<RotateSpeed> rotateSpeed;
}
