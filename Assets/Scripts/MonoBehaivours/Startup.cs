using System;
using Leopotam.EcsLite;
using LeopotamGroup.Globals;
using System.Collections;
using System.Collections.Generic;
using Kk.LeoHot;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Platformer
{
    public class Startup : MonoBehaviour
    {
        private EcsWorld ecsWorld;
        private EcsSystems initSystems;
        private EcsSystems initOnceSystems;
        private EcsSystems updateSystems;
        private EcsSystems fixedUpdateSystems;
        [SerializeField] private ConfigurationSO configuration;
        [SerializeField] private Text coinCounter;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject playerWonPanel;
        [SerializeField] private SerializableEcsUniverse stash;

        [SerializeField] private bool preInitOnceDone;

        private void OnEnable()
        {
            ecsWorld = new EcsWorld();
            var gameData = new GameData();

            gameData.configuration = configuration;
            gameData.coinCounter = coinCounter;
            gameData.gameOverPanel = gameOverPanel;
            gameData.playerWonPanel = playerWonPanel;
            gameData.sceneService = Service<SceneService>.Get(true);

            if (!preInitOnceDone)
            {
                new EcsSystems(ecsWorld, gameData)
                    .Add(new PlayerInitOnceSystem())
                    .Init();
                preInitOnceDone = true;
            }

            initSystems = new EcsSystems(ecsWorld, gameData)
                .Add(new PlayerInitSystem());


            initOnceSystems = new EcsSystems(ecsWorld, gameData)
                .Add(new DangerousInitSystem())
                .Add(new CameraFollowInitSystem());

            updateSystems = new EcsSystems(ecsWorld, gameData)
                .Add(new PlayerInputSystem())
                .Add(new DangerousRunSystem())
                .Add(new CoinHitSystem())
                .Add(new BuffHitSystem())
                .Add(new DangerousHitSystem())
                .Add(new WinHitSystem())
                .Add(new SpeedBuffSystem())
                .Add(new JumpBuffSystem())
                .DelHere<HitComponent>();

            fixedUpdateSystems = new EcsSystems(ecsWorld, gameData)
                .Add(new PlayerMoveSystem())
                .Add(new CameraFollowSystem())
                .Add(new PlayerJumpSystem());


            if (stash == null)
            {
                stash = new SerializableEcsUniverse();
            }

            // do not care which `*Systems`, because they share the same world
            stash.UnpackState(initOnceSystems);

            initSystems.Init();
            initOnceSystems.Init();
            updateSystems.Init();
            fixedUpdateSystems.Init();
        }

        private void Start()
        {
            initOnceSystems.Run();
        }

        private void Update()
        {
            updateSystems.Run();
        }

        private void FixedUpdate()
        {
            fixedUpdateSystems.Run();
        }

        private void OnDisable()
        {
            stash.PackState(initOnceSystems);
        }

        private void OnDestroy()
        {
            initOnceSystems.Destroy();
            updateSystems.Destroy();
            fixedUpdateSystems.Destroy();
            ecsWorld.Destroy();
        }
    }
}