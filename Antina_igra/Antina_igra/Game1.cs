using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Antina_igra
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public void LelevUp()
        {
            scrolling1.brzina_kretanja += 1;
            scrolling2.brzina_kretanja += 1;
            barijera1.brzina_kretanja += 1;
            barijera2.brzina_kretanja += 1;
            barijera3.brzina_kretanja += 1;
            barijera.brzina_kretanja += 1;
            player1.speed += 1;
            sljedeciLevel += 1;
            maca.brzina_kretanja += 1;
            
        }
        Barijera maca;
        Stit stit;
        LevelUp lvlUp;
        public GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Barijera barijera,barijera2;
        PokretnaBarijera barijera1, barijera3;
        Random r = new Random();
        int mjera;

        int sljedeciLevel;
        Scrolling scrolling1;
        Scrolling scrolling2;
        Player player1;
        KeyboardState keyState;

        SpriteFont score;

        public Game1()
        {
            
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 900;
            graphics.PreferredBackBufferHeight = 540;
            graphics.ApplyChanges();


            Content.RootDirectory = "Content";
        }

      


        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            player1.Initialize();
            player1.playerAnimation.Initialize();
            sljedeciLevel = lvlUp.level + 1;
            mjera = graphics.PreferredBackBufferWidth / 900;

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            score = Content.Load<SpriteFont>("Sprites/spriteFont1");
            // TODO: use this.Content to load your game content here
            scrolling1 = new Scrolling(Content.Load<Texture2D>("Backgrounds/bg1"), new Rectangle(0, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), 3);
            scrolling2 = new Scrolling(Content.Load<Texture2D>("Backgrounds/bg2"), new Rectangle(graphics.PreferredBackBufferWidth, 0, graphics.PreferredBackBufferWidth, graphics.PreferredBackBufferHeight), 3);

            player1 = new Player(Content.Load<Texture2D>("Sprites/tica_gotova"), new Rectangle(0, 0, 50, 57));
            player1.playerAnimation.Image = player1.texture;
            player1.texture_stit = Content.Load<Texture2D>("Sprites/tica_stit");
           // player1.playerAnimation.Image = player1.texture;

            //standardna jedinica
            maca = new Barijera(Content.Load<Texture2D>("Sprites/maca"), new Rectangle(2200, 0,  80,  80));
            stit = new Stit(Content.Load<Texture2D>("Sprites/stit"), new Rectangle(2000, 50, 64, 64));
            lvlUp = new LevelUp(Content.Load<Texture2D>("Sprites/be_ready"), new Rectangle(1000, 250,150, 50));
            barijera = new Barijera(Content.Load<Texture2D>("Sprites/barijera"), new Rectangle(1000, 0, 35, 160));
            barijera1 = new PokretnaBarijera(Content.Load<Texture2D>("Sprites/barijera"), new Rectangle(1250, 100, 35, 150), false, graphics.PreferredBackBufferHeight);
            barijera2 = new Barijera(Content.Load<Texture2D>("Sprites/barijera"), new Rectangle(1500, 400, 35, 160));
            barijera3 = new PokretnaBarijera(Content.Load<Texture2D>("Sprites/barijera"), new Rectangle(1750, 500, 35, 150), true, graphics.PreferredBackBufferHeight);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // Pozadina*********************************************************************
            if (scrolling1.rectangle.X + graphics.PreferredBackBufferWidth <= 0)
                scrolling1.rectangle.X = scrolling2.rectangle.X + graphics.PreferredBackBufferWidth;
            if (scrolling2.rectangle.X + graphics.PreferredBackBufferWidth <= 0)
                scrolling2.rectangle.X = scrolling1.rectangle.X + graphics.PreferredBackBufferWidth;
            scrolling1.Update();
            scrolling2.Update();
            //******************************************************************************

            stit.Update(player1,350,lvlUp);
            player1.Update(gameTime, graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth);

            int t = r.Next(5, 30);
            maca.Update(player1, t, graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth*3);

            //triba uštimat s velicinom sprite-a
            ///////////////////////////////////////
            int interval = (int)(graphics.PreferredBackBufferWidth - graphics.PreferredBackBufferWidth/2);
            barijera.Update(player1, r.Next(0, interval), graphics.PreferredBackBufferHeight,graphics.PreferredBackBufferWidth);
            barijera1.Update(player1, r.Next(0, interval), graphics.PreferredBackBufferHeight,graphics.PreferredBackBufferWidth);
            barijera2.Update(player1, r.Next(0, interval), graphics.PreferredBackBufferHeight, graphics.PreferredBackBufferWidth);
            barijera3.Update(player1, r.Next(0, interval), graphics.PreferredBackBufferHeight,graphics.PreferredBackBufferWidth);
            lvlUp.Update(player1, 300);

            if (sljedeciLevel == lvlUp.level)
                LelevUp();


            

            barijera1.Kretanje();
            barijera3.Kretanje();
            if (player1.stit)
                player1.playerAnimation.Image = player1.texture_stit;
            else
                player1.playerAnimation.Image = player1.texture;
            if(player1.alive==false)
            {
             Initialize();
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            scrolling1.Draw(spriteBatch);
            scrolling2.Draw(spriteBatch);
            lvlUp.Draw(spriteBatch);

            //maca.Draw(spriteBatch);
            stit.Draw(spriteBatch);
            barijera.Draw(spriteBatch);
            barijera1.Draw(spriteBatch);
            barijera2.Draw(spriteBatch);
            barijera3.Draw(spriteBatch);
            spriteBatch.Draw(maca.texture, new Rectangle(maca.rectangle.X, maca.rectangle.Y + 20, 80, 80), Color.White);
           // stit.Draw(spriteBatch);

            player1.playerAnimation.Draw(spriteBatch);
            spriteBatch.DrawString(score, ("Score:" + player1.score.ToString()), new Vector2(0, 0), Color.White);
            if (player1.alive == false)
            {
                spriteBatch.DrawString(score, ("Score:" + player1.score.ToString()), new Vector2(300, 300), Color.White);
                System.Threading.Thread.Sleep(3000);
            }

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
