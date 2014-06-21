using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using playn.core;
using tripleplay.util;
using System.Diagnostics;

namespace TestGame
{
    class CanvasTestGame : Game.Default, Mouse.Listener
    {
        public CanvasTestGame() : base(16) { }

        ImageLayer canvasImageLayer;

        public override void init()
        {
            Image pea;

            ImageLayer imageLayer = PlayN.graphics().createImageLayer(PlayN.assets().getImage("images/bg.png"));
            PlayN.graphics().rootLayer().add(imageLayer);

            ImageLayer imageLayer2 = PlayN.graphics().createImageLayer(pea = PlayN.assets().getImageSync("images/pea.png"));
            PlayN.graphics().rootLayer().add(imageLayer2);

            //testSpeed();

            CanvasImage image = PlayN.graphics().createImage(300, 300);
            Canvas canvas = image.canvas();

            canvas.setFillColor(Color.argb(255, 0, 0, 0));
            Path path = canvas.createPath();
            path.moveTo(20, 20);
            path.lineTo(70, 25);
            path.lineTo(35, 45);
            path.lineTo(30, 80);
            path.close();
            canvas.strokePath(path);

            canvas.setStrokeWidth(3);
            canvas.setStrokeColor(Color.argb(255, 0, 0, 0));

            canvas.setFillColor(Color.argb(100, 0, 0, 255));
            canvas.strokeCircle(41.5f, 30, 40);
            canvas.fillCircle(41.5f, 30, 40);
            canvas.setFillColor(Color.argb(100, 255, 0, 0));
            canvas.translate(51, 51);
            canvas.rotate((float)Math.PI / 10);
            canvas.scale(0.5f, 0.5f);
            canvas.fillRect(0, 0, 100, 75);
            canvas.strokeRect(0, 0, 100, 75);
            canvas.drawImage(pea, 0, 0);
            canvas.setFillColor(Colors.BLACK);
            canvas.drawLine(0, 0, 50, 5);

            PlayN.graphics().rootLayer().add(canvasImageLayer = PlayN.graphics().createImageLayer(image));

            PlayN.mouse().setListener(this);
        }

        private static void testSpeed()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int n = 50;
            for (int i = 0; i < n; i++)
            {
                CanvasImage image = PlayN.graphics().createImage(300, 300);
                Canvas canvas = image.canvas();
                canvas.fillRect(0, 0, image.width(), image.height());
            }
            sw.Stop();
            Console.WriteLine(sw.ElapsedMilliseconds / n);
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
