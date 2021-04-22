using System.Collections;
using UnityEngine;

namespace PurpleCable
{
    public abstract class SimpleAnimation : MonoBehaviour
    {
        [SerializeField] public float Duration = 0.3f;

        [SerializeField] protected float Delay = 0f;

        public float TotalDuration => Delay + Duration;

        public bool IsAnimating { get; private set; } = false;

        public bool IsDoneAnimating { get; private set; } = false;

        private float _timer = 0f;

        private bool _isUpdateDone = false;

        private bool _originalActiveSelf = false;

        private void Start()
        {
            _originalActiveSelf = gameObject.activeSelf;
        }

        protected void FixedUpdate()
        {
            if (!IsAnimating || IsDoneAnimating)
                return;

            if (_isUpdateDone || _timer >= TotalDuration * 5)
            {
                SetEndValue();

                EndAnimation();

                return;
            }

            _timer += Time.deltaTime;

            if (_timer >= Delay)
            {
                if (!_isUpdateDone)
                {
                    if (MustUpdate())
                        UpdateValue(_timer / Duration);
                    else
                        _isUpdateDone = true;
                }
            }
        }

        public void StartAnimation()
        {
            _timer = 0f;
            _isUpdateDone = false;
            IsAnimating = true;
            IsDoneAnimating = false;
            gameObject.SetActive(true);
            this.enabled = true;

            StartAnimationExtended();
        }

        protected virtual void StartAnimationExtended()
        { }


        public void EndAnimation()
        {
            IsAnimating = false;
            IsDoneAnimating = true;

            this.enabled = false;

            EndAnimationExtended();
        }

        protected virtual void EndAnimationExtended()
        { }

        public virtual void ResetAnimation()
        {
            _timer = 0f;
            _isUpdateDone = false;
            IsAnimating = false;
            IsDoneAnimating = false;
            gameObject.SetActive(_originalActiveSelf);

            this.enabled = true;
        }

        protected abstract void SetEndValue();

        protected abstract bool MustUpdate();

        protected abstract void UpdateValue(float t);
    }
}
