using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace zombiefactory
{
    class BaseResource<T> : IEquatable<BaseResource<T>>
    {
        public string Name { get; protected set; }
        public string Path { get; protected set; }
        public T ResourceData { get; protected set; }
        ContentManager Content { get; set; }

        public BaseResource(ContentManager content, string path)
        {
            Path = path;
            Name = ExtractName(Path);
            Content = content;
            ResourceData = default(T);
        }

        public void Load()
        {
            if (ResourceData == null)
            {
                ResourceData = Content.Load<T>(Path);
            }
        }

        private static string ExtractName(string path)
        {
            int start = Math.Max(path.LastIndexOf('/'), path.LastIndexOf('\\')) + 1;
            return path.Substring(start).ToLower();
        }

        public bool Equals(BaseResource<T> other)
        {
            return Name == other.Name;
        }
    }
}
