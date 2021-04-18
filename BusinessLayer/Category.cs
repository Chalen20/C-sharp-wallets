using System;
using System.Drawing;

namespace BusinessLayer
{
    public class Category
    {
        /*public static int InstanceCount { get; private set; }*/

        private Guid _guid;
        private string _name;
        private Color _color;
        private string _icon;

        public Guid Guid
        {
            get { return _guid; }
            private set { _guid = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public string Icon
        {
            get { return _icon; }
            set { _icon = value; }
        }

        public Category()
        {
           /* InstanceCount++;*/
            Guid = Guid.NewGuid();
        }

        /*public Category(int id): this()
        {
            _id = id;
        }*/

        public bool Validate()
        {
            var result = true;
            if (String.IsNullOrWhiteSpace(Name))
            {
                result = false;
            }
            if(Color.IsEmpty)
            {
                result = false;
            }
            return result;
        }
    }
}
