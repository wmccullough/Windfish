using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Windfish.Core
{
    public abstract class Component
    {

        public GameObject _gameObject;
        public void Initialize(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public abstract void Update(GameTime gameTime);

        public abstract void Draw(SpriteBatch spriteBatch);

        public void RemoveFromParent()
        {
            _gameObject.RemoveComponent(this);
        }

        public T GetComponentFromParent<T>() where T : Component
        {
            return _gameObject.GetComponent<T>();
        }

        public int GetParentId()
        {
            return _gameObject.Id;
        }
    }
}