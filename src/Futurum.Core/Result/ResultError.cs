using Futurum.Core.Option;

namespace Futurum.Core.Result;

/// <summary>
/// Interface for all Result Errors
/// </summary>
public interface IResultError
{
    /// <summary>
    /// Get Error as <see cref="ResultErrorStructure"/> 
    /// </summary>
    ResultErrorStructure GetErrorStructure();
}

/// <summary>
/// A <see cref="IResultError"/> that is a leaf node <see cref="IResultError"/>
/// </summary>
public interface IResultErrorNonComposite : IResultError
{
    /// <summary>
    /// Get Error as <see cref="string"/> 
    /// </summary>
    string GetErrorString();
}

/// <summary>
/// A <see cref="IResultError"/> with an optional parent <see cref="IResultError"/> and children <see cref="IResultError"/>
/// </summary>
public interface IResultErrorComposite : IResultError
{
    /// <summary>
    /// Get Error as <see cref="string"/> 
    /// </summary>
    string GetErrorString(string seperator);
    
    /// <summary>
    /// Parent <see cref="IResultError"/>
    /// </summary>
    Option<IResultErrorNonComposite> Parent { get; }
    
    /// <summary>
    /// Children <see cref="IResultError"/>
    /// </summary>
    IEnumerable<IResultError> Children { get; }
}