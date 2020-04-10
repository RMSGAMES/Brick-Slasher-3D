using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI projectileCount;

    public void UpdateProjectileCount(int t_count)
    {
        projectileCount.text = "x " + t_count;
    }
}