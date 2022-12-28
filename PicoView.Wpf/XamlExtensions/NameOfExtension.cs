// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Windows.Markup;
using System.Windows;
using System.Windows.Data;

// ReSharper disable once CheckNamespace
namespace PicoView.Wpf;

public class NameOfExtension : MarkupExtension
{
    public NameOfExtension(Binding binding)
    {
        _propertyPath = binding.Path;
    }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
        var indexOfLastVariableName = _propertyPath.Path.LastIndexOf('.');
        return _propertyPath.Path[(indexOfLastVariableName + 1)..];
    }

    private readonly PropertyPath _propertyPath;
}