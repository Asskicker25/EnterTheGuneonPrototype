using UnityEngine;
using UnityEngine.EventSystems;

namespace RedLabsGames.Utls.GamepadDevice
{
    public class TouchPad : OnScreenJoystick, IDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        public Camera CurrentEventCamera { get; set; }

        public bool PreserveInertia = true;
        public float Friction = 3f;

        private int _lastDragFrameNumber;
        private bool _isCurrentlyTweaking;

        [Tooltip("Constraints on the joystick movement axis")]
        public ControlMovementDirection ControlMoveAxis = ControlMovementDirection.Both;

        
        void Awake()
        {
            SendValue(Vector2.zero);
        }

        /// <summary>
        /// Send value to the gamepad when touch input is dragged
        /// </summary>
        /// <param name="eventData"></param>
        public virtual void OnDrag(PointerEventData eventData)
        {

            if ((ControlMoveAxis & ControlMovementDirection.Horizontal) != 0)
            {
                SendValue(new Vector2(eventData.delta.x, Value.y));
            }
            if ((ControlMoveAxis & ControlMovementDirection.Vertical) != 0)
            {
                SendValue(new Vector2(Value.x, eventData.delta.y));
            }

            _lastDragFrameNumber = Time.renderedFrameCount;
        }
        
        /// <summary>
        /// Setting the axis value to zero when pointer is up
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerUp(PointerEventData eventData)
        {
            _isCurrentlyTweaking = false;
            if (!PreserveInertia)
            {
                SendValue(Vector2.zero);
            }
        }

        /// <summary>
        /// Sending pointer data to OnDrag and enabling boolean to check input continously
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            _isCurrentlyTweaking = true;
            OnDrag(eventData);
        }

       
        private void Update()
        {
            if (_isCurrentlyTweaking && _lastDragFrameNumber < Time.renderedFrameCount - 2)
            {
                SendValue(Vector2.zero);
            }

            if (PreserveInertia && !_isCurrentlyTweaking)
            {

                Value.x = Mathf.Lerp(Value.x, 0f, Friction * Time.deltaTime);
                Value.y = Mathf.Lerp(Value.y, 0f, Friction * Time.deltaTime);

                SendValue(new Vector2(Value.x, Value.y));
            }
        }
    }
}
