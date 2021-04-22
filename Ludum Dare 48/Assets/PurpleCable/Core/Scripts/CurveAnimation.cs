using System.Linq;
using UnityEngine;

namespace PurpleCable
{
    public class CurveAnimation : SimpleAnimation
    {
        [SerializeField] protected Vector3[] LocalPositions = null;

        [SerializeField] LineRenderer LineRenderer = null;

        protected Vector3 _startLocalPosition = Vector3.zero;

        private int _index = 0;
        private Vector3 _nextLocalPosition = Vector3.zero;

        private Vector3 _positionVelocity = Vector3.zero;

        private float _tLerp = 0f;

        private void Awake()
        {
            if (LocalPositions == null || LocalPositions.Length == 0)
            {
                Destroy(gameObject);
                return;
            }

            _startLocalPosition = transform.localPosition;

            _nextLocalPosition = LocalPositions[0];
        }

        private void OnValidate()
        {
            if (LineRenderer != null)
            {
                LocalPositions = new Vector3[LineRenderer.positionCount];

                for (int i = 0; i < LocalPositions.Length; i++)
                {
                    LocalPositions[i] = LineRenderer.GetPosition(i) - LineRenderer.transform.position;
                }
            }
        }

        protected override void SetEndValue()
        {
            transform.localPosition = LocalPositions.Last();
        }

        protected override bool MustUpdate()
        {
            if (_index >= LocalPositions.Length || _nextLocalPosition == null)
                return false;

            return Vector3.Distance(transform.localPosition, _nextLocalPosition) > 0.01f;
        }

        protected override void UpdateValue(float t)
        {
            if (LineRenderer == null)
                transform.localPosition = Vector3.SmoothDamp(transform.localPosition, _nextLocalPosition, ref _positionVelocity, Duration / LocalPositions.Length);
            else
                transform.localPosition = Vector3.Lerp(transform.localPosition, _nextLocalPosition, _tLerp / (Duration / LocalPositions.Length));

            if (Vector3.Distance(transform.localPosition, _nextLocalPosition) <= 0.01f)
            {
                _index++;
                _tLerp = 0;

                _nextLocalPosition = LocalPositions.ElementAtOrDefault(_index);

                return;
            }

            _tLerp += Time.deltaTime;
        }

        private void OnDrawGizmos()
        {
            if (LocalPositions == null || LocalPositions.Length == 0)
                return;


            Vector3 GetPosition(int index)
            {
                return transform.position + (LocalPositions[index] - transform.localPosition);
            }

            Gizmos.DrawLine(transform.position, GetPosition(0));

            for (int i = 0; i < LocalPositions.Length - 1; i++)
            {
                Gizmos.DrawLine(GetPosition(i), GetPosition(i + 1));
            }
        }

        public override void ResetAnimation()
        {
            base.ResetAnimation();

            transform.localPosition = _startLocalPosition;

            _index = 0;
            _positionVelocity = Vector3.zero;
            _tLerp = 0;

            _nextLocalPosition = LocalPositions[0];
        }
    }
}
