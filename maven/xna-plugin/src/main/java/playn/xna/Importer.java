package playn.xna;

import java.io.PrintWriter;
import java.util.HashMap;

public class Importer implements IWritable {
	
	private XNAProject game;
	private String fontImport;
	
	public Importer(XNAProject game, Font[] fonts) {
		this.game = game;
		fontImport = "";
		for (Font font : fonts) {
			HashMap<String, String> map = new HashMap<String, String>();
			map.put("name", font.name);
			map.put("file", font.getFilename(false));
			map.put("size", "" + font.size);
			map.put("style", font.getPlayNStyle());
			fontImport += FormatUtils.format(FONT_FORMAT, map);
		}
	}
	
	public void write(PrintWriter writer) {
		HashMap<String, String> map = new HashMap<String, String>();
		map.put("game", game.name);
		map.put("fonts", fontImport);
		
		writer.print(FormatUtils.format(FORMAT, map));
	}
	
	public final static String FORMAT = 
			"using PlayNXNA;\n" + 
			"using playn.core;\n" + 
			"\n" + 
			"namespace $game$\n" + 
			"{\n" + 
			"    public class Importer\n" + 
			"    {\n" + 
			"        public static void Import()\n" + 
			"        {\n" + 
			"            XNAGraphics graphics = (XNAGraphics)PlayN.graphics();\n" + 
			"$fonts$" +
			"        }\n" + 
			"    }\n" + 
			"}\n";
	
	public final static String FONT_FORMAT = 
			"            graphics.registerFont(\"$name$\", \"fonts\\\\$file$.spritefont\", $size$f, Font.Style.$style$);\n";
}
