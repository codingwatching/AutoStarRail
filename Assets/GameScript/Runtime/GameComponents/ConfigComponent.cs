using cfg;
using Cysharp.Threading.Tasks;
using Luban;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigComponent : GameComponent
{
    public Tables Tables { get; private set; }
    protected override async UniTask OnInitialize()
    {
        Log.Info("====��ʼ������====");
        Tables = new Tables();
        await Tables.LoadAll(Loader);
        Log.Info("====��ʼ���������====");
    }

    private async UniTask<ByteBuf> Loader(string tableName)
    {
        var resourceComponent = GameManager.Instance.Resource;
        var asset = await resourceComponent.LoadAssetAsync<TextAsset>($"{tableName}");
        return new ByteBuf(asset.bytes);
    }
}
