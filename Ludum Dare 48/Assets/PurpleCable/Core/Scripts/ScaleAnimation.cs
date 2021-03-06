using UnityEngine;

namespace PurpleCable
{
    public class ScaleAnimation : SimpleAnimation
    {
        public Vector3 EndLocalScale = Vector3.one;

        private Vector3 _originalLocalScale = Vector3.one;

        private Vector3 _scaleVelocity = Vector3.zero;

        private void Awake()
        {
            _originalLocalScale = transform.localScale;
        }

        protected override void SetEndValue()
        {
            transform.localScale = EndLocalScale;
        }

        protected override bool MustUpdate()
        {
            return Vector3.Distance(transform.localScale, EndLocalScale) > 0.01f;
        }

        protected override void UpdateValue(float t)
        {
            transform.localScale = Vector3.SmoothDamp(transform.localScale, EndLocalScale, ref _scaleVelocity, Duration);
        }

        public override void ResetAnimation()
        {
            base.ResetAnimation();

            transform.localScale = _originalLocalScale;
        }
    }
}
