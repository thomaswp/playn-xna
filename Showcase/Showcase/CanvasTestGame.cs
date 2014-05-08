using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using tripleplay.util;

namespace Showcase
{
    class CanvasTestGame : Game.Default, Mouse.Listener
    {
        public CanvasTestGame() : base(16) { }

        ImageLayer canvasImageLayer;

        public override void init()
        {
            Image pea;

            ImageLayer imageLayer = PlayN.graphics().createImageLayer(PlayN.assets().getImage("peas/images/bg"));
            PlayN.graphics().rootLayer().add(imageLayer);

            ImageLayer imageLayer2 = PlayN.graphics().createImageLayer(pea = PlayN.assets().getImageSync("peas/images/pea"));
            PlayN.graphics().rootLayer().add(imageLayer2);

            CanvasImage image = PlayN.graphics().createImage(100, 150);
            Canvas canvas = image.canvas();
            canvas.setFillColor(Color.argb(100, 0, 0, 255));
            canvas.fillCircle(40, 30, 40);
            canvas.setFillColor(Color.argb(100, 255, 0, 0));
            canvas.fillRoundRect(0, 25, 100, 75, 15);
            canvas.drawImage(pea, 0, 0);
            canvas.setFillColor(Colors.BLACK);
            canvas.drawLine(0, 0, 20, 150);
            
            PlayN.graphics().rootLayer().add(canvasImageLayer = PlayN.graphics().createImageLayer(image));

            PlayN.mouse().setListener(this);
        }

        public void onMouseDown(Mouse.ButtonEvent mbe)
        {
        }

        public void onMouseMove(Mouse.MotionEvent mme)
        {

            canvasImageLayer.setTranslation(mme.x(), mme.y());
        }

        public void onMouseUp(Mouse.ButtonEvent mbe)
        {
        }

        public void onMouseWheelScroll(Mouse.WheelEvent mwe)
        {
        }
    }
}
