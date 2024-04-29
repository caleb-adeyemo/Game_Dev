using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasProgress
{
    event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float ProgressNormalized;
        public Color BarColor;
        public bool BarSprite;

        public OnProgressChangedEventArgs(float progressNormalized, Color barColor, bool barSprite)
        {
            ProgressNormalized = progressNormalized;
            BarColor = barColor;
            BarSprite = barSprite;
        }
    }
}

