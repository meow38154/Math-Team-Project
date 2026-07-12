using UnityEngine;

namespace GDH
{
    public enum TrigonometricFunction
    {
        SIN,
        COS,
        TAN
    }

    public sealed class MovementModifier : MonoSingleton<MovementModifier>
    {
        [Header("Cycle")]
        [SerializeField, Min(0.1f)]
        private float cycleDuration = 6f;

        [Header("Movement Modifier")]
        [SerializeField, Min(0f)]
        private float minimumModifier = 0.15f;

        [SerializeField, Min(0f)]
        private float maximumModifier = 1f;

        [SerializeField, Min(0f)]
        private float smoothingTime = 0.08f;

        [Header("Tangent")]
        [SerializeField, Range(1f, 80f)]
        private float tangentLimitDegrees = 70f;

        public NotifyValue<TrigonometricFunction> CurrentState { get; private set; }
            = new NotifyValue<TrigonometricFunction>();

        private const float TwoPi = Mathf.PI * 2f;

        private float _cycleTime;
        private float _currentModifier = 1f;
        private float _smoothingVelocity;

        protected override void Awake()
        {
            base.Awake();
            
            CurrentState.Value = TrigonometricFunction.SIN;
            CurrentState.OnValueChanged += OnMovementMethodChange;

            _currentModifier = CalculateMovementModifier(0f);
        }

        private void Update()
        {
            UpdateCycle();
            UpdateModifier();
        }

        public float GetMovementModifier()
        {
            return _currentModifier;
        }

        private void UpdateCycle()
        {
            _cycleTime += Time.deltaTime;

            if (_cycleTime < cycleDuration)
                return;

            _cycleTime %= cycleDuration;
            ChangeToNextState();
        }

        private void UpdateModifier()
        {
            float normalizedTime = _cycleTime / cycleDuration;
            float targetModifier = CalculateMovementModifier(normalizedTime);

            if (smoothingTime <= 0f)
            {
                _currentModifier = targetModifier;
                return;
            }

            _currentModifier = Mathf.SmoothDamp(
                _currentModifier,
                targetModifier,
                ref _smoothingVelocity,
                smoothingTime,
                Mathf.Infinity,
                Time.deltaTime
            );
        }

        private float CalculateMovementModifier(float normalizedTime)
        {
            float angle = normalizedTime * TwoPi;

            float graphValue = CurrentState.Value switch
            {
                TrigonometricFunction.SIN => Mathf.Sin(angle),
                TrigonometricFunction.COS => Mathf.Cos(angle),
                TrigonometricFunction.TAN => CalculateSafeTangent(angle),
                _ => 0f
            };
            
            float normalizedGraphValue = graphValue * 0.5f + 0.5f;
            
            return Mathf.Lerp(
                minimumModifier,
                maximumModifier,
                normalizedGraphValue
            );
        }

        private float CalculateSafeTangent(float angle)
        {
            float limitRadians =
                tangentLimitDegrees * Mathf.Deg2Rad;

            float boundedAngle =
                Mathf.Sin(angle) * limitRadians;

            return Mathf.Tan(boundedAngle)
                   / Mathf.Tan(limitRadians);
        }

        private void ChangeToNextState()
        {
            CurrentState.Value = CurrentState.Value switch
            {
                TrigonometricFunction.SIN
                    => TrigonometricFunction.COS,

                TrigonometricFunction.COS
                    => TrigonometricFunction.TAN,

                TrigonometricFunction.TAN
                    => TrigonometricFunction.SIN,

                _ => TrigonometricFunction.SIN
            };
        }

        private void OnMovementMethodChange(
            TrigonometricFunction previous,
            TrigonometricFunction next)
        {
            _cycleTime = 0f;
            
            _smoothingVelocity = 0f;

            Debug.Log($"{previous} -> {next}");
        }

        protected override void OnDestroy()
        {
            CurrentState.OnValueChanged -= OnMovementMethodChange;
            base.OnDestroy();
        }
    }
}