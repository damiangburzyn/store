﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace System
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum value)
        {
            return
                value
                    .GetType()
                    .GetMember(value.ToString())
                    .FirstOrDefault()
                    ?.GetCustomAttribute<DescriptionAttribute>()
                    ?.Description;
        }
    }
}