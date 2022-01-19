namespace Futurum.Core.Option;

public static partial class OptionExtensions
{
    internal static T GetValue<T>(Option<T> option) =>
        option.Value;
}