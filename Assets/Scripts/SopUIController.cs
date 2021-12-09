using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class SopUIController : MonoBehaviour
{
    public List<Sprite> sopImages;
    public Image tipsImage;
    public Sprite correct;

    public class EventListener<T>
    {
        public delegate void OnValueChangeDelegate(T newVal);
        public event OnValueChangeDelegate OnVariableChange;
        private T m_value;
        public T Value
        {
            get
            {
                return m_value;
            }
            set
            {
                if (m_value.Equals(value)) return;
                OnVariableChange?.Invoke(value);
                m_value = value;
            }
        }
    }

    private EventListener<int> listener;
    private void Start()
    {
        listener = new EventListener<int>();
        listener.OnVariableChange += ChangeImage;
    }
    private void Update()
    {
        listener.Value = EngineController.step;
    }

    private void ChangeImage(int value)
    {
        if (EngineController.step < sopImages.Count)
        {
            tipsImage.sprite = sopImages[EngineController.step];
        }
        else if (EngineController.step == sopImages.Count)
        {
            tipsImage.sprite = correct;
        }
    }
}
