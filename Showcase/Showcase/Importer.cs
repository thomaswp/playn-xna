using PlayNXNA;
using playn.core;

namespace Showcase
{
    public class Importer
    {
        public static void Import()
        {
            XNAGraphics graphics = (XNAGraphics)PlayN.graphics();
            graphics.registerFont("Helvetica", "Helvetica-24", 24, Font.Style.PLAIN);
            graphics.registerFont("Helvetica", "Helvetica-24-Bold", 24, Font.Style.BOLD);
            graphics.registerFont("Helvetica", "Helvetica-24-Italic", 24, Font.Style.ITALIC);
            graphics.registerFont("Helvetica", "Helvetica-24-Bold-Italic", 24, Font.Style.BOLD_ITALIC);
            graphics.registerFont("Helvetica", "Helvetica-12", 12, Font.Style.PLAIN);
            graphics.registerFont("Helvetica", "Helvetica-12-Bold", 12, Font.Style.BOLD);
            graphics.registerFont("Helvetica", "Helvetica-12-Italic", 12, Font.Style.ITALIC);
            graphics.registerFont("Helvetica", "Helvetica-12-Bold-Italic", 12, Font.Style.BOLD_ITALIC);
            graphics.registerFont("Helvetica", "Helvetica-9", 9, Font.Style.PLAIN);
            graphics.registerFont("Museo-300", "Museo-24", 24, Font.Style.PLAIN);
            graphics.registerFont("Courier", "CourierNew-24", 24, Font.Style.PLAIN);
            graphics.registerFont("Courier", "CourierNew-12", 12, Font.Style.PLAIN);
        }
    }
}
