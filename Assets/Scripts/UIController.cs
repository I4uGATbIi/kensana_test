using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Client
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private List<WindowView> _buttonedWindows;
        [SerializeField] private WindowView _timedWindow;
        [SerializeField] private Vector2Int _minMaxRandomTime;

        public UnityEvent OnTimerFinished;

        private bool _isTimerFinished;

        private void OnEnable()
        {
            OnTimerFinished.AddListener(OpenTimedWindow);
            _buttonedWindows.ForEach(window => window.OnViewChange.AddListener(_ => OpenTimedWindow()));
        }

        private void OnDestroy()
        {
            OnTimerFinished.RemoveAllListeners();
            _buttonedWindows.ForEach(window => window.OnViewChange.RemoveListener(_ => OpenTimedWindow()));
        }

        private void Start()
        {
            int timeDelay = Random.Range(_minMaxRandomTime.x, _minMaxRandomTime.y + 1);
            StartCoroutine(Timer(timeDelay));
        }

        private IEnumerator Timer(float time)
        {
            _isTimerFinished = false;
            yield return new WaitForSeconds(time);
            _isTimerFinished = true;
            OnTimerFinished?.Invoke();
        }

        private void OpenTimedWindow()
        {
            if (!_isTimerFinished)
                return;
            if (!_buttonedWindows.Any(window => window.IsOpen))
                _timedWindow.Show();
        }
    }
}