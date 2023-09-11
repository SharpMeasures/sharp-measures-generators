﻿using System;
using System.Diagnostics.CodeAnalysis;

[AttributeUsage(AttributeTargets.Class)]
[SuppressMessage("Design", "CA1050: Declare types in namespaces")]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed")]
[SuppressMessage("Major Bug", "S3903: Types should be defined in named namespaces")]
public sealed class GenericAttribute<T> : Attribute { }

[AttributeUsage(AttributeTargets.Class)]
[SuppressMessage("Design", "CA1050: Declare types in namespaces")]
[SuppressMessage("Major Code Smell", "S2326: Unused type parameters should be removed")]
[SuppressMessage("Major Bug", "S3903: Types should be defined in named namespaces")]
public sealed class GenericAttribute<T1, T2> : Attribute { }
