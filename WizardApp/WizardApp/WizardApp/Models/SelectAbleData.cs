﻿using System;
namespace WizardApp.Models
{
    public class SelectableData<T>
    {
        public T Data { get; set; }
        public bool Selected { get; set; }
    }
}

