using Leopotam.EcsLite;
using UnityEngine;

namespace Platformer
{
    public class CameraFollowInitSystem: IEcsRunSystem
    {
        public void Run(EcsSystems ecsSystems)
        {
            var gameData = ecsSystems.GetShared<GameData>();

            var cameraEntity = ecsSystems.GetWorld().NewEntity();

            var cameraPool = ecsSystems.GetWorld().GetPool<CameraComponent>();
            cameraPool.Add(cameraEntity);
            ref var cameraComponent = ref cameraPool.Get(cameraEntity);

            cameraComponent.cameraTransform = Camera.main.transform;
            cameraComponent.cameraSmoothness = gameData.configuration.cameraFollowSmoothness;
            cameraComponent.curVelocity = Vector3.zero;
            cameraComponent.offset = new Vector3(0f, 1f, -9f);
        }
    }
}