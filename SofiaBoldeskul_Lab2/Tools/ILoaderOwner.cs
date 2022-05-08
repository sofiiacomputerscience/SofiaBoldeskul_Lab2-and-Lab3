using System.ComponentModel;
using System.Windows;

namespace SofiaBoldeskul_Lab2.Tools
{
    internal interface ILoaderOwner : INotifyPropertyChanged
    {
        Visibility LoaderVisibility { get; set; }
        bool IsControlEnabled { get; set; }
    }
}