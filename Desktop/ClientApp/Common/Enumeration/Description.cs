using System;

namespace ClientApp
{
    class Description : Attribute
    {
        public string Text;
        public Description(string text)
        {
            Text = text;
        }
    }
}
