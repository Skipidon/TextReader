using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace WpfRichText.Ex.State
{
    public abstract class State : IState
    {
        Color _stateColor;

        public Color StateColor
        {
            get { return _stateColor; }
            set { _stateColor = value; }
        }

    }
}
