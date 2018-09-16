using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.IO;

public class GameScreen : BaseGame
{
    Player player;
    Enemy enemy;
    Map map;
    Camera camera;
    List<Bullet> bullets = new List<Bullet>();
    float timeSinceScore;
    public SpriteFont font;
    public Texture2D door;


    // The AnimatedSpriteStrip class is not part of XNA but has been written by Simon Schofield
    // to help with the animation of animated sprites and can be found in the 
    // solution explorer
    //private AnimatedSpriteStripManager SpriteManager = new AnimatedSpriteStripManager(10);

    public GameScreen()
    {

    }

    /// <summary>
    /// Allows the game to perform any initialization it needs to before starting to run.
    /// This is where it can query for any required services and load any non-graphic
    /// related content.  Calling base.Initialize will enumerate through any components
    /// and initialize them as well.
    /// </summary>
    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        map = new Map();
        player = new Player();
        enemy = new Enemy();


        base.Initialize();
    }

    /// <summary>
    /// LoadContent will be called once per game and is the place to load
    /// all of your content.
    /// </summary>
    protected override void LoadContent()
    {
        // Create a new SpriteBatch, which can be used to draw textures.
        spriteBatch = new SpriteBatch(GraphicsDevice);
        camera = new Camera(GraphicsDevice.Viewport);
        Song theme = Content.Load<Song>("audio/theme");
        MediaPlayer.Play(theme);
        MediaPlayer.IsRepeating = true;
        door = Content.Load<Texture2D>("objects/door");



        player.spriteManager = LoadSprites();
        enemy.Load(Content);
        font = Content.Load<SpriteFont>("fonts/arial");
        player.rectangle_texture = Content.Load<Texture2D>("test");
        Tile.Content = Content;

        StreamReader reader = new StreamReader("map.txt");
        string[,] mapint = new string[10000, 16];
        string[] line = File.ReadAllLines("map.txt");
        File.ReadLines("map.txt");

        Random random = new Random();

        for (int row = 0; row < 9999; row++)
        {

            int randrow = random.Next(20);
            for (int columns = 0; columns < 16; columns++)
            {
                if (row == 0)
                {
                    if (columns % 2 == 0) { mapint[row, columns] = "1"; }

                    else if (columns % 2 == 1) { mapint[row, columns] = "2"; }
                }

                else if (row == 1 || row == 2 || row == 3 || row == 4)
                {
                    mapint[row, columns] = "0";

                }

                else
                {

                    if (row % 4 != 0)
                    {
                        if (row % 4 == 3)
                        {
                            int trapspawn = random.Next(10);
                            if (trapspawn == 4 || trapspawn == 5)
                            {
                                string trap = trapspawn.ToString();
                                mapint[row, columns] = trap;
                            }
                            else { mapint[row, columns] = "0"; }

                        }
                        else
                        {
                            mapint[row, columns] = "0";
                        }
                    }
                    else
                    {

                        mapint[row, columns] = line[randrow].Substring(columns, 1);
                    }
                }


            }
        }






        mapint[4, 6] = "3"; mapint[4, 7] = "3"; mapint[4, 8] = "3"; mapint[4, 9] = "3";

        for (int i = 0; i < mapint.GetLength(0); i++)
        {
            for (int j = 0; j < mapint.GetLength(1); j++)
            {
                if (mapint[i, j] == "4")
                {
                    if (mapint[i + 1, j] != "3")
                    {
                        mapint[i, j] = "0";
                    }
                }
            }
        }

        map.Generate(mapint, 80);
        //map.Generate( map = new int[,]
        //{
        //     {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
        //     {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
        //     {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
        //     {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
        //     {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
        //     {1,1,1,1,1,1,1,1,1,1,1,1,0,0,0,1 },
        //     {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
        //     {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
        //     {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
        //     {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
        //     {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
        //     {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
        //     {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
        //     {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
        //     {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
        //     {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
        //     {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
        //     {1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1 },
        //     {1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1 }


        //}, 80);

        // TODO: use this.Content to load your game content here
    }

    /// <summary>
    /// UnloadContent will be called once per game and is the place to unload
    /// all content.
    /// </summary>
    protected override void UnloadContent()
    {
        if (player.isDead == true)
        {
            this.Exit();
            RunningState.set_state(3);
        }
        else
            RunningState.set_state(1);
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
        if (player.isDead == true)
        {
            RunningState.setScore(player.score);
            this.Exit();
            RunningState.set_state(3);
        }

        timeSinceScore += (float)gameTime.ElapsedGameTime.TotalSeconds;

        if (timeSinceScore > 2f)
        {
            player.score += 100;
            timeSinceScore = 0f;
        }




        input.Update();
        player.Update(gameTime);
        enemy.Update(gameTime);
        UpdateBullets();

        if ((input.IsKeyDown(Keys.Z)))
        {
            player.spriteManager.setCurrentAction("shoot");
            if (input.WasKeyPressed(Keys.Z))
            {
                Shoot();
            }

        }

        else if ((input.IsKeyDown(Keys.Z)) && (input.IsKeyDown(Keys.Left)))
        {
            player.spriteManager.setCurrentAction("run+shoot");
            if (input.WasKeyPressed(Keys.Z))
            {
                player.velocity.X = -5*player.speed;
                player.spriteManager.setCurrentDirection("left");
                player.isLeft = true;
                Shoot();
            }

        }

        else if ((input.IsKeyDown(Keys.Z)) && (input.IsKeyDown(Keys.Right)))
        {
            player.spriteManager.setCurrentAction("run+shoot");
            if (input.WasKeyPressed(Keys.Z))
            {
                player.velocity.X = 5*player.speed;
                player.spriteManager.setCurrentDirection("right");
                player.isLeft = false;
                Shoot();
            }

        }

        else if ((input.IsKeyDown(Keys.Space)) )
        {

            player.spriteManager.setCurrentAction("jump");
            if (player.hasJumped == false)
            {
                player.position.Y -= 10f;
                player.velocity.Y = -9;
                player.hasJumped = true;
            }

        }

        else if ((input.IsKeyDown(Keys.Left)) && (input.IsKeyDown(Keys.Down)))
        {
            player.velocity.X = -5 * player.speed;
            player.spriteManager.setCurrentDirection("left");
            player.spriteManager.setCurrentAction("slide");
            player.isLeft = true;
        }

        else if ((input.IsKeyDown(Keys.Right)) && (input.IsKeyDown(Keys.Down)))
        {
            player.velocity.X = 5 * player.speed;
            player.spriteManager.setCurrentDirection("right");
            player.spriteManager.setCurrentAction("slide");
            player.isLeft = false;
        }

        else if (input.IsKeyDown(Keys.Left))
        {
            player.velocity.X = -5 * player.speed;
            player.spriteManager.setCurrentDirection("left");
            player.spriteManager.setCurrentAction("run");
            player.isLeft = true;
        }

        else if (input.IsKeyDown(Keys.Right))
        {
            player.velocity.X = 5 * player.speed;
            player.spriteManager.setCurrentDirection("right");
            player.spriteManager.setCurrentAction("run");
            player.isLeft = false;
        }


        else
        {
            player.velocity.X = 0;
            player.spriteManager.setCurrentAction("idle");
        }


        foreach (PlatformTiles tile in map.CollisionTiles)
        {
            player.PlatformCollsion(tile.Rectangle, map.Width, map.Height, camera.bottom_edge,camera.top_edge);
            enemy.Collision(tile.Rectangle, map.Width, map.Height);
            camera.Update(player.position, map.Width, map.Height, gameTime,player);
        }

        //for (int j = 0; j < bullets.Count; j++)
        //{
        //    if (bullets[j].TrapCollsion(bullets[j].rectangle, map.Width, map.Height, map.TrapTiles[j]))
        //    {
        //        BackgroundTiles newTile = new BackgroundTiles("0", map.TrapTiles[j].Rectangle);
        //        map.TrapTiles.RemoveAt(j);
        //        map.BackgroundTiles.Insert(j+1, newTile);

        //    }

        //}
        for (int i = 0; i < map.TrapTiles.Count; i++)
        {
            player.TrapCollsion(map.TrapTiles[i].Rectangle, map.Width, map.Height);
            foreach(Bullet bullet in bullets)
            {
                bullet.TrapCollsion(map.TrapTiles[i].Rectangle, map.Width, map.Height,map.TrapTiles[i] ,player);
                
            }
        }

        for (int i = 0; i < map.TrapTiles.Count; i++)
        {
            if(map.TrapTiles[i].isVisible == false)
            {
                BackgroundTiles newTile = new BackgroundTiles("0", map.TrapTiles[i].Rectangle);
                map.TrapTiles.RemoveAt(i);
                map.BackgroundTiles.Insert(i, newTile);
            }
        }



        base.Update(gameTime);
    }

    public AnimatedSpriteStripManager LoadSprites()
    {
        AnimatedSpriteStripManager player_manager = new AnimatedSpriteStripManager(7);

        // Player Idle
        player.texture = Content.Load<Texture2D>("SpriteSheet/idle");
        AnimatedSpriteStrip idle_strip = new AnimatedSpriteStrip(player.texture, 0.05f, true);
        idle_strip.setName("idle");
        player_manager.addAnimatedSpriteStrip(idle_strip);

        // Player Running
        player.texture = Content.Load<Texture2D>("SpriteSheet/run");
        AnimatedSpriteStrip run_strip = new AnimatedSpriteStrip(player.texture, 0.05f, true);
        run_strip.setName("run");
        player_manager.addAnimatedSpriteStrip(run_strip);

        // Player Jump
        player.texture = Content.Load<Texture2D>("SpriteSheet/jump");
        AnimatedSpriteStrip jump_strip = new AnimatedSpriteStrip(player.texture, 0.1f, true);
        jump_strip.setName("jump");
        player_manager.addAnimatedSpriteStrip(jump_strip);

        // Player Shoot
        player.texture = Content.Load<Texture2D>("SpriteSheet/shoot");
        AnimatedSpriteStrip shoot_strip = new AnimatedSpriteStrip(player.texture, 0.2f, false);
        shoot_strip.setName("shoot");
        player_manager.addAnimatedSpriteStrip(shoot_strip);

        // Player Run + Shoot
        player.texture = Content.Load<Texture2D>("SpriteSheet/run+shoot");
        AnimatedSpriteStrip run_shoot_strip = new AnimatedSpriteStrip(player.texture, 0.05f, true);
        run_shoot_strip.setName("run+shoot");
        player_manager.addAnimatedSpriteStrip(run_shoot_strip);

        // Player Slide
        player.texture = Content.Load<Texture2D>("SpriteSheet/slide");
        AnimatedSpriteStrip slide_strip = new AnimatedSpriteStrip(player.texture, 0.05f, true);
        slide_strip.setName("slide");
        player_manager.addAnimatedSpriteStrip(slide_strip);


        return player_manager;
    }

    public void Shoot()
    {
        Bullet newBullet = new Bullet(Content.Load<Texture2D>("objects/bullet"), player.isLeft);
        if (player.isLeft)
        {
            newBullet.velocity = new Vector2(-20, 0);
            newBullet.position = player.position + new Vector2(-40, -8);
        }

        else
        {
            newBullet.velocity = new Vector2(20, 0);
            newBullet.position = player.position + new Vector2(40, -8);
        }


        newBullet.position += newBullet.velocity;
        newBullet.isVisible = true;

        newBullet.rectangle.Width = newBullet.texture.Width;
        newBullet.rectangle.Height = newBullet.texture.Height;

        if (bullets.Count < 5)
        {
            bullets.Add(newBullet);
        }

    }

    public void UpdateBullets()
    {
        for (int j = 0; j < bullets.Count; j++)
        {
            {
                bullets[j].position += bullets[j].velocity;
                bullets[j].rectangle.X = (int)bullets[j].position.X;
                bullets[j].rectangle.Y = (int)bullets[j].position.Y;

                if (bullets[j].position.X < 0 || bullets[j].position.X > window_width)
                {
                    bullets[j].isVisible = false;
                }

                for (int i = 0; i < bullets.Count; i++)
                {
                    if (!bullets[i].isVisible)
                    {
                        bullets.RemoveAt(i);
                        i--;
                    }
                }

            }
        }
    }



    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);


        // Draw the sprite.
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.Transform);

        map.Draw(spriteBatch);
        //spriteBatch.Draw(player.rectangle_texture, player.hitbox_rectangle, Color.White);
        spriteBatch.Draw(door, new Vector2(600, 155), Color.White);

        player.spriteManager.Draw(gameTime, spriteBatch, player);
        //enemy.Draw(spriteBatch);

        foreach (Bullet bullet in bullets)
        {
            bullet.Draw(spriteBatch, player);
        }
        spriteBatch.DrawString(font, "Score: "+player.score.ToString(), new Vector2(20, camera.top_edge+20), Color.Red);


        spriteBatch.End();

        base.Draw(gameTime);
    }

}
