﻿using System;
using YIUIBind;
using YIUIFramework;
using UnityEngine;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using YooAsset;
using Game.Log;

namespace Game.UI.UILogin
{
    /// <summary>
    /// Author  YIUI
    /// Date    2024.4.14
    /// </summary>
    public sealed partial class UILoginPanel : UILoginPanelBase
    {

        #region 生命周期

        protected override void Initialize()
        {
            u_ComBtn_EnterGame.onClick.AddListener(OnClickBtnEnterGame);
        }

        protected override void Start()
        {
            GameLog.Debug($"UILoginPanel Start");
        }

        protected override void OnEnable()
        {
            GameLog.Debug($"UILoginPanel OnEnable");
        }

        protected override void OnDisable()
        {
            GameLog.Debug($"UILoginPanel OnDisable");
        }

        protected override void OnDestroy()
        {
            GameLog.Debug($"UILoginPanel OnDestroy");
        }

        protected override async UniTask<bool> OnOpen()
        {
            await UniTask.CompletedTask;
            GameLog.Debug($"UILoginPanel OnOpen");
            return true;
        }

        protected override async UniTask<bool> OnOpen(ParamVo param)
        {
            return await base.OnOpen(param);
        }

        #endregion

        #region Event开始
        private async void OnClickBtnEnterGame()
        {
            var loginService = GameEntry.Ins.GetService<LoginService>();
            await loginService.Login(LoginChannel.Local, "0");
            Close();
        }

        #endregion Event结束

    }
}