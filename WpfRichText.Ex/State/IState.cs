using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace WpfRichText.Ex.State
{
    public interface IState
    {
        Color StateColor { get; set; }
    }
}
