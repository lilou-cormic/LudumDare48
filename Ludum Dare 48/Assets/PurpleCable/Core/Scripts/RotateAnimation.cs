using UnityEngine;

namespace PurpleCable
{
    public class RotateAnimation : SimpleAnimation
    {
        [SerializeField] Vector3 EndLocalRotation = Vector3.zero;

        [SerializeField] bool UnParent = false;

        private Quaternion _originalLocalRotation = Quaternion.identity;

        private Quaternion _deriv = Quaternion.identity;

        private void Awake()
        {
            _originalLocalRotation = transform.localRotation;
        }

        protected override void StartAnimationExtended()
        {
            if (UnParent)
                transform.parent = transform.parent.parent;
        }

        protected override void SetEndValue()
        {
            transform.localRotation = Quaternion.Euler(EndLocalRotation);
        }

        protected override bool MustUpdate()
        {
            return Vector3.Distance(transform.localRotation.eulerAngles, EndLocalRotation) > 0.01f;
        }

        protected override void UpdateValue(float t)
        {
            transform.localRotation = RotationExtensions.SmoothDamp(transform.localRotation, Quaternion.Euler(EndLocalRotation), ref _deriv, Duration);
        }

        public override void ResetAnimation()
        {
            base.ResetAnimation();

            transform.localRotation = _originalLocalRotation;
        }
    }
}
