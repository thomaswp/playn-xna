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

            CanvasImage image = PlayN.graphics().createImage(250, 250);
            Canvas canvas = image.canvas();
            canvas.setFillColor(Color.argb(100, 0, 0, 255));
            canvas.fillCircle(40, 30, 40);
            canvas.setFillColor(Color.argb(100, 255, 0, 0));
            canvas.translate(51, 51);
            canvas.rotate((float)Math.PI / 10);
            canvas.scale(0.4f, 0.7f);
            canvas.fillRect(0, 0, 100, 75);
            canvas.drawImage(pea, 0, 0);
            canvas.setFillColor(Colors.BLACK);
            canvas.drawLine(0, 0, 50, 5);
            
            PlayN.graphics().rootLayer().add(canvasImageLayer = PlayN.graphics().createImageLayer(image));

            PlayN.mouse().setListener(this);
        }

        public void onMouseDown(Mouse.ButtonEvent mbe)
        {
        }

        public void onMouseMove(Mouse.MotionEvent mme)
        {

            canvasImageLayer.setTranslation(mme.x() + 25, mme.y() + 25);
        }

        public void onMouseUp(Mouse.ButtonEvent mbe)
        {
        }

        public void onMouseWheelScroll(Mouse.WheelEvent mwe)
        {
        }
    }
}
