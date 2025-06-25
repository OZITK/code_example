using TMPro;
using UnityEngine;

namespace OZITK
{
    public class DebugWindow : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textPrefab;
        [SerializeField] private Transform content;

        public TextMeshProUGUI CreateDebugText()
        {
            return Instantiate(textPrefab, content);
        }
    }
}
