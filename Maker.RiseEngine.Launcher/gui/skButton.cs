using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Maker.Skift.Controls
{
    public class skButton : Button
    {

        private MouseState State = MouseState.None;

        #region Properties
        [Category("Colors")]
        public Color TextColor { get; set; } = Color.Black;
        [Category("Colors")]
        public Color GradientColor { get; set; } = Color.Gainsboro;

        [Category("Options")]
        public bool Flat { get; set; } = false;
        [Category("Options")]
        public bool Gardient { get; set; } = false;
        #endregion

        public skButton()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);
            DoubleBuffered = true;
            Size = new Size(128, 32);
            BackColor = Color.White;
            Font = new Font("Segoe UI", 10);
            Cursor = Cursors.Hand;
        }

        #region MouseEvent Handeling
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            State = MouseState.Down;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            State = MouseState.Over;
            Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            State = MouseState.Over;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            State = MouseState.None;
            Invalidate();
        }
        #endregion

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle Base = new Rectangle(0, 0, Width, Height);

            // Setup drawing instace.
            Bitmap controlBitmap = new Bitmap(Width, Height);
            Graphics G = Graphics.FromImage(controlBitmap);

            // Set up graphic.
            G.SmoothingMode = SmoothingMode.HighQuality;
            G.PixelOffsetMode = PixelOffsetMode.HighQuality;
            G.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            // Clear the bitmap.
            G.Clear(Color.White);

            // Draw Base.
            G.FillRectangle(new SolidBrush(BackColor), Base);

            // Draw Advanced style.
            if (!Flat)
            {
                LinearGradientBrush gBrush = new LinearGradientBrush(Base, BackColor, GradientColor, LinearGradientMode.Vertical);
                G.FillRectangle(gBrush, Base);
                G.DrawRectangle(new Pen(new SolidBrush(Color.FromArgb(100, 0, 0, 0))), Base);
            }

            // Draw mouse stats feedbacks.
            switch (State)
            {
                case MouseState.Over:
                    G.FillRectangle(new SolidBrush(Color.FromArgb(20, Color.White)), Base);
                    G.DrawRectangle(new Pen(new SolidBrush(Color.Black)), Base);
                    break;
                case MouseState.Down:
                    G.FillRectangle(new SolidBrush(Color.FromArgb(20, Color.Black)), Base);
                    G.DrawRectangle(new Pen(new SolidBrush(Color.Black)), Base);
                    break;
            }

            //-- Text
            G.DrawString(Text, Font, new SolidBrush(TextColor), Base, Helpers.CenterSF);

            if (!Enabled) {
                G.FillRectangle(new SolidBrush(Color.FromArgb(100, Color.White)), Base);
            }

            base.OnPaint(e);

            // Finaliazing Graphics.
            G.Dispose();
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.DrawImageUnscaled(controlBitmap, 0, 0);
            controlBitmap.Dispose();
        }


    }
}
