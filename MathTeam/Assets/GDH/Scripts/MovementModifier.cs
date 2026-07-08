using System.Collections;
using UnityEngine;
namespace GDH
{
    public enum TrigonometricFunction
    {
        SIN,
        COS,
        TAN
    }
    public class MovementModifier : MonoSingleton<MovementModifier>
    {
        public NotifyValue<TrigonometricFunction> CurrentState { get; private set; } = new NotifyValue<TrigonometricFunction>();
        private float _elapsedTime = 1;
        protected override void Awake()
        {
            base.Awake();
            CurrentState.OnValueChanged += OnMovementMethodChange;
            CurrentState.Value = TrigonometricFunction.TAN;
            StartCoroutine(ElapsedTimeModifyCoroutine());
        }
        public void OnMovementMethodChange(TrigonometricFunction prev, TrigonometricFunction next)
        {
            StopAllCoroutines();
            _elapsedTime = 1;
            StartCoroutine(ElapsedTimeModifyCoroutine());
            Debug.Log($"{prev}->{next}");
        }
        public float GetMovementModifier()
        {
            return CalculateMovementModifier();
        }

        private float CalculateMovementModifier()
        {
            float value = CurrentState.Value switch
            {
                TrigonometricFunction.SIN => Mathf.Sin(Mathf.Deg2Rad * _elapsedTime),
                TrigonometricFunction.COS => Mathf.Cos(Mathf.Deg2Rad * _elapsedTime),
                TrigonometricFunction.TAN => Mathf.Tan(Mathf.Deg2Rad * _elapsedTime * 0.2f),
                _ => 1
            };
            return value;
        }
        private IEnumerator ElapsedTimeModifyCoroutine()
        {
            while(_elapsedTime <= 360)
            {
                yield return new WaitForSeconds(0.075f);
                _elapsedTime++;
            }
            OnElapsedTimeFull();
        }
        private void OnElapsedTimeFull()
        {
            CurrentState.Value = CurrentState.Value switch
            {
                TrigonometricFunction.SIN => TrigonometricFunction.COS,
                TrigonometricFunction.COS => TrigonometricFunction.TAN,
                TrigonometricFunction.TAN => TrigonometricFunction.SIN,
                _ => TrigonometricFunction.SIN
            };
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}
