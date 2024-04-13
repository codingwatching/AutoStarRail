using Cysharp.Threading.Tasks;
using Game.Log;
using System;
using System.Collections.Generic;
using UniFramework.Event;

namespace Game
{
    public enum LoginState
    {

    }

    public enum LoginChannel
    {
        Local = 0,
        Google = 1,
        Facebook = 2,
        Apple = 3,
        WeChat = 4,
        QQ = 5
    }

    [GameService(GameServiceLifeSpan.Game)]
    public class LoginService : GameService
    {
        #region Fields & Properties
        public LoginChannel LoginChannel { get; private set; }

        public string PlayerId { get; private set; }

        private Dictionary<Type, PlayerData> _cachePlayerDataMap = new();

        private EventGroup _eventGroup;
        #endregion

        #region Life Cycle
        public override UniTask Init()
        {
            return base.Init();
        }

        public override void Release()
        {
            _cachePlayerDataMap.Clear();
            base.Release();
        }
        #endregion

        #region Public Methods
        public async UniTask Login(LoginChannel loginChannel, string playerId)
        {
            // ��LoginChannel��PlayerId��ֵ
            LoginChannel = loginChannel;
            PlayerId = playerId;
            GameLog.Info($"LoginService Login, LoginChannel: {LoginChannel}, PlayerId: {PlayerId}");

            // ����LoginService
            await GameEntry.Ins.StartServices(GameServiceLifeSpan.Login);

            // ���͵�¼�¼�
            GameEntryEventsDefine.LoginSuccess.SendEventMessage(LoginChannel, playerId);
        }

        public void Logout()
        {
            // �ر�LoginService
            GameEntry.Ins.StopServices(GameServiceLifeSpan.Login);

            // ���͵ǳ��¼�
            GameEntryEventsDefine.LogoutSuccess.SendEventMessage(LoginChannel, PlayerId);
        }
        #endregion

        #region Private Methods
        #endregion

        #region Event Handlers
        #endregion
    }
}