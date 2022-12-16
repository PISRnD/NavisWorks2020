using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PivdcSupportUi
{
    public class WinFormToolTipHelp : ToolTip
    {
        private Button btnTemporay;

        private bool myAutoSize { get; set; }
        private int PADDING { get; set; }
        private Size mySize { get; set; }
        private int myInternalImageWidth { get; set; }
        private Brush myBackColorBrush { get; set; }
        private Brush myBorderBrush { get; set; }
        private Font myFont { get; set; }
        private Brush myTextBrush { get; set; }
        private int BORDER_THICKNESS { get; set; }
        private StringFormat MyTextFormat { get; set; }

        public WinFormToolTipHelp(bool isAutoSize, FontFamily fontFamily, float fontSize, Color textColor, Color borderColor, Color rectangleColor)
        {
            InitializeComponent();
            myAutoSize = isAutoSize;
            this.OwnerDraw = true;
            PADDING = 0;
            mySize = new Size(300, 150);
            myFont = new Font(fontFamily, fontSize);
            BORDER_THICKNESS = 0;
            myTextBrush = new SolidBrush(textColor);
            myBorderBrush = new SolidBrush(borderColor);
            myBackColorBrush = new SolidBrush(rectangleColor);
            MyTextFormat = new StringFormat
            {
                FormatFlags = StringFormatFlags.LineLimit,
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.None
            };
            this.Popup += new PopupEventHandler(CustomizedToolTip_Popup);
            this.Draw += new DrawToolTipEventHandler(CustomizedToolTip_Draw);
        }

        private void CustomizedToolTip_Popup(object sender, PopupEventArgs e) // use this event to set the size of the tool tip
        {
            if (OwnerDraw)
            {
                if (!myAutoSize)
                {
                    e.ToolTipSize = mySize;
                    myInternalImageWidth = mySize.Height;
                }
                else
                {
                    Size oldSize = e.ToolTipSize;
                    Control parent = e.AssociatedControl;
                    Image toolTipImage = parent.Tag as Image;
                    if (toolTipImage != null)
                    {
                        myInternalImageWidth = oldSize.Height;
                        oldSize.Width += myInternalImageWidth + PADDING;
                    }
                    else
                    {
                        oldSize.Width += PADDING;
                    }
                    e.ToolTipSize = oldSize;
                }
            }
        }

        private void CustomizedToolTip_Draw(object sender, DrawToolTipEventArgs e) // use this to customzie the tool tip
        {
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            Rectangle myToolTipRectangle = new Rectangle
            {
                Size = e.Bounds.Size
            };
            e.Graphics.FillRectangle(myBorderBrush, myToolTipRectangle);
            Rectangle myImageRectangle = Rectangle.Inflate(myToolTipRectangle, -BORDER_THICKNESS, -BORDER_THICKNESS);
            e.Graphics.FillRectangle(myBackColorBrush, myImageRectangle);
            Control parent = e.AssociatedControl;
            Image toolTipImage = parent.Tag as Image;
            if (toolTipImage != null)
            {
                myImageRectangle.Width = myInternalImageWidth;
                Rectangle myTextRectangle = new Rectangle(myImageRectangle.Right, myImageRectangle.Top,
                (myToolTipRectangle.Width - myImageRectangle.Right - BORDER_THICKNESS), myImageRectangle.Height)
                {
                    Location = new Point(myImageRectangle.Right, myImageRectangle.Top)
                };
                e.Graphics.FillRectangle(myBackColorBrush, myTextRectangle);
                e.Graphics.DrawImage(toolTipImage, myImageRectangle);
                e.Graphics.DrawString(e.ToolTipText, myFont, myTextBrush, myTextRectangle, MyTextFormat);
            }
            else
            {
                e.Graphics.DrawString(e.ToolTipText, myFont, myTextBrush, myImageRectangle, MyTextFormat);
            }
        }

        private void InitializeComponent()
        {
            this.btnTemporay = new System.Windows.Forms.Button();
            // 
            // btnTemporay
            // 
            this.btnTemporay.Location = new System.Drawing.Point(0, 0);
            this.btnTemporay.Name = "btnTemporay";
            this.btnTemporay.Size = new System.Drawing.Size(75, 23);
            this.btnTemporay.TabIndex = 0;
            this.btnTemporay.Text = "Temporary Button";
            this.btnTemporay.UseVisualStyleBackColor = true;
        }
    }
}