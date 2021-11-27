using Leopotam.EcsLite;

namespace Platformer
{
    public class PlayerInitOnceSystem: IEcsInitSystem
    {
        public void Init(EcsSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();

            var playerEntity = ecsWorld.NewEntity();

            var playerPool = ecsWorld.GetPool<PlayerComponent>();
            playerPool.Add(playerEntity);
            var playerInputPool = ecsWorld.GetPool<PlayerInputComponent>();
            playerInputPool.Add(playerEntity);
        }
    }
}