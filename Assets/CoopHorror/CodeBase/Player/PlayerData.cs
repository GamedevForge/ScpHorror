using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Game/PlayerData")]
public class PlayerData : ScriptableObject
{
    [field: SerializeField] public int Coins { get; private set; }

    public void AddCoins(int coins)
    {
        if (coins > 0)
        {
            Coins += coins;
        }
    }
}
