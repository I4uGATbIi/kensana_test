using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Client
{
    public class WindowView : MonoBehaviour
    {
        [SerializeField] private WindowView _childWindow;

        private bool _isOpen;

        public UnityEvent<bool> OnViewChange;
        public bool IsOpen => _isOpen;

        public virtual void Show()
        {
            gameObject.SetActive(true);
            _isOpen = true;
            OnViewChange?.Invoke(_isOpen);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
            _isOpen = false;
            HideChild();
            OnViewChange?.Invoke(_isOpen);
        }

        public virtual void ShowChild()
        {
            if (_childWindow != null)
                _childWindow.Show();
        }

        public virtual void HideChild()
        {
            if (_childWindow != null)
                _childWindow.Hide();
        }
    }
}