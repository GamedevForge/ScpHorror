using Random = UnityEngine.Random;

namespace CoopHorror.CodeBase
{
    public static class LocalPlayerData
    {
        private static string _nickName;
        public static string NickName
        {
            set => _nickName = value;
            get
            {
                if (string.IsNullOrWhiteSpace(_nickName))
                {
                    int randomNumber = Random.Range(0, 9999);
                    _nickName = $"Player {randomNumber.ToString("0000")}";
                }
                return _nickName;
            }
        }
    }
}
