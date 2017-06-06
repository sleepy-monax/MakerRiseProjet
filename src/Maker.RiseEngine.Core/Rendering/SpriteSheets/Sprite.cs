using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Maker.RiseEngine.Rendering.SpriteSheets
{
    public enum AnimationMode {Forward, BackAndForward}

    public class Sprite
    {
        SpriteSheet spriteSheet;
        public Frame[] AnimationFrames;
        AnimationMode AnimationMode;
        int AnimationSpeed;
        bool IsAnimated;

        int CurrentFrame = 0;
        int ElapsedTime = 0;
        bool Forward = true;
        int LasteFrame = 0;

        public Sprite(SpriteSheet spriteSheet, Frame frame)
        {
            this.spriteSheet = spriteSheet;

            IsAnimated = false;
            AnimationFrames = new Frame[1];
            AnimationFrames[0] = frame;
        }

        public Sprite(SpriteSheet SpriteSheet, Frame[] animationFrames, AnimationMode animationMode, int animationSpeed)
        {
            spriteSheet = SpriteSheet;

            IsAnimated = true;
            AnimationFrames = animationFrames;
            AnimationSpeed = animationSpeed;
            AnimationMode = animationMode;
        }

        public void Draw(SpriteBatch spritebatch, Rectangle DestinationRectangle, Color color, GameTime gameTime)
        {

            if (IsAnimated)
            {
                spritebatch.Draw(spriteSheet.Texture, DestinationRectangle, new Rectangle(AnimationFrames[0].X * spriteSheet.TileSize.X, AnimationFrames[0].Y * spriteSheet.TileSize.Y, AnimationFrames[0].Width * spriteSheet.TileSize.X, AnimationFrames[0].Height * spriteSheet.TileSize.Y), color);
            }
            else
            {
                UpdateAnimation(gameTime);
                spritebatch.Draw(spriteSheet.Texture, DestinationRectangle, new Rectangle(AnimationFrames[CurrentFrame].X * spriteSheet.TileSize.X, AnimationFrames[CurrentFrame].Y * spriteSheet.TileSize.Y, AnimationFrames[CurrentFrame].Width * spriteSheet.TileSize.X, AnimationFrames[CurrentFrame].Height * spriteSheet.TileSize.Y), color);
            }

        }

        public void UpdateAnimation(GameTime gameTime)
        {
            if (IsAnimated && !(LasteFrame == Rise.Engine.CurrentFrame))
            {
                LasteFrame = Rise.Engine.CurrentFrame;
                ElapsedTime += gameTime.ElapsedGameTime.Milliseconds;

                if (ElapsedTime >= AnimationSpeed)
                {
                    ElapsedTime -= AnimationSpeed;
                    switch (AnimationMode)
                    {
                        case AnimationMode.Forward:
                            CurrentFrame++;
                            if (CurrentFrame == AnimationFrames.Length)
                                CurrentFrame = 0;

                            break;
                        case AnimationMode.BackAndForward:
                            if (Forward)
                            {
                                if (CurrentFrame == AnimationFrames.Length - 1)
                                {
                                    CurrentFrame -= 1;
                                    Forward = false;
                                }
                                else CurrentFrame++;
                            }
                            else
                            {
                                if (CurrentFrame == 0)
                                {
                                    CurrentFrame += 1;
                                    Forward = true;
                                }
                                else CurrentFrame--;
                            }

                            break;
                        default:
                            break;
                    }
                }

            }
 
        }
    }

    public class Frame
    {
        public int X;
        public int Y;
        public int Width;
        public int Height;

        public Frame(int x, int y, int w, int h)
        {
            X = x;
            Y = y;
            Width = w;
            Height = h;
        }
    }
}
