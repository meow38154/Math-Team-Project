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
            CurrentState.Value = TrigonometricFunction.SIN;
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
                TrigonometricFunction.SIN => Mathf.Sin(_elapsedTime),
                TrigonometricFunction.COS => Mathf.Cos(_elapsedTime),
                TrigonometricFunction.TAN => Mathf.Tan(_elapsedTime),
                _ => 1
            };
            return value;
        }
        private IEnumerator ElapsedTimeModifyCoroutine()
        {
            while(_elapsedTime <= 90)
            {
                yield return new WaitForSeconds(0.3f);
                _elapsedTime++;
                Debug.Log("TimeElapse");
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
