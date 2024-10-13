using UnityEngine;

namespace RoR
{
    public class FpsChanger : MonoBehaviour
    {
        [Header("В контекстном меню надо выбрать метод смены фпс")]

        [Range(24, 1000)]
        [SerializeField] private int _fps = 165;
        [Range(0, 1)]
        [SerializeField] private int _vsync = 0;

        [ContextMenu("ChangeFps")]
        public void ChangeFps() => Application.targetFrameRate = _fps;

        [ContextMenu("ChangeVsync")]
        public void ChangeVsync() => QualitySettings.vSyncCount = _vsync;    
    }
}