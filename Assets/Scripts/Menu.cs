using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace OZITK
{
    [RequireComponent(typeof(Canvas))]
    public class Menu : MonoBehaviour
    {
        [SerializeField] protected InputActionReference input;

        private Canvas canvas;

        private void Awake()
        {
            canvas = GetComponent<Canvas>();
            input.action.performed += ToggleCanvas;
        }

        private void OnDestroy()
        {
            input.action.performed -= ToggleCanvas;
        }

        private void ToggleCanvas(InputAction.CallbackContext obj)
        {
            canvas.enabled = !canvas.enabled;
            SetCursor(canvas.enabled);
        }

        private void SetCursor(bool state)
        {
            Cursor.lockState = state ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = state;
        }

        public void Restart()
        {
            SceneManager.LoadScene(0);
        }
        public void Exit()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}
