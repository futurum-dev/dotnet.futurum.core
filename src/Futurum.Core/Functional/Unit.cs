using System.Diagnostics.CodeAnalysis;

namespace Futurum.Core.Functional;

[ExcludeFromCodeCoverage]
public struct Unit
{
    public static readonly Unit Value = new();
}