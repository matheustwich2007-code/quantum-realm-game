using UnityEngine;

namespace QuantumRealm.Player
{
    public class MobileTouchController : MonoBehaviour
    {
        public Vector2 MoveInput { get; private set; }
        public bool FirePressed { get; private set; }
        public bool JumpPressed { get; private set; }
        public bool DashPressed { get; private set; }

        private Vector2 leftTouchStart;
        private Vector2 leftTouchCurrent;
        private bool hasLeftTouch;

        private void Update()
        {
            if (!Application.isMobilePlatform)
            {
                return;
            }

            HandleTouchInput();
        }

        private void HandleTouchInput()
        {
            if (Input.touchCount <= 0)
            {
                MoveInput = Vector2.zero;
                FirePressed = false;
                JumpPressed = false;
                DashPressed = false;
                return;
            }

            for (int i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);

                if (touch.position.x < Screen.width * 0.5f)
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        hasLeftTouch = true;
                        leftTouchStart = touch.position;
                    }

                    if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                    {
                        leftTouchCurrent = touch.position;
                    }

                    if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        hasLeftTouch = false;
                        leftTouchCurrent = Vector2.zero;
                    }

                    Vector2 delta = leftTouchCurrent - leftTouchStart;
                    MoveInput = new Vector2(Mathf.Clamp(delta.x / 150f, -1f, 1f), Mathf.Clamp(delta.y / 150f, -1f, 1f));
                }
                else
                {
                    if (touch.phase == TouchPhase.Began)
                    {
                        FirePressed = true;
                    }

                    if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                    {
                        FirePressed = false;
                    }

                    if (touch.tapCount == 2)
                    {
                        DashPressed = true;
                    }
                }
            }

            if (Input.touchCount == 1)
            {
                Touch t = Input.GetTouch(0);
                if (t.position.x > Screen.width * 0.7f && t.phase == TouchPhase.Began)
                {
                    JumpPressed = true;
                }
            }
        }
    }
}
