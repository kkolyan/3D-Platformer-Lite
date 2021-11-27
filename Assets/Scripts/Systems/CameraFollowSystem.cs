using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CameraFollowSystem : IEcsRunSystem
    {

        public void Run(EcsSystems ecsSystems)
        {
            var filter = ecsSystems.GetWorld().Filter<PlayerComponent>().End();
            var cameraFilter = ecsSystems.GetWorld().Filter<CameraComponent>().End();
            var playerPool = ecsSystems.GetWorld().GetPool<PlayerComponent>();
            var cameraPool = ecsSystems.GetWorld().GetPool<CameraComponent>();

            foreach (int cameraEntity in cameraFilter)
            {
                ref var cameraComponent = ref cameraPool.Get(cameraEntity);
                foreach(var entity in filter)
                {
                    ref var playerComponent = ref playerPool.Get(entity);

                    Vector3 currentPosition = cameraComponent.cameraTransform.position;
                    Vector3 targetPoint = playerComponent.playerTransform.position + cameraComponent.offset;

                    cameraComponent.cameraTransform.position = Vector3.SmoothDamp(currentPosition, targetPoint, ref cameraComponent.curVelocity, cameraComponent.cameraSmoothness);
                }

                break;
            }
        }
    }
}
