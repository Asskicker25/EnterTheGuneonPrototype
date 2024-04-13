using UnityEngine;

namespace Scripts.UI
{
    [CreateAssetMenu(menuName =  "Configs/UI/UIConfig", fileName = "UIConfig")]
    public class UIConfig : ScriptableObject
    {
        public UICanvasView mCanvasView;
    }
}
