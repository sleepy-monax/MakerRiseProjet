using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.RiseEngine.Core.Rendering.SpriteSheets
{
    public class Sprite
    {

        SpriteSheet ParrentSpriteSheet;
        public TilesheetColectionItem MainSprite;
        public TilesheetColectionItem[] sprites;
        int CurrentFrame = 0;
        int ElapsedTime = 0;
        bool Forward = true;

        int LasteFrame = 0;




        public Sprite(SpriteSheet _ParrentMapedSheet, TilesheetColectionItem _sprites)
        {

            ParrentSpriteSheet = _ParrentMapedSheet;
            MainSprite = _sprites;
            sprites = new TilesheetColectionItem[1];
            sprites[0] = _sprites;


        }

        public Sprite(SpriteSheet _ParrentMapedSheet, TilesheetColectionItem _MainSprite, TilesheetColectionItem[] _sprites)
        {

            ParrentSpriteSheet = _ParrentMapedSheet;
            MainSprite = _MainSprite;
            sprites = _sprites;
            MainSprite.Height = sprites[0].Height;
            MainSprite.Width = sprites[0].Width;

        }




        public void Draw(SpriteBatch spritebatch, Rectangle DestinationRectangle, Color color, GameTime gameTime)
        {


            if (MainSprite.Animated == false)
            {
                spritebatch.Draw(ParrentSpriteSheet.SpriteSheetTexture2D, DestinationRectangle, new Rectangle(MainSprite.X * ParrentSpriteSheet.SpriteSize.X, MainSprite.Y * ParrentSpriteSheet.SpriteSize.Y, MainSprite.Width * ParrentSpriteSheet.SpriteSize.X, MainSprite.Height * ParrentSpriteSheet.SpriteSize.Y), color);

            }
            else
            {

                UpdateAnimation(gameTime);
                spritebatch.Draw(ParrentSpriteSheet.SpriteSheetTexture2D, DestinationRectangle, new Rectangle(sprites[CurrentFrame].X * ParrentSpriteSheet.SpriteSize.X, sprites[CurrentFrame].Y * ParrentSpriteSheet.SpriteSize.Y, sprites[CurrentFrame].Width * ParrentSpriteSheet.SpriteSize.X, sprites[CurrentFrame].Height * ParrentSpriteSheet.SpriteSize.Y), color);
            }


        }

        public void UpdateAnimation(GameTime _gameTime)
        {
            if (MainSprite.Animated == true)
            {

                if (!(LasteFrame == Engine.CurrentFrame))
                {
                    LasteFrame = Engine.CurrentFrame;

                    ElapsedTime += _gameTime.ElapsedGameTime.Milliseconds;

                    if (ElapsedTime >= MainSprite.FrameTime)
                    {
                        ElapsedTime = 0;
                        switch (MainSprite.AnimMode)
                        {
                            case AnimationMode.Forward:

                                CurrentFrame++;
                                if (CurrentFrame == MainSprite.Frames.Length)
                                    CurrentFrame = 0;

                                break;
                            case AnimationMode.BackAndForward:

                                if (Forward)
                                {
                                    CurrentFrame++;
                                    if (CurrentFrame == MainSprite.Frames.Length)
                                    {
                                        CurrentFrame -= 2;
                                        Forward = false;
                                    }

                                }
                                else
                                {
                                    CurrentFrame--;
                                    if (CurrentFrame == -1)
                                    {
                                        CurrentFrame += 2;
                                        Forward = true;
                                    }
                                }

                                break;
                            default:
                                break;
                        }
                    }

                }


            }
        }

    }
}
