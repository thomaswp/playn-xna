package playn.xna;

import java.io.PrintWriter;
import java.util.HashMap;

public class Solution {
	
	public XNAProject game;
	public ContentProject content;
	public String guid;
	
	private static String FORMAT = 
			  "\nMicrosoft Visual Studio Solution File, Format Version 11.00\n"
			+ "# Visual C# Express 2010\n"
			+ "Project(\"{$guid$}\") = \"$gameName$\", \"game\\$gameName$.csproj\", \"{$gameGuid$}\"\n"
			+ "EndProject\n"
			+ "Project(\"{$guid$}\") = \"$gameName$Content\", \"content\\$gameName$Content.contentproj\", \"{$contentGuid$}\"\n"
			+ "EndProject\nGlobal\n"
			+ "\tGlobalSection(SolutionConfigurationPlatforms) = preSolution\n\t\tDebug|x86 = Debug|x86\n"
			+ "\t\tRelease|x86 = Release|x86\n"
			+ "\tEndGlobalSection\n"
			+ "\tGlobalSection(ProjectConfigurationPlatforms) = postSolution\n"
			+ "\t\t{$gameGuid$}.Debug|x86.ActiveCfg = Debug|x86\n"
			+ "\t\t{$gameGuid$}.Debug|x86.Build.0 = Debug|x86\n"
			+ "\t\t{$gameGuid$}.Release|x86.ActiveCfg = Release|x86\n"
			+ "\t\t{$gameGuid$}.Release|x86.Build.0 = Release|x86\n"
			+ "\t\t{$contentGuid$}.Debug|x86.ActiveCfg = Debug|x86\n"
			+ "\t\t{$contentGuid$}.Release|x86.ActiveCfg = Release|x86\n"
			+ "\tEndGlobalSection\n"
			+ "\tGlobalSection(SolutionProperties) = preSolution\n\t\tHideSolutionNode = FALSE\n"
			+ "\tEndGlobalSection\nEndGlobal\n";
	
	public Solution(XNAProject game) {
		this.game = game;
		this.content = game.content;
		this.guid = "FAE04EC0-301F-11D3-BF4B-00C04F79EFBC";
	}
	
	public void write(PrintWriter writer) {
		HashMap<String, String> map = new HashMap<String, String>();
		map.put("gameName", game.name);
		map.put("gameGuid", game.guid);
		map.put("contentGuid", content.guid);
		map.put("guid", guid);
		writer.print(FormatUtils.format(FORMAT, map));
		writer.flush();
	}
}
