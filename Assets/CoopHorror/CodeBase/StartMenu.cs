using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CoopHorror.CodeBase
{
    public class StartMenu : MonoBehaviour
    {
        [SerializeField] private NetworkRunner _networkRunnerPrefab;
        [SerializeField] private TMP_InputField _nickName;
        [SerializeField] private TextMeshProUGUI _nickNamePlaceholder;
        [SerializeField] private TMP_InputField _roomName;
        [SerializeField] private Button _startGameButton;
        [SerializeField] private string _gameScenePath;
        [SerializeField] private int _maxPlayers = 4;

        private NetworkRunner _runnerInstance;

        private void Awake()
        {
            _startGameButton.onClick.AddListener(StartGame);
        }

        private async void StartGame()
        {
            SetPlayerData();
            
            _runnerInstance = FindObjectOfType<NetworkRunner>();

            if (_runnerInstance == null)
            {
                _runnerInstance = Instantiate(_networkRunnerPrefab);
            }
            
            _runnerInstance.ProvideInput = true;
            
            StartGameArgs startGameArgs = new StartGameArgs()
            {
                GameMode = GameMode.Shared,
                SessionName = _roomName.text,
                PlayerCount = _maxPlayers,
                Scene = SceneRef.FromIndex(SceneUtility.GetBuildIndexByScenePath(_gameScenePath))
            };
            
            await _runnerInstance.StartGame(startGameArgs);
            
            if (_runnerInstance.IsServer)
            {
                _runnerInstance.LoadScene(_gameScenePath);
            }
        }
        
        private void SetPlayerData()
        {
            if (string.IsNullOrWhiteSpace(_nickName.text))
                LocalPlayerData.NickName = _nickNamePlaceholder.text;
            else
                LocalPlayerData.NickName = _nickName.text;
        }
    }
}
