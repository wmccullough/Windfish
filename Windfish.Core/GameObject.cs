using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Windfish.Core
{
    public class GameObject
    {
        public GameObject()
        {
            _components = new List<Component>();
        }

        public int Id { get; set; }
        private List<Component> _components;

        public void AddComponent(Component component)
        {
            _components.Add(component);
            component.Initialize(this);
        }
        public void AddComponentRange(List<Component> components)
        {
            _components.AddRange(components);
            foreach (Component component in components) {
                component.Initialize(this);
            }
        }
        public void RemoveComponent(Component component)
        {
            _components.Remove(component);
        }

        public T GetComponent<T>() where T : Component
        {
            return (T)_components.FirstOrDefault(o => o is T);
        }

        public void Update(GameTime gameTime)
        {
            foreach (Component component in _components) {
                component.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Component component in _components) {
                component.Draw(spriteBatch);
            }

        }
        
    }
}
