using TMPro;
using UnityEngine;

namespace CoopHorror.CodeBase
{
    public class NickNameGenerator : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _nickName;
        
        private void Awake()
        {
            _nickName.text = LocalPlayerData.NickName;
        }
    }
}
