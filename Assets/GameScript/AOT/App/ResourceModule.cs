using Cysharp.Threading.Tasks;
using Game.Log;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniFramework.Event;
using UnityEngine;
using YooAsset;

namespace Game
{
    public class ResourceModule : GameModule
    {
        private const float UNLOAD_UNUSED_ASSETS_INTERVAL = 15f;
        private float _unloadUnusedAssetsCountdown = UNLOAD_UNUSED_ASSETS_INTERVAL;
        private ResourcePackage _gamePackage;

        #region ��������
        protected override async UniTask OnInitialize()
        {
            // ��ʼ���¼�ϵͳ
            UniEvent.Initalize();

            // ��ʼ����Դϵͳ
            YooAssets.Initialize(UnityConsoleLog.Instance);

            // ���ظ���ҳ��
            var go = Resources.Load<GameObject>("PatchWindow");
            GameObject.Instantiate(go);

            // ��ʼ������������
            PatchOperation operation = new PatchOperation("DefaultPackage", EDefaultBuildPipeline.BuiltinBuildPipeline.ToString(), AppConst.PlayMode);
            YooAssets.StartOperation(operation);

            await operation.ToUniTask();

            // ����Ĭ�ϵ���Դ��
            _gamePackage = YooAssets.GetPackage("DefaultPackage");
            YooAssets.SetDefaultPackage(_gamePackage);
        }

        private void Update()
        {
            if (_gamePackage == null)
            {
                return;
            }
            // ��ʱж��δʹ�õ���Դ
            if (_unloadUnusedAssetsCountdown > 0)
            {
                _unloadUnusedAssetsCountdown -= Time.deltaTime;
            }
            else
            {
                _gamePackage.UnloadUnusedAssets();
                _unloadUnusedAssetsCountdown += UNLOAD_UNUSED_ASSETS_INTERVAL;
            }
        }
        #endregion
    }
}
