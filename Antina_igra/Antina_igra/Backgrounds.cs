
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Antina_igra
{
    class Sprite
    {
        public Random r = new Random();
        public int brzina_kretanja;
        public Texture2D texture;
        public Rectangle rectangle;
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);
        }
        
    }

    class Scrolling : Sprite
    {
        public Scrolling(Texture2D newTexture, Rectangle newRectangle,int brzina)
        {
            texture = newTexture;
            rectangle = newRectangle;
            brzina_kretanja = brzina;
        }

        public void Update()
        {
            rectangle.X -= brzina_kretanja;
        }
        
    }

     class Player : Sprite
    {
        public Animation playerAnimation=new Animation();
        KeyboardState keyState;

        public Texture2D texture_stit;
        public float speed;
        public Vector2 Velocity;
        public bool horizontal;
        public bool vertical;
        public int score;
        public bool alive;
        public bool stit;


        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public void Initialize()
        {
        }
        
        public void Update(GameTime gameTime,int visina_ekrana, int duljina)
        {
            
            playerAnimation.active = true;

            //kretanje sa ubrzanim gibanjem *******************************************
            keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.Right))
            {
                if (Velocity.X < 0)
                   Velocity.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds * 0.3f;
                Velocity.X += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                horizontal = true;
            }
            if (keyState.IsKeyDown(Keys.Left))
            {
                if (Velocity.X > 0)
                   Velocity.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds * 0.3f;
                Velocity.X -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                horizontal = true;
            }
            if (keyState.IsKeyDown(Keys.Up))
            {
                if (Velocity.Y > 0)
                   Velocity.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds * 0.3f;
                Velocity.Y -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                vertical = true;
            }
            if (keyState.IsKeyDown(Keys.Down))
            {
                if (Velocity.Y < 0)
                    Velocity.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds * 0.3f;
                Velocity.Y += speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
                vertical = true;
            }
            //***************************************************
            //u slučaju da bizi s ekrana gubi svo ubrzanje
            if ((rectangle.X < -5 && Velocity.X < 0) || rectangle.X>duljina-rectangle.Width && Velocity.X>0)
                Velocity.X = 0;
                

            vertical = false;
            horizontal = false;
            
            rectangle.X += (int)(Velocity.X * 1.9f);
            rectangle.Y += (int)(Velocity.Y*1.9f);
            //kretanje zavrsava ovdje ***************************************************

            if (alive)
                score += 1;

            //uvjet za odbijanje, svi faktori su empiristički uštimani*******************
            if (rectangle.Y > visina_ekrana - visina_ekrana/4.3f)
            {
                rectangle.Y -=(int) (Velocity.Y * 2.5f);
                Velocity.Y = -Velocity.Y * 0.65f;
            }
            if (rectangle.Y < -rectangle.Height/2)
            {
                rectangle.Y -= (int)(Velocity.Y * 2.5f);
                Velocity.Y = -Velocity.Y * 0.65f;
            }
            //***************************************************************************


                

            playerAnimation.Position=new Vector2(rectangle.Location.X,rectangle.Location.Y);
            playerAnimation.Update(gameTime);
        }


        public Player(Texture2D tex,Rectangle rect)
        {
            speed = 6;
            texture = tex;
            rectangle = rect;
            alive = true;
            score = 0;
            stit = false;
        }

    }
}
