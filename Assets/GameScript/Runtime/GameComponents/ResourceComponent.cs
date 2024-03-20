using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UniFramework.Event;
using UnityEngine;
using YooAsset;

public class ResourceComponent : GameComponent
{
    /// <summary>
    /// ��Դϵͳ����ģʽ
    /// </summary>
    public EPlayMode PlayMode = EPlayMode.EditorSimulateMode;

    private const float UNLOAD_UNUSED_ASSETS_INTERVAL = 15f;
    private float _unloadUnusedAssetsCountdown = UNLOAD_UNUSED_ASSETS_INTERVAL;
    private ResourcePackage _gamePackage;

    #region ��������
    protected override async UniTask OnInitialize()
    {
        // ��ʼ���¼�ϵͳ
        UniEvent.Initalize();

        // ��ʼ����Դϵͳ
        YooAssets.Initialize();

        // ���ظ���ҳ��
        var go = Resources.Load<GameObject>("PatchWindow");
        GameObject.Instantiate(go);

        // ��ʼ������������
        PatchOperation operation = new PatchOperation("DefaultPackage", EDefaultBuildPipeline.BuiltinBuildPipeline.ToString(), PlayMode);
        YooAssets.StartOperation(operation);

        await operation.ToUniTask();

        // ����Ĭ�ϵ���Դ��
        _gamePackage = YooAssets.GetPackage("DefaultPackage");
        YooAssets.SetDefaultPackage(_gamePackage);

        // �л�����ҳ�泡��
        SceneEventDefine.ChangeToHomeScene.SendEventMessage();
    }

    private void Update()
    {
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

    #region ������Դ
    public async UniTask<T> LoadAssetAsync<T>(string assetPath) where T : Object
    {
        var handle = YooAssets.LoadAssetAsync<T>(assetPath);
        await handle.ToUniTask();
        return handle.GetAssetObject<T>();
    }

    public T LoadAssetSync<T>(string assetPath) where T : Object
    {
        var handle = YooAssets.LoadAssetSync<T>(assetPath);
        return handle.GetAssetObject<T>();
    }
    #endregion
}
