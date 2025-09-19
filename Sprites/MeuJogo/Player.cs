using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MeuJogo
{
    public class Player
    {
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; }
        public float Speed { get; set; }

        public Player(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
            Speed = 200f; // Velocidade em pixels por segundo
        }

        public void Update(GameTime gameTime)
        {
            // Lógica de atualização do jogador será adicionada aqui
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}
