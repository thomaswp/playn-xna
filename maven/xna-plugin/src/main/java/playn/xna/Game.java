package playn.xna;

import java.io.PrintWriter;
import java.util.HashMap;

public class Game implements IWritable {
	
	public String gameName;
	public XNAProject game;
	
	public Game(String gameName, XNAProject game) {
		this.gameName = gameName;
		this.game = game;
	}
	
	public void write(PrintWriter writer) {
		HashMap<String, String> map = new HashMap<String, String>();
		map.put("game", gameName);
		map.put("name", game.name);
		writer.print(FormatUtils.format(FORMAT, map));
	}
	
	public final static String FORMAT = 
			"using System;\n" + 
			"using System.Collections.Generic;\n" + 
			"using System.Linq;\n" + 
			"using System.Text;\n" + 
			"using playn.core;\n" + 
			"using PlayNXNA;\n" + 
			"\n" + 
			"namespace $name$\n" + 
			"{\n" + 
			"    public class $name$XNA : XNAGame\n" + 
			"    {\n" + 
			"        protected override void Initialize()\n" + 
			"        {\n" + 
			"            base.Initialize();\n" + 
			"\n" + 
			"            Importer.Import();\n" +
			"            Game game = new $game$();\n" + 
			"            PlayN.run(game);\n" + 
			"        }\n" + 
			"\n" + 
			"        protected override XNAPlatform registerPlatform()\n" + 
			"        {\n" + 
			"            return XNAPlatform.register();\n" + 
			"        }\n" + 
			"\n" + 
			"        static void Main(string[] args)\n" + 
			"        {\n" + 
			"            using ($name$XNA game = new $name$XNA())\n" + 
			"            {\n" + 
			"                game.Run();\n" + 
			"            }\n" + 
			"        }\n" + 
			"    }\n" + 
			"}\n";
}
