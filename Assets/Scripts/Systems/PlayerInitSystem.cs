using System;
using Leopotam.EcsLite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        public void Init(EcsSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();

            EcsFilter playerFilter = ecsWorld.Filter<PlayerComponent>().End();

            int playerEntity = -1;
            foreach (int i in playerFilter) playerEntity = i;
            if (playerEntity < 0)
            {
                throw new Exception("player not found"); 
            }
            var gameData = ecsSystems.GetShared<GameData>();

            var playerPool = ecsWorld.GetPool<PlayerComponent>();
            ref var playerComponent = ref playerPool.Get(playerEntity);

            var playerGO = GameObject.FindGameObjectWithTag("Player");
            playerGO.GetComponentInChildren<GroundCheckerView>().groundedPool = ecsSystems.GetWorld().GetPool<GroundedComponent>();
            playerGO.GetComponentInChildren<GroundCheckerView>().playerEntity = playerEntity;
            playerGO.GetComponentInChildren<CollisionCheckerView>().ecsWorld = ecsWorld;
            playerComponent.playerSpeed = gameData.configuration.playerSpeed;
            playerComponent.playerTransform = playerGO.transform;
            playerComponent.playerJumpForce = gameData.configuration.playerJumpForce;
            playerComponent.playerCollider = playerGO.GetComponent<CapsuleCollider>();
            playerComponent.playerRB = playerGO.GetComponent<Rigidbody>();
        }
    }
}