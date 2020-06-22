﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Shared.ClassExtensions
{ }
public static class EnumExtension
{
    public static string EnumDisplayName(this Enum enumValue)
    {
        return enumValue.GetType().GetMember(enumValue.ToString())
                       .First()
                       .GetCustomAttribute<DisplayAttribute>()
                       .Name;
    }
}
