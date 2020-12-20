﻿using UnityEngine;
using System;

namespace RTEditor
{
    [Flags]
    public enum SphereTreeNodeFlags
    {
        None = 0x0,
        Root = 0x1,
        SuperSphere = 0x2,
        Terminal = 0x4,
        MustRecompute = 0x8,
        MustIntegrate = 0x10
    }
}